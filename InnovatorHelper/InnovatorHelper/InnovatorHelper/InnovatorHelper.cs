using System;
using mshtml;
using SHDocVw;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Reflection;

namespace InnovatorHelper
{
  class InnovatorHelper
  {
    // In order to try replace type and item IDs on ones you would like to open
    public static string OpenItemType = "Part";
    public static string OpenItemID_1 = "EE2B97B5EFBF40988E6C5CEFA0C1371D";
    public static string OpenItemID_2 = "DAB4954352AD4C8B983E3C8CE291D04C";

    // In order to try replace on appropriate values
    private static string InnovatorURL = @"http://starcraft/Innovator5345TagHEAD/Client/scripts/Innovator.aspx";
    private static string databaseID = "SolutionsInnCore91Build5345TagHEAD";
    private static string userName = "admin";
    private static string password = "innovator";

    private static int TimeoutSeconds = 5;

    private static InternetExplorer ieApp = null;
    private static WebBrowser browser = null;
    private static bool isDocumentComplete = false;
    private static bool isLoggedIn = false;

    private static DWebBrowserEvents2_OnQuitEventHandler IE_OnQuitHandler = null;
    private static DWebBrowserEvents2_DocumentCompleteEventHandler IE_DocumentCompleteHandler = null;

    private static WebBrowser Browser
    {
      get
      {
        if (browser == null)
        {
          InternetExplorerClass ie = new InternetExplorerClass();
          ie.Visible = true;

          // Attach events
          IE_DocumentCompleteHandler = new DWebBrowserEvents2_DocumentCompleteEventHandler(IE_DocumentComplete);
          ie.DocumentComplete += IE_DocumentCompleteHandler;

          IE_OnQuitHandler = new DWebBrowserEvents2_OnQuitEventHandler(ie_OnQuit);
          ie.OnQuit += IE_OnQuitHandler;

          ieApp = ie;
          browser = (WebBrowser)ie;

          // Init browser
          browser.MenuBar = false;
          browser.AddressBar = false;
          browser.ToolBar = 0;

          isLoggedIn = false;
          isDocumentComplete = false;
        }

        return browser;
      }
    }

    private static void ie_OnQuit()
    {
      isLoggedIn = false;
      isDocumentComplete = false;
      DetachEvents();

      ieApp = null;
      browser = null;
    }

    private static void IE_DocumentComplete(object pDisp, ref object URL)
    {
      isDocumentComplete = true;
    }

    private static void DetachEvents()
    {
      // detach events
      ieApp.OnQuit -= IE_OnQuitHandler;
      ieApp.DocumentComplete -= IE_DocumentCompleteHandler;
      IE_DocumentCompleteHandler = null;
      IE_OnQuitHandler = null;
      GC.Collect();
    }

    private static void Navigate(string url)
    {
      object flags = null;
      object targetFrameName = null;
      object postData = null;
      object headers = null;

      isDocumentComplete = false;

      Browser.Navigate(url, ref flags, ref targetFrameName, ref postData, ref headers);
    }

    private static IHTMLElement GetElementById(string elementId)
    {
      HTMLDocument document = (HTMLDocument)Browser.Document;
      for (int i = 0; i < document.frames.length; i++ )
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
			isDocumentComplete = false;
      HTMLInputElementClass input = (HTMLInputElementClass)GetElementById(buttonId);
			input.click();
			WaitForComplete();
		}

		private static void WaitForComplete() 
    {
			int elapsedSeconds = 0;
			while (!isDocumentComplete && elapsedSeconds != TimeoutSeconds) 
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

    private static void Login(string userName, string pwd, string dbId)
    {
      // If you want only log into Innovator use following
      // Navigate(InnovatorURL);

      // If you want log into Innovator and open some item use this
      Navigate(string.Format("{0}?StartItem={1}:{2}" , InnovatorURL, OpenItemType, OpenItemID_1 ));

      // wait for page load
      WaitForComplete();
      // wait for controls loading complete
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
      if (dbId != "" && dbId != null)
      {
        SelectComboboxValue("database", dbId);
      }

      // login
      isDocumentComplete = false;
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
      Login(userName, password, databaseID);
    }

    /// <summary>
    /// Open Innovator item
    /// </summary>
    /// <param name="itemType"></param>
    /// <param name="itemId"></param>
    public static void OpenItem(string itemType, string itemId)
    {
      if (!isLoggedIn)
      {
        System.Windows.Forms.MessageBox.Show("Innovator window isn't opened. Please, login into Innovator before.");
        return;
      }

      HTMLDocument document = (HTMLDocument)Browser.Document;
      object window = document.Script;
      string script = string.Format(@"top.aras.uiShowItem('{0}', '{1}', 'tab view', true)", itemType, itemId);
      object[] args = new object[] { script };
      object res = window.GetType().InvokeMember("eval", BindingFlags.InvokeMethod, null, window, args);
    }

    public static void Quit()
    {
      if (browser != null && isLoggedIn)
        browser.Quit();
      isLoggedIn = false;
    }

    #endregion
  }
}
