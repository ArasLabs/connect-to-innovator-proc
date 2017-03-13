using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace InnovatorHelper
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();
      radioButtonNew.Checked = true;
      this.openItemButton.Enabled = false;
    }

    private void openInnovatorButton_Click(object sender, EventArgs e)
    {
        if (radioButtonNew.Checked)
        {
            if (string.IsNullOrEmpty(InnovatorHelper.InnovatorURL) || string.IsNullOrEmpty(InnovatorHelper.DatabaseName))
            {
                MessageBox.Show("Both URL and database must be specified", "Missing Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            InnovatorHelper.NewIE = true;
            if (InnovatorHelper.Browser == null)
            {
                MessageBox.Show("Failed to open new IE window", "IE Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            openInnovatorButton.Enabled = false;
            radioButtonExisting.Enabled = false;
            radioButtonNew.Enabled = false;
            InnovatorHelper.Login();
            openItemButton.Enabled = true;
        }
        else
        {
            if (string.IsNullOrEmpty(InnovatorHelper.InnovatorURL))
            {
                MessageBox.Show("Innovator URL is not specified", "Missing Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            InnovatorHelper.NewIE = false;
            if (InnovatorHelper.Browser == null)
            {
                MessageBox.Show("Failed to find IE window with the specified Innovator URL", "IE Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            radioButtonExisting.Enabled = false;
            radioButtonNew.Enabled = false;
            InnovatorHelper.OpenItem(InnovatorHelper.OpenItemType, InnovatorHelper.OpenItemID_1);
            openItemButton.Enabled = true;
        }
    }

    private void openItemButton_Click(object sender, EventArgs e)
    {
        radioButtonExisting.Enabled = false;
        radioButtonNew.Enabled = false;
        InnovatorHelper.OpenItem(InnovatorHelper.OpenItemType, InnovatorHelper.OpenItemID_2);
    }

    private void exitButton_Click(object sender, EventArgs e)
    {
      InnovatorHelper.Quit();
      Application.Exit();
    }

    private void textBoxURL_TextChanged(object sender, EventArgs e)
    {
        InnovatorHelper.InnovatorURL = textBoxURL.Text;
    }

    private void textBoxDB_TextChanged(object sender, EventArgs e)
    {
        InnovatorHelper.DatabaseName = textBoxDB.Text;
    }

    private void radioButtonExisting_CheckedChanged(object sender, EventArgs e)
    {
        if (radioButtonExisting.Checked)
        {
            openInnovatorButton.Text = "Open Part 1";
            textBoxDB.Enabled = false;
        }
        else
        {
            openInnovatorButton.Text = "Login to Innovator";
            textBoxDB.Enabled = true;
        }
    }
  }
}
