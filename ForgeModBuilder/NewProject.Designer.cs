namespace ForgeModBuilder
{
    partial class NewProjectMenu
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
            this.MinecraftVersions = new System.Windows.Forms.ListBox();
            this.ForgeVersions = new System.Windows.Forms.ListBox();
            this.ProjectName = new System.Windows.Forms.TextBox();
            this.ProjectVersion = new System.Windows.Forms.TextBox();
            this.ProjectGroupName = new System.Windows.Forms.TextBox();
            this.JavaVersion = new System.Windows.Forms.ComboBox();
            this.CreateProjectButton = new System.Windows.Forms.Button();
            this.CancelSetupButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // MinecraftVersions
            // 
            this.MinecraftVersions.FormattingEnabled = true;
            this.MinecraftVersions.Location = new System.Drawing.Point(12, 12);
            this.MinecraftVersions.Name = "MinecraftVersions";
            this.MinecraftVersions.Size = new System.Drawing.Size(120, 147);
            this.MinecraftVersions.TabIndex = 0;
            this.MinecraftVersions.SelectedIndexChanged += new System.EventHandler(this.MinecraftVersions_SelectedIndexChanged);
            // 
            // ForgeVersions
            // 
            this.ForgeVersions.FormattingEnabled = true;
            this.ForgeVersions.Location = new System.Drawing.Point(152, 12);
            this.ForgeVersions.Name = "ForgeVersions";
            this.ForgeVersions.Size = new System.Drawing.Size(120, 147);
            this.ForgeVersions.TabIndex = 1;
            // 
            // ProjectName
            // 
            this.ProjectName.Location = new System.Drawing.Point(12, 165);
            this.ProjectName.Name = "ProjectName";
            this.ProjectName.Size = new System.Drawing.Size(120, 20);
            this.ProjectName.TabIndex = 2;
            this.ProjectName.Text = "Project Name";
            // 
            // ProjectVersion
            // 
            this.ProjectVersion.Location = new System.Drawing.Point(152, 165);
            this.ProjectVersion.Name = "ProjectVersion";
            this.ProjectVersion.Size = new System.Drawing.Size(120, 20);
            this.ProjectVersion.TabIndex = 3;
            this.ProjectVersion.Text = "Project Version";
            // 
            // ProjectGroupName
            // 
            this.ProjectGroupName.Location = new System.Drawing.Point(12, 191);
            this.ProjectGroupName.Name = "ProjectGroupName";
            this.ProjectGroupName.Size = new System.Drawing.Size(120, 20);
            this.ProjectGroupName.TabIndex = 4;
            this.ProjectGroupName.Text = "Project Group Name";
            // 
            // JavaVersion
            // 
            this.JavaVersion.FormattingEnabled = true;
            this.JavaVersion.Items.AddRange(new object[] {
            "1.8",
            "1.7",
            "1.6"});
            this.JavaVersion.Location = new System.Drawing.Point(152, 191);
            this.JavaVersion.Name = "JavaVersion";
            this.JavaVersion.Size = new System.Drawing.Size(120, 21);
            this.JavaVersion.TabIndex = 5;
            this.JavaVersion.Text = "Java Version";
            // 
            // CreateProjectButton
            // 
            this.CreateProjectButton.Location = new System.Drawing.Point(12, 217);
            this.CreateProjectButton.Name = "CreateProjectButton";
            this.CreateProjectButton.Size = new System.Drawing.Size(120, 32);
            this.CreateProjectButton.TabIndex = 6;
            this.CreateProjectButton.Text = "Create Project";
            this.CreateProjectButton.UseVisualStyleBackColor = true;
            this.CreateProjectButton.Click += new System.EventHandler(this.CreateProjectButton_Click);
            // 
            // CancelSetupButton
            // 
            this.CancelSetupButton.Location = new System.Drawing.Point(152, 218);
            this.CancelSetupButton.Name = "CancelSetupButton";
            this.CancelSetupButton.Size = new System.Drawing.Size(120, 32);
            this.CancelSetupButton.TabIndex = 7;
            this.CancelSetupButton.Text = "Cancel";
            this.CancelSetupButton.UseVisualStyleBackColor = true;
            // 
            // NewProjectMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 256);
            this.Controls.Add(this.CancelSetupButton);
            this.Controls.Add(this.CreateProjectButton);
            this.Controls.Add(this.JavaVersion);
            this.Controls.Add(this.ProjectGroupName);
            this.Controls.Add(this.ProjectVersion);
            this.Controls.Add(this.ProjectName);
            this.Controls.Add(this.ForgeVersions);
            this.Controls.Add(this.MinecraftVersions);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewProjectMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "New Project";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox MinecraftVersions;
        private System.Windows.Forms.ListBox ForgeVersions;
        private System.Windows.Forms.TextBox ProjectName;
        private System.Windows.Forms.TextBox ProjectVersion;
        private System.Windows.Forms.TextBox ProjectGroupName;
        private System.Windows.Forms.ComboBox JavaVersion;
        private System.Windows.Forms.Button CreateProjectButton;
        private System.Windows.Forms.Button CancelSetupButton;
    }
}