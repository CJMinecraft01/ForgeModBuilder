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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.MinecraftVerionsListBox = new System.Windows.Forms.ListBox();
            this.ForgeVersionsListBox = new System.Windows.Forms.ListBox();
            this.CreateButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ExampleArchiveLabel = new System.Windows.Forms.Label();
            this.GeneratedArchiveNameLabel = new System.Windows.Forms.Label();
            this.ArchiveBaseNameTextBox = new System.Windows.Forms.TextBox();
            this.VersionTextBox = new System.Windows.Forms.TextBox();
            this.GroupTextBox = new System.Windows.Forms.TextBox();
            this.JavaVersionComboBox = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.GroupTextBox, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.VersionTextBox, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.GeneratedArchiveNameLabel, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.ForgeVersionsListBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.MinecraftVerionsListBox, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.ExampleArchiveLabel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.ArchiveBaseNameTextBox, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.JavaVersionComboBox, 1, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(5);
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(315, 278);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // MinecraftVerionsListBox
            // 
            this.MinecraftVerionsListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MinecraftVerionsListBox.FormattingEnabled = true;
            this.MinecraftVerionsListBox.Location = new System.Drawing.Point(8, 8);
            this.MinecraftVerionsListBox.Name = "MinecraftVerionsListBox";
            this.MinecraftVerionsListBox.Size = new System.Drawing.Size(146, 149);
            this.MinecraftVerionsListBox.TabIndex = 1;
            // 
            // ForgeVersionsListBox
            // 
            this.ForgeVersionsListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ForgeVersionsListBox.FormattingEnabled = true;
            this.ForgeVersionsListBox.Location = new System.Drawing.Point(160, 8);
            this.ForgeVersionsListBox.Name = "ForgeVersionsListBox";
            this.ForgeVersionsListBox.Size = new System.Drawing.Size(147, 149);
            this.ForgeVersionsListBox.TabIndex = 2;
            // 
            // CreateButton
            // 
            this.CreateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CreateButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.CreateButton.Enabled = false;
            this.CreateButton.Location = new System.Drawing.Point(3, 1);
            this.CreateButton.Name = "CreateButton";
            this.CreateButton.Size = new System.Drawing.Size(66, 23);
            this.CreateButton.TabIndex = 10;
            this.CreateButton.Text = "Create";
            this.CreateButton.UseVisualStyleBackColor = true;
            // 
            // CancelButton
            // 
            this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton.Location = new System.Drawing.Point(82, 1);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(66, 23);
            this.CancelButton.TabIndex = 11;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.CreateButton);
            this.panel1.Controls.Add(this.CancelButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(160, 246);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(147, 24);
            this.panel1.TabIndex = 12;
            // 
            // ExampleArchiveLabel
            // 
            this.ExampleArchiveLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.ExampleArchiveLabel.AutoSize = true;
            this.ExampleArchiveLabel.Location = new System.Drawing.Point(65, 168);
            this.ExampleArchiveLabel.Name = "ExampleArchiveLabel";
            this.ExampleArchiveLabel.Size = new System.Drawing.Size(89, 13);
            this.ExampleArchiveLabel.TabIndex = 13;
            this.ExampleArchiveLabel.Text = "Example Archive:";
            // 
            // GeneratedArchiveNameLabel
            // 
            this.GeneratedArchiveNameLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.GeneratedArchiveNameLabel.AutoSize = true;
            this.GeneratedArchiveNameLabel.Location = new System.Drawing.Point(160, 168);
            this.GeneratedArchiveNameLabel.Name = "GeneratedArchiveNameLabel";
            this.GeneratedArchiveNameLabel.Size = new System.Drawing.Size(147, 13);
            this.GeneratedArchiveNameLabel.TabIndex = 14;
            this.GeneratedArchiveNameLabel.Text = "ArchiveBaseName-Version.jar";
            // 
            // ArchiveBaseNameTextBox
            // 
            this.ArchiveBaseNameTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ArchiveBaseNameTextBox.Location = new System.Drawing.Point(8, 193);
            this.ArchiveBaseNameTextBox.Name = "ArchiveBaseNameTextBox";
            this.ArchiveBaseNameTextBox.Size = new System.Drawing.Size(146, 20);
            this.ArchiveBaseNameTextBox.TabIndex = 15;
            this.ArchiveBaseNameTextBox.Text = "ArchiveBaseName";
            // 
            // VersionTextBox
            // 
            this.VersionTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VersionTextBox.Location = new System.Drawing.Point(160, 193);
            this.VersionTextBox.Name = "VersionTextBox";
            this.VersionTextBox.Size = new System.Drawing.Size(147, 20);
            this.VersionTextBox.TabIndex = 16;
            this.VersionTextBox.Text = "Version";
            // 
            // GroupTextBox
            // 
            this.GroupTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GroupTextBox.Location = new System.Drawing.Point(8, 219);
            this.GroupTextBox.Name = "GroupTextBox";
            this.GroupTextBox.Size = new System.Drawing.Size(146, 20);
            this.GroupTextBox.TabIndex = 17;
            this.GroupTextBox.Text = "Group";
            // 
            // JavaVersionComboBox
            // 
            this.JavaVersionComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.JavaVersionComboBox.FormattingEnabled = true;
            this.JavaVersionComboBox.Location = new System.Drawing.Point(160, 219);
            this.JavaVersionComboBox.Name = "JavaVersionComboBox";
            this.JavaVersionComboBox.Size = new System.Drawing.Size(147, 21);
            this.JavaVersionComboBox.TabIndex = 18;
            this.JavaVersionComboBox.Text = "Java Version";
            // 
            // NewProjectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(315, 278);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "NewProjectForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "New Project";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ListBox ForgeVersionsListBox;
        private System.Windows.Forms.ListBox MinecraftVerionsListBox;
        private System.Windows.Forms.Label GeneratedArchiveNameLabel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button CreateButton;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Label ExampleArchiveLabel;
        private System.Windows.Forms.TextBox GroupTextBox;
        private System.Windows.Forms.TextBox VersionTextBox;
        private System.Windows.Forms.TextBox ArchiveBaseNameTextBox;
        private System.Windows.Forms.ComboBox JavaVersionComboBox;
    }
}