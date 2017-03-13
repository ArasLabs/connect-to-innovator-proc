using System;
using mshtml;
using SHDocVw;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Reflection;

namespace InnovatorHelper
{
    /// <summary>
    /// Starting from 9.1 SP5 the Innovator\Client\default.aspx to which the origianl request was sent
    /// (e.g. http://localhost/MyInnovator) opens a new IE window with particular registry settings, etc.
    /// The change was mostly done to work-around the new IE8 threading model. As the result, when opening
    /// a new Innovator we have to:
    ///   a. send the URL to the original IE window
    ///   b. wait till the original window is closed and the new window, started by the original widnow, is opened
    ///   c. find the newly opened window and got its handle
    /// </summary>
    /// <remarks>
    /// The sample is provided "as is". In a slow network environment it might be either required to increase
    /// the timeout or change the algorithm how the "waiting" methods are working.
    /// </remarks>
    class InnovatorHelper
    {
        // It's assumed in the example that items of the specified type with specified 
        // IDs exist in the Innovator that the sample is connecting to.
        // In order to try please replace item IDs (and optionally type) on ones you would like to open
        public static string OpenItemType = "Part";
        public static string OpenItemID_1 = "8EBDEAE57ADA4D4A8EE3C9CB9CDBE13C";
        public static string OpenItemID_2 = "218F48BEC9C94401849FE84AEEBE66CE";

        // For connecting to existing Innovator only the URL must be specified in UI.
        // For opening a new Innovator the URL and db name must be specified in UI; specified
        // below user credentials are used during login.
        private static string m_innovatorURL;
        private static string m_databaseName;
        private static string userName = "admin";
        private static string password = "innovator";

        // Max waiting time in seconds after which the action is aborted
        private static int TimeoutSeconds = 30;

        private static WebBrowser browser = null;
        private static bool isLoggedIn = false;
        private static bool newIE = false;
        private static int origHWND = 0;

        private static DWebBrowserEvents2_OnQuitEventHandler IE_OnQuitHandler = null;

        internal static string InnovatorURL
        {
            get
            {
                return m_innovatorURL;
            }

            set
            {
                m_innovatorURL = value;
            }
        }

        internal static string DatabaseName
        {
            get
            {
                return m_databaseName;
            }

            set
            {
                m_databaseName = value;
            }
        }

        internal static bool NewIE
        {
            get
            {
                return newIE;
            }

            set
            {
                newIE = value;
                if (!newIE)
                    isLoggedIn = true;
            }
        }

        internal static WebBrowser Browser
        {
            get
            {
                if (browser == null)
                {
                    InternetExplorer ie = getIEHandle();
                    if (ie == null)
                        return null;

                    if (NewIE)
                    {
                        origHWND = ie.HWND;
                        ie.Visible = true;
                    }
                    else
                    {
                        origHWND = 0;
                    }

                    browser = (WebBrowser)ie;
                }

                return browser;
            }
        }

        /// <summary>
        /// If connection to existing Innovator is required then find the first IE window with the
        /// specified URL. Otherwise, create a new IE window.
        /// </summary>
        /// <returns></returns>
        private static InternetExplorer getIEHandle()
        {
            if (!NewIE)
            {
                SHDocVw.ShellWindows sws = new SHDocVw.ShellWindows();
                for (int i = 0; i < sws.Count; i++)
                {
                    SHDocVw.InternetExplorer ie = (SHDocVw.InternetExplorer)sws.Item(i);
                    int h = ie.HWND;
                    if (ie.LocationURL.Length >= InnovatorURL.Length && 
                        InnovatorURL.ToLower().Equals(ie.LocationURL.ToLower().Substring(0, InnovatorURL.Length)))
                        return ie;
                }

                return null;
            }
            else
            {
                return (InternetExplorer)(new InternetExplorerClass());
            }
        }

        private static void ie_OnQuit()
        {
            isLoggedIn = false;
            DetachEvents();
            browser = null;
        }

        private static void DetachEvents()
        {
            // detach events
            browser.OnQuit -= IE_OnQuitHandler;
            IE_OnQuitHandler = null;
            GC.Collect();
        }

        private static void Navigate(string url)
        {
            object flags = null;
            object targetFrameName = null;
            object postData = null;
            object headers = null;

            Browser.Navigate(url, ref flags, ref targetFrameName, ref postData, ref headers);
        }

        private static IHTMLElement GetElementById(string elementId)
        {
            HTMLDocument document = (HTMLDocument)Browser.Document;
            for (int i = 0; i < document.frames.length; i++)
            {
                object fIndex = i;
                HTMLWindow2 frame = (HTMLWindow2)document.frames.item(ref fIndex);
                if (frame.name == "main")
                {
                    document = (HTMLDocument)frame.document;
                    break;
                }
            }
            IHTMLElement element = document.getElementById(elementId);

            int nullElementCount = 0;
            // The following loop is to account for any latency that IE
            // might experience.  Tweak the number of times to attempt
            // to continue checking the document before giving up.
            while (element == null && nullElementCount < 2)
            {
                Thread.Sleep(500);
                element = document.getElementById(elementId);
                nullElementCount++;
            }

            return element;
        }

