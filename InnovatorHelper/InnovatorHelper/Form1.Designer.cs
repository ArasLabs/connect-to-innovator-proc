namespace InnovatorHelper
{
  partial class Form1
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.openInnovatorButton = new System.Windows.Forms.Button();
        this.openItemButton = new System.Windows.Forms.Button();
        this.exitButton = new System.Windows.Forms.Button();
        this.label1 = new System.Windows.Forms.Label();
        this.textBoxURL = new System.Windows.Forms.TextBox();
        this.radioButtonExisting = new System.Windows.Forms.RadioButton();
        this.radioButtonNew = new System.Windows.Forms.RadioButton();
        this.label2 = new System.Windows.Forms.Label();
        this.textBoxDB = new System.Windows.Forms.TextBox();
        this.SuspendLayout();
        // 
        // openInnovatorButton
        // 
        this.openInnovatorButton.Location = new System.Drawing.Point(305, 30);
        this.openInnovatorButton.Name = "openInnovatorButton";
        this.openInnovatorButton.Size = new System.Drawing.Size(113, 23);
        this.openInnovatorButton.TabIndex = 0;
        this.openInnovatorButton.Text = "Login to Innovator";
        this.openInnovatorButton.UseVisualStyleBackColor = true;
        this.openInnovatorButton.Click += new System.EventHandler(this.openInnovatorButton_Click);
        // 
        // openItemButton
        // 
        this.openItemButton.Location = new System.Drawing.Point(305, 59);
        this.openItemButton.Name = "openItemButton";
        this.openItemButton.Size = new System.Drawing.Size(113, 23);
        this.openItemButton.TabIndex = 1;
        this.openItemButton.Text = "Open Part2";
        this.openItemButton.UseVisualStyleBackColor = true;
        this.openItemButton.Click += new System.EventHandler(this.openItemButton_Click);
        // 
        // exitButton
        // 
        this.exitButton.Location = new System.Drawing.Point(305, 88);
        this.exitButton.Name = "exitButton";
        this.exitButton.Size = new System.Drawing.Size(113, 23);
        this.exitButton.TabIndex = 2;
        this.exitButton.Text = "Exit";
        this.exitButton.UseVisualStyleBackColor = true;
        this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
        // 
        // label1
        // 
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(12, 17);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(244, 13);
        this.label1.TabIndex = 4;
        this.label1.Text = "Innovator URL (e.g. http://localhost/MyInnovator)";
        // 
        // textBoxURL
        // 
        this.textBoxURL.Location = new System.Drawing.Point(15, 33);
        this.textBoxURL.Name = "textBoxURL";
        this.textBoxURL.Size = new System.Drawing.Size(256, 20);
        this.textBoxURL.TabIndex = 5;
        this.textBoxURL.TextChanged += new System.EventHandler(this.textBoxURL_TextChanged);
        // 
        // radioButtonExisting
        // 
        this.radioButtonExisting.AutoSize = true;
        this.radioButtonExisting.Location = new System.Drawing.Point(15, 114);
        this.radioButtonExisting.Name = "radioButtonExisting";
        this.radioButtonExisting.Size = new System.Drawing.Size(109, 17);
        this.radioButtonExisting.TabIndex = 6;
        this.radioButtonExisting.TabStop = true;
        this.radioButtonExisting.Text = "Existing Innovator";
        this.radioButtonExisting.UseVisualStyleBackColor = true;
        this.radioButtonExisting.CheckedChanged += new System.EventHandler(this.radioButtonExisting_CheckedChanged);
        // 
        // radioButtonNew
        // 
        this.radioButtonNew.AutoSize = true;
        this.radioButtonNew.Location = new System.Drawing.Point(149, 114);
        this.radioButtonNew.Name = "radioButtonNew";
        this.radioButtonNew.Size = new System.Drawing.Size(95, 17);
        this.radioButtonNew.TabIndex = 7;
        this.radioButtonNew.TabStop = true;
        this.radioButtonNew.Text = "New Innovator";
        this.radioButtonNew.UseVisualStyleBackColor = true;
        // 
        // label2
        // 
        this.label2.AutoSize = true;
        this.label2.Location = new System.Drawing.Point(12, 64);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(84, 13);
        this.label2.TabIndex = 8;
        this.label2.Text = "Database Name";
        // 
        // textBoxDB
        // 
        this.textBoxDB.Location = new System.Drawing.Point(15, 80);
        this.textBoxDB.Name = "textBoxDB";
        this.textBoxDB.Size = new System.Drawing.Size(256, 20);
        this.textBoxDB.TabIndex = 9;
        this.textBoxDB.TextChanged += new System.EventHandler(this.textBoxDB_TextChanged);
        // 
        // Form1
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(430, 168);
        this.Controls.Add(this.textBoxDB);
        this.Controls.Add(this.label2);
        this.Controls.Add(this.radioButtonNew);
        this.Controls.Add(this.radioButtonExisting);
        this.Controls.Add(this.textBoxURL);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.exitButton);
        this.Controls.Add(this.openItemButton);
        this.Controls.Add(this.openInnovatorButton);
        this.Name = "Form1";
        this.Text = "InnovatorHelper";
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button openInnovatorButton;
    private System.Windows.Forms.Button openItemButton;
    private System.Windows.Forms.Button exitButton;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox textBoxURL;
    private System.Windows.Forms.RadioButton radioButtonExisting;
    private System.Windows.Forms.RadioButton radioButtonNew;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox textBoxDB;
  }
}

