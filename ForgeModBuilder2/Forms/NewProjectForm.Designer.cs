namespace ForgeModBuilder.Forms
{
    partial class NewProjectForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewProjectForm));
            this.NewProjectPanel = new System.Windows.Forms.TableLayoutPanel();
            this.GroupTextBox = new System.Windows.Forms.TextBox();
            this.VersionTextBox = new System.Windows.Forms.TextBox();
            this.GeneratedArchiveNameLabel = new System.Windows.Forms.Label();
            this.ForgeVersionsListBox = new System.Windows.Forms.ListBox();
            this.MinecraftVerionsListBox = new System.Windows.Forms.ListBox();
            this.ExampleArchiveLabel = new System.Windows.Forms.Label();
            this.ArchivesBaseNameTextBox = new System.Windows.Forms.TextBox();
            this.JavaVersionComboBox = new System.Windows.Forms.ComboBox();
            this.ButtonsPanel = new System.Windows.Forms.Panel();
            this.CreateButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.NewProjectPanel.SuspendLayout();
            this.ButtonsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // NewProjectPanel
            // 
            this.NewProjectPanel.ColumnCount = 2;
            this.NewProjectPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.NewProjectPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.NewProjectPanel.Controls.Add(this.GroupTextBox, 0, 3);
            this.NewProjectPanel.Controls.Add(this.VersionTextBox, 1, 2);
            this.NewProjectPanel.Controls.Add(this.GeneratedArchiveNameLabel, 1, 1);
            this.NewProjectPanel.Controls.Add(this.ForgeVersionsListBox, 1, 0);
            this.NewProjectPanel.Controls.Add(this.MinecraftVerionsListBox, 0, 0);
            this.NewProjectPanel.Controls.Add(this.ExampleArchiveLabel, 0, 1);
            this.NewProjectPanel.Controls.Add(this.ArchivesBaseNameTextBox, 0, 2);
            this.NewProjectPanel.Controls.Add(this.JavaVersionComboBox, 1, 3);
            this.NewProjectPanel.Controls.Add(this.ButtonsPanel, 1, 4);
            this.NewProjectPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NewProjectPanel.Location = new System.Drawing.Point(0, 0);
            this.NewProjectPanel.Name = "NewProjectPanel";
            this.NewProjectPanel.Padding = new System.Windows.Forms.Padding(5);
            this.NewProjectPanel.RowCount = 5;
            this.NewProjectPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.NewProjectPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.NewProjectPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.NewProjectPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.NewProjectPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.NewProjectPanel.Size = new System.Drawing.Size(325, 272);
            this.NewProjectPanel.TabIndex = 0;
            // 
            // GroupTextBox
            // 
            this.GroupTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GroupTextBox.Location = new System.Drawing.Point(8, 213);
            this.GroupTextBox.Name = "GroupTextBox";
            this.GroupTextBox.Size = new System.Drawing.Size(151, 20);
            this.GroupTextBox.TabIndex = 5;
            this.GroupTextBox.Text = "Group";
            // 
            // VersionTextBox
            // 
            this.VersionTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VersionTextBox.Location = new System.Drawing.Point(165, 187);
            this.VersionTextBox.Name = "VersionTextBox";
            this.VersionTextBox.Size = new System.Drawing.Size(152, 20);
            this.VersionTextBox.TabIndex = 4;
            this.VersionTextBox.Text = "Version";
            // 
            // GeneratedArchiveNameLabel
            // 
            this.GeneratedArchiveNameLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.GeneratedArchiveNameLabel.AutoSize = true;
            this.GeneratedArchiveNameLabel.Location = new System.Drawing.Point(165, 165);
            this.GeneratedArchiveNameLabel.Name = "GeneratedArchiveNameLabel";
            this.GeneratedArchiveNameLabel.Size = new System.Drawing.Size(152, 13);
            this.GeneratedArchiveNameLabel.TabIndex = 14;
            this.GeneratedArchiveNameLabel.Text = "ArchivesBaseName-Version.jar";
            // 
            // ForgeVersionsListBox
            // 
            this.ForgeVersionsListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ForgeVersionsListBox.FormattingEnabled = true;
            this.ForgeVersionsListBox.Location = new System.Drawing.Point(165, 8);
            this.ForgeVersionsListBox.Name = "ForgeVersionsListBox";
            this.ForgeVersionsListBox.Size = new System.Drawing.Size(152, 148);
            this.ForgeVersionsListBox.TabIndex = 2;
            // 
            // MinecraftVerionsListBox
            // 
            this.MinecraftVerionsListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MinecraftVerionsListBox.FormattingEnabled = true;
            this.MinecraftVerionsListBox.Location = new System.Drawing.Point(8, 8);
            this.MinecraftVerionsListBox.Name = "MinecraftVerionsListBox";
            this.MinecraftVerionsListBox.Size = new System.Drawing.Size(151, 148);
            this.MinecraftVerionsListBox.TabIndex = 1;
            // 
            // ExampleArchiveLabel
            // 
            this.ExampleArchiveLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.ExampleArchiveLabel.AutoSize = true;
            this.ExampleArchiveLabel.Location = new System.Drawing.Point(70, 165);
            this.ExampleArchiveLabel.Name = "ExampleArchiveLabel";
            this.ExampleArchiveLabel.Size = new System.Drawing.Size(89, 13);
            this.ExampleArchiveLabel.TabIndex = 13;
            this.ExampleArchiveLabel.Text = "Example Archive:";
            // 
            // ArchivesBaseNameTextBox
            // 
            this.ArchivesBaseNameTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ArchivesBaseNameTextBox.Location = new System.Drawing.Point(8, 187);
            this.ArchivesBaseNameTextBox.Name = "ArchivesBaseNameTextBox";
            this.ArchivesBaseNameTextBox.Size = new System.Drawing.Size(151, 20);
            this.ArchivesBaseNameTextBox.TabIndex = 3;
            this.ArchivesBaseNameTextBox.Text = "ArchivesBaseName";
            // 
            // JavaVersionComboBox
            // 
            this.JavaVersionComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.JavaVersionComboBox.FormattingEnabled = true;
            this.JavaVersionComboBox.Items.AddRange(new object[] {
            "8",
            "7",
            "6"});
            this.JavaVersionComboBox.Location = new System.Drawing.Point(165, 213);
            this.JavaVersionComboBox.Name = "JavaVersionComboBox";
            this.JavaVersionComboBox.Size = new System.Drawing.Size(152, 21);
            this.JavaVersionComboBox.TabIndex = 6;
            this.JavaVersionComboBox.Text = "Java Version";
            // 
            // ButtonsPanel
            // 
            this.ButtonsPanel.Controls.Add(this.CreateButton);
            this.ButtonsPanel.Controls.Add(this.CancelButton);
            this.ButtonsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ButtonsPanel.Location = new System.Drawing.Point(165, 240);
            this.ButtonsPanel.Name = "ButtonsPanel";
            this.ButtonsPanel.Size = new System.Drawing.Size(152, 24);
            this.ButtonsPanel.TabIndex = 12;
            // 
            // CreateButton
            // 
            this.CreateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CreateButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.CreateButton.Enabled = false;
            this.CreateButton.Location = new System.Drawing.Point(3, 1);
            this.CreateButton.Name = "CreateButton";
            this.CreateButton.Size = new System.Drawing.Size(66, 23);
            this.CreateButton.TabIndex = 7;
            this.CreateButton.Text = "Create";
            this.CreateButton.UseVisualStyleBackColor = true;
            // 
            // CancelButton
            // 
            this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton.Location = new System.Drawing.Point(87, 1);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(66, 23);
            this.CancelButton.TabIndex = 8;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            // 
            // NewProjectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(325, 272);
            this.Controls.Add(this.NewProjectPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "NewProjectForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "New Project";
            this.NewProjectPanel.ResumeLayout(false);
            this.NewProjectPanel.PerformLayout();
            this.ButtonsPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel NewProjectPanel;
        private System.Windows.Forms.Label GeneratedArchiveNameLabel;
        private System.Windows.Forms.Panel ButtonsPanel;
        private System.Windows.Forms.Button CreateButton;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Label ExampleArchiveLabel;
        public System.Windows.Forms.ListBox ForgeVersionsListBox;
        public System.Windows.Forms.ListBox MinecraftVerionsListBox;
        public System.Windows.Forms.TextBox GroupTextBox;
        public System.Windows.Forms.TextBox VersionTextBox;
        public System.Windows.Forms.TextBox ArchivesBaseNameTextBox;
        public System.Windows.Forms.ComboBox JavaVersionComboBox;
    }
}