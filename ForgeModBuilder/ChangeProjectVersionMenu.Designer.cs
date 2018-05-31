namespace ForgeModBuilder
{
    partial class ChangeProjectVersionMenu
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
            this.ForgeVersions = new System.Windows.Forms.ListBox();
            this.ChangeProjectVersionButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ForgeVersions
            // 
            this.ForgeVersions.FormattingEnabled = true;
            this.ForgeVersions.Location = new System.Drawing.Point(12, 12);
            this.ForgeVersions.Name = "ForgeVersions";
            this.ForgeVersions.Size = new System.Drawing.Size(156, 147);
            this.ForgeVersions.TabIndex = 2;
            // 
            // ChangeProjectVersionButton
            // 
            this.ChangeProjectVersionButton.Location = new System.Drawing.Point(12, 165);
            this.ChangeProjectVersionButton.Name = "ChangeProjectVersionButton";
            this.ChangeProjectVersionButton.Size = new System.Drawing.Size(156, 44);
            this.ChangeProjectVersionButton.TabIndex = 7;
            this.ChangeProjectVersionButton.Text = "Change Project Version";
            this.ChangeProjectVersionButton.UseVisualStyleBackColor = true;
            this.ChangeProjectVersionButton.Click += new System.EventHandler(this.ChangeProjectVersionButton_Click);
            // 
            // ChangeProjectVersionMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(180, 221);
            this.Controls.Add(this.ChangeProjectVersionButton);
            this.Controls.Add(this.ForgeVersions);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChangeProjectVersionMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Change Project Version";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox ForgeVersions;
        private System.Windows.Forms.Button ChangeProjectVersionButton;
    }
}