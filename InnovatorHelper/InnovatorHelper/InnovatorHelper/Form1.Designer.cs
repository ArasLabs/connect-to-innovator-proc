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
      this.SuspendLayout();
      // 
      // openInnovatorButton
      // 
      this.openInnovatorButton.Location = new System.Drawing.Point(12, 31);
      this.openInnovatorButton.Name = "openInnovatorButton";
      this.openInnovatorButton.Size = new System.Drawing.Size(113, 23);
      this.openInnovatorButton.TabIndex = 0;
      this.openInnovatorButton.Text = "Open Innovator";
      this.openInnovatorButton.UseVisualStyleBackColor = true;
      this.openInnovatorButton.Click += new System.EventHandler(this.openInnovatorButton_Click);
      // 
      // openItemButton
      // 
      this.openItemButton.Location = new System.Drawing.Point(147, 31);
      this.openItemButton.Name = "openItemButton";
      this.openItemButton.Size = new System.Drawing.Size(75, 23);
      this.openItemButton.TabIndex = 1;
      this.openItemButton.Text = "Open Part2";
      this.openItemButton.UseVisualStyleBackColor = true;
      this.openItemButton.Click += new System.EventHandler(this.openItemButton_Click);
      // 
      // exitButton
      // 
      this.exitButton.Location = new System.Drawing.Point(147, 110);
      this.exitButton.Name = "exitButton";
      this.exitButton.Size = new System.Drawing.Size(75, 23);
      this.exitButton.TabIndex = 2;
      this.exitButton.Text = "Exit";
      this.exitButton.UseVisualStyleBackColor = true;
      this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(234, 145);
      this.Controls.Add(this.exitButton);
      this.Controls.Add(this.openItemButton);
      this.Controls.Add(this.openInnovatorButton);
      this.Name = "Form1";
      this.Text = "InnovatorHelper";
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button openInnovatorButton;
    private System.Windows.Forms.Button openItemButton;
    private System.Windows.Forms.Button exitButton;
  }
}

