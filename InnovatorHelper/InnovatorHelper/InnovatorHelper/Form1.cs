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
    }

    private void openInnovatorButton_Click(object sender, EventArgs e)
    {
      InnovatorHelper.Login();
    }

    private void openItemButton_Click(object sender, EventArgs e)
    {
      InnovatorHelper.OpenItem(InnovatorHelper.OpenItemType, InnovatorHelper.OpenItemID_2);
    }

    private void exitButton_Click(object sender, EventArgs e)
    {
      InnovatorHelper.Quit();
      Application.Exit();
    }
  }
}
