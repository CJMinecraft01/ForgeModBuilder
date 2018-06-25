namespace ForgeModBuilder.Forms
{
    partial class LanguageSelectionForm
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
            this.LanguagesComboBox = new System.Windows.Forms.ComboBox();
            this.OKButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.SelectLanguageLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // LanguagesComboBox
            // 
            this.LanguagesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.LanguagesComboBox.FormattingEnabled = true;
            this.LanguagesComboBox.Location = new System.Drawing.Point(15, 29);
            this.LanguagesComboBox.Name = "LanguagesComboBox";
            this.LanguagesComboBox.Size = new System.Drawing.Size(156, 21);
            this.LanguagesComboBox.TabIndex = 1;
            // 
            // OKButton
            // 
            this.OKButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OKButton.Location = new System.Drawing.Point(15, 56);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 2;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            // 
            // CancelButton
            // 
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton.Location = new System.Drawing.Point(96, 56);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 3;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            // 
            // SelectLanguageLabel
            // 
            this.SelectLanguageLabel.AutoSize = true;
            this.SelectLanguageLabel.Location = new System.Drawing.Point(12, 9);
            this.SelectLanguageLabel.Name = "SelectLanguageLabel";
            this.SelectLanguageLabel.Size = new System.Drawing.Size(126, 13);
            this.SelectLanguageLabel.TabIndex = 4;
            this.SelectLanguageLabel.Text = "Please select a language";
            // 
            // LanguageSelectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(186, 88);
            this.Controls.Add(this.SelectLanguageLabel);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.LanguagesComboBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "LanguageSelectionForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Language";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.Label SelectLanguageLabel;
        public System.Windows.Forms.ComboBox LanguagesComboBox;
        public System.Windows.Forms.Button OKButton;
        public System.Windows.Forms.Button CancelButton;
    }
}