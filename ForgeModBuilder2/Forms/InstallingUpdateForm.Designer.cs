using ForgeModBuilder.Managers;

namespace ForgeModBuilder.Forms
{
    partial class InstallingUpdateForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InstallingUpdateForm));
            this.CancelButton = new System.Windows.Forms.Button();
            this.ProgressBar = new System.Windows.Forms.ProgressBar();
            this.TitleLabel = new System.Windows.Forms.Label();
            this.TaskDetailsLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // CancelButton
            // 
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton.Location = new System.Drawing.Point(283, 55);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 0;
            this.CancelButton.Text = LanguageManager.CurrentLanguage.Localize("button.cancel");
            this.CancelButton.UseVisualStyleBackColor = true;
            // 
            // ProgressBar
            // 
            this.ProgressBar.Location = new System.Drawing.Point(12, 32);
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(346, 17);
            this.ProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.ProgressBar.TabIndex = 1;
            this.ProgressBar.Maximum = 100;
            // 
            // TitleLabel
            // 
            this.TitleLabel.AutoSize = true;
            this.TitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TitleLabel.Location = new System.Drawing.Point(10, 10);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(171, 15);
            this.TitleLabel.TabIndex = 2;
            this.TitleLabel.Text = LanguageManager.CurrentLanguage.Localize("form.update.label.title");
            // 
            // TaskDetailsLabel
            // 
            this.TaskDetailsLabel.AutoSize = true;
            this.TaskDetailsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TaskDetailsLabel.Location = new System.Drawing.Point(10, 55);
            this.TaskDetailsLabel.Name = "TaskDetailsLabel";
            this.TaskDetailsLabel.Size = new System.Drawing.Size(112, 15);
            this.TaskDetailsLabel.TabIndex = 3;
            this.TaskDetailsLabel.Text = LanguageManager.CurrentLanguage.Localize("form.update.label.task_details.1");
            // 
            // InstallingUpdateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(370, 83);
            this.Controls.Add(this.TaskDetailsLabel);
            this.Controls.Add(this.TitleLabel);
            this.Controls.Add(this.ProgressBar);
            this.Controls.Add(this.CancelButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "InstallingUpdateForm";
            this.Text = LanguageManager.CurrentLanguage.Localize("form.main.title");
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button CancelButton;
        public System.Windows.Forms.ProgressBar ProgressBar;
        public System.Windows.Forms.Label TaskDetailsLabel;
        public System.Windows.Forms.Label TitleLabel;
    }
}