        private static void SetInputValue(string inputId, string elementValue)
        {
            HTMLInputElementClass input = (HTMLInputElementClass)GetElementById(inputId);
            input.value = elementValue;
        }

        public static void SelectComboboxValue(string inputId, string value)
        {
            HTMLSelectElementClass input = (HTMLSelectElementClass)GetElementById(inputId);
            HTMLElementCollection options = (HTMLElementCollection)input.options;
            HTMLOptionElement el = (HTMLOptionElement)options.namedItem(value);
            if (el != null)
                input.selectedIndex = el.index;
        }

        public static void ClickButton(string buttonId)
        {
            HTMLInputElementClass input = (HTMLInputElementClass)GetElementById(buttonId);
            input.click();
            WaitForComplete();
        }

        private static bool WaitForOriginalIEClose()
        {
            if (origHWND == 0)
                return true;

            int elapsedSeconds = 0;
            bool origOpen = true;
            while (origOpen && elapsedSeconds <= TimeoutSeconds)
            {
                Thread.Sleep(1000);
                origOpen = doesIEWindowExist(origHWND);
                elapsedSeconds++;
            }

            return (elapsedSeconds <= TimeoutSeconds);
        }

        private static bool doesIEWindowExist(int hwnd)
        {
            SHDocVw.ShellWindows sws = new SHDocVw.ShellWindows();
            for (int i = 0; i < sws.Count; i++)
            {
                SHDocVw.InternetExplorer ie = (SHDocVw.InternetExplorer)sws.Item(i);
                if (ie.HWND == hwnd)
                    return true;
            }

            return false;
        }

        private static void WaitForComplete()
        {
            int elapsedSeconds = 0;
            while (Browser.ReadyState != tagREADYSTATE.READYSTATE_COMPLETE && elapsedSeconds != TimeoutSeconds)
            {
                Thread.Sleep(1000);
                elapsedSeconds++;
            }
        }

        private static void WaitForElement(string id)
        {
            int elapsedSeconds = 0;
            while (GetElementById(id) == null && elapsedSeconds != TimeoutSeconds)
            {
                Thread.Sleep(1000);
                elapsedSeconds++;
            }
        }

        private static void Login(string userName, string pwd)
        {
            // Put the URL in the original window which starts the new IE window and closes
            // the original IE window.
            Navigate(InnovatorURL);

            // Wait till the original window is closed
            if (!WaitForOriginalIEClose())
                return;

            browser = null;
            NewIE = false;
            // The property 'get' must find the handle for the Innovator window opened by the original IE window
            if (Browser == null)
                return;

            // Wait for the newly opened window to fully load
            WaitForComplete();

            // Wait for controls loading to complete
            WaitForElement("login");

            // fill login form fields
            if (userName != "" && userName != null)
            {
                SetInputValue("login_name", userName);
            }
            if (password != "" && password != null)
            {
                SetInputValue("password", password);
            }
            SelectComboboxValue("database", DatabaseName);

            // login
            ClickButton("login");
            WaitForComplete();
            isLoggedIn = true;
        }

        #region Public members

        /// <summary>
        /// Log into Innovator
        /// </summary>
        public static void Login()
        {
            Login(userName, password);
        }

        /// <summary>
        /// Open Innovator item.
        /// </summary>
        /// <remarks>
        /// The method demos how to execute a JavaScript code in the Innovator client.
        /// </remarks>
        /// <param name="itemType"></param>
        /// <param name="itemId"></param>
        public static void OpenItem(string itemType, string itemId)
        {
            if (!isLoggedIn)
            {
                System.Windows.Forms.MessageBox.Show("Innovator window isn't opened. Please, login into Innovator first.");
                return;
            }

            HTMLDocument document = (HTMLDocument)Browser.Document;
            object window = document.Script;
            string script = string.Format(@"top.aras.uiShowItem('{0}', '{1}', 'tab view', true)", itemType, itemId);
            object[] args = new object[] { script };
            object res = window.GetType().InvokeMember("eval", BindingFlags.InvokeMethod, null, window, args);
        }

        // Note that even if we connected to a previously existing Innovator window, quiting the sample
        // through "Exit" button will close the Innovator IE window. Of course, other applications that uses
        // a similar code might have a different policy regarding closing Innovator.
        public static void Quit()
        {
            // If IE window is already closed or failed to closed 
            // then just swallow the exception
            try
            {
                if (browser != null && isLoggedIn)
                    browser.Quit();
            }
            catch(Exception)
            {}

            isLoggedIn = false;
        }

        #endregion
    }
}
