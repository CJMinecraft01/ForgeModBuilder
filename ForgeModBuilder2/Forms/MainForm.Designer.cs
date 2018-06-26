using ForgeModBuilder.Managers;

namespace ForgeModBuilder.Forms
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.MenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.projectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BottomPanel = new System.Windows.Forms.Panel();
            this.StatusLabel = new System.Windows.Forms.Label();
            this.LastConsoleMessageLabel = new System.Windows.Forms.Label();
            this.MainPanel = new System.Windows.Forms.TableLayoutPanel();
            this.TopPanel = new System.Windows.Forms.Panel();
            this.CentrePanel = new System.Windows.Forms.TableLayoutPanel();
            this.LeftTabControl = new System.Windows.Forms.TabControl();
            this.ProjectsTab = new System.Windows.Forms.TabPage();
            this.RightTabControl = new System.Windows.Forms.TabControl();
            this.ConsoleTab = new System.Windows.Forms.TabPage();
            this.ConsoleBottomPanel = new System.Windows.Forms.Panel();
            this.ExecuteCommandButton = new System.Windows.Forms.Button();
            this.CommandEntryTextBox = new System.Windows.Forms.TextBox();
            this.ConsoleTextBox = new System.Windows.Forms.TextBox();
            this.ProjectsListBox = new System.Windows.Forms.ListBox();
            this.NewProjectButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.MenuStrip.SuspendLayout();
            this.BottomPanel.SuspendLayout();
            this.MainPanel.SuspendLayout();
            this.TopPanel.SuspendLayout();
            this.CentrePanel.SuspendLayout();
            this.LeftTabControl.SuspendLayout();
            this.ProjectsTab.SuspendLayout();
            this.RightTabControl.SuspendLayout();
            this.ConsoleTab.SuspendLayout();
            this.ConsoleBottomPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // MenuStrip
            // 
            this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.projectToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.MenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip.Name = "MenuStrip";
            this.MenuStrip.Size = new System.Drawing.Size(734, 24);
            this.MenuStrip.TabIndex = 0;
            this.MenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // projectToolStripMenuItem
            // 
            this.projectToolStripMenuItem.Name = "projectToolStripMenuItem";
            this.projectToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.projectToolStripMenuItem.Text = "Project";
            // 
            // BottomPanel
            // 
            this.BottomPanel.Controls.Add(this.LastConsoleMessageLabel);
            this.BottomPanel.Controls.Add(this.StatusLabel);
            this.BottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BottomPanel.Location = new System.Drawing.Point(0, 536);
            this.BottomPanel.Name = "BottomPanel";
            this.BottomPanel.Size = new System.Drawing.Size(734, 25);
            this.BottomPanel.TabIndex = 1;
            // 
            // StatusLabel
            // 
            this.StatusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.StatusLabel.Location = new System.Drawing.Point(3, 6);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(100, 15);
            this.StatusLabel.TabIndex = 1;
            this.StatusLabel.Text = "Status";
            // 
            // LastConsoleMessageLabel
            // 
            this.LastConsoleMessageLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LastConsoleMessageLabel.AutoSize = true;
            this.LastConsoleMessageLabel.Location = new System.Drawing.Point(525, 6);
            this.LastConsoleMessageLabel.Name = "LastConsoleMessageLabel";
            this.LastConsoleMessageLabel.Size = new System.Drawing.Size(114, 13);
            this.LastConsoleMessageLabel.TabIndex = 2;
            this.LastConsoleMessageLabel.Text = "Last Console Message";
            // 
            // MainPanel
            // 
            this.MainPanel.ColumnCount = 1;
            this.MainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MainPanel.Controls.Add(this.CentrePanel, 0, 1);
            this.MainPanel.Controls.Add(this.TopPanel, 0, 0);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(0, 24);
            this.MainPanel.Margin = new System.Windows.Forms.Padding(0);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.RowCount = 2;
            this.MainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.MainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.MainPanel.Size = new System.Drawing.Size(734, 512);
            this.MainPanel.TabIndex = 3;
            // 
            // TopPanel
            // 
            this.TopPanel.Controls.Add(this.button4);
            this.TopPanel.Controls.Add(this.button3);
            this.TopPanel.Controls.Add(this.button2);
            this.TopPanel.Controls.Add(this.button1);
            this.TopPanel.Controls.Add(this.NewProjectButton);
            this.TopPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TopPanel.Location = new System.Drawing.Point(0, 0);
            this.TopPanel.Margin = new System.Windows.Forms.Padding(0);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new System.Drawing.Size(734, 50);
            this.TopPanel.TabIndex = 0;
            // 
            // CentrePanel
            // 
            this.CentrePanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.CentrePanel.ColumnCount = 2;
            this.CentrePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.CentrePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.CentrePanel.Controls.Add(this.LeftTabControl, 0, 0);
            this.CentrePanel.Controls.Add(this.RightTabControl, 1, 0);
            this.CentrePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CentrePanel.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.CentrePanel.Location = new System.Drawing.Point(0, 50);
            this.CentrePanel.Margin = new System.Windows.Forms.Padding(0);
            this.CentrePanel.Name = "CentrePanel";
            this.CentrePanel.RowCount = 1;
            this.CentrePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.CentrePanel.Size = new System.Drawing.Size(734, 462);
            this.CentrePanel.TabIndex = 3;
            // 
            // LeftTabControl
            // 
            this.LeftTabControl.Controls.Add(this.ProjectsTab);
            this.LeftTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LeftTabControl.Location = new System.Drawing.Point(4, 4);
            this.LeftTabControl.Name = "LeftTabControl";
            this.LeftTabControl.SelectedIndex = 0;
            this.LeftTabControl.Size = new System.Drawing.Size(213, 454);
            this.LeftTabControl.TabIndex = 0;
            // 
            // ProjectsTab
            // 
            this.ProjectsTab.Controls.Add(this.ProjectsListBox);
            this.ProjectsTab.Location = new System.Drawing.Point(4, 22);
            this.ProjectsTab.Name = "ProjectsTab";
            this.ProjectsTab.Padding = new System.Windows.Forms.Padding(3);
            this.ProjectsTab.Size = new System.Drawing.Size(205, 428);
            this.ProjectsTab.TabIndex = 0;
            this.ProjectsTab.Text = "Projects";
            this.ProjectsTab.UseVisualStyleBackColor = true;
            // 
            // RightTabControl
            // 
            this.RightTabControl.Controls.Add(this.ConsoleTab);
            this.RightTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RightTabControl.Location = new System.Drawing.Point(224, 4);
            this.RightTabControl.Name = "RightTabControl";
            this.RightTabControl.SelectedIndex = 0;
            this.RightTabControl.Size = new System.Drawing.Size(506, 454);
            this.RightTabControl.TabIndex = 1;
            // 
            // ConsoleTab
            // 
            this.ConsoleTab.Controls.Add(this.ConsoleTextBox);
            this.ConsoleTab.Controls.Add(this.ConsoleBottomPanel);
            this.ConsoleTab.Location = new System.Drawing.Point(4, 22);
            this.ConsoleTab.Name = "ConsoleTab";
            this.ConsoleTab.Padding = new System.Windows.Forms.Padding(3);
            this.ConsoleTab.Size = new System.Drawing.Size(498, 428);
            this.ConsoleTab.TabIndex = 0;
            this.ConsoleTab.Text = "Console";
            this.ConsoleTab.UseVisualStyleBackColor = true;
            // 
            // ConsoleBottomPanel
            // 
            this.ConsoleBottomPanel.Controls.Add(this.ExecuteCommandButton);
            this.ConsoleBottomPanel.Controls.Add(this.CommandEntryTextBox);
            this.ConsoleBottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ConsoleBottomPanel.Location = new System.Drawing.Point(3, 400);
            this.ConsoleBottomPanel.Margin = new System.Windows.Forms.Padding(0);
            this.ConsoleBottomPanel.Name = "ConsoleBottomPanel";
            this.ConsoleBottomPanel.Size = new System.Drawing.Size(492, 25);
            this.ConsoleBottomPanel.TabIndex = 0;
            // 
            // ExecuteCommandButton
            // 
            this.ExecuteCommandButton.Location = new System.Drawing.Point(417, 2);
            this.ExecuteCommandButton.Name = "ExecuteCommandButton";
            this.ExecuteCommandButton.Size = new System.Drawing.Size(75, 23);
            this.ExecuteCommandButton.TabIndex = 1;
            this.ExecuteCommandButton.Text = "Execute";
            this.ExecuteCommandButton.UseVisualStyleBackColor = true;
            // 
            // CommandEntryTextBox
            // 
            this.CommandEntryTextBox.Location = new System.Drawing.Point(0, 3);
            this.CommandEntryTextBox.Name = "CommandEntryTextBox";
            this.CommandEntryTextBox.Size = new System.Drawing.Size(411, 20);
            this.CommandEntryTextBox.TabIndex = 0;
            // 
            // ConsoleTextBox
            // 
            this.ConsoleTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ConsoleTextBox.Location = new System.Drawing.Point(3, 3);
            this.ConsoleTextBox.Multiline = true;
            this.ConsoleTextBox.Name = "ConsoleTextBox";
            this.ConsoleTextBox.ReadOnly = true;
            this.ConsoleTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.ConsoleTextBox.Size = new System.Drawing.Size(492, 397);
            this.ConsoleTextBox.TabIndex = 1;
            // 
            // ProjectsListBox
            // 
            this.ProjectsListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProjectsListBox.FormattingEnabled = true;
            this.ProjectsListBox.Items.AddRange(new object[] {
            "Industrial Tech 1.12",
            "CJCore 1.12"});
            this.ProjectsListBox.Location = new System.Drawing.Point(3, 3);
            this.ProjectsListBox.Name = "ProjectsListBox";
            this.ProjectsListBox.Size = new System.Drawing.Size(199, 422);
            this.ProjectsListBox.TabIndex = 0;
            // 
            // NewProjectButton
            // 
            this.NewProjectButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("NewProjectButton.BackgroundImage")));
            this.NewProjectButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.NewProjectButton.FlatAppearance.BorderSize = 0;
            this.NewProjectButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.NewProjectButton.Location = new System.Drawing.Point(0, 0);
            this.NewProjectButton.Margin = new System.Windows.Forms.Padding(0);
            this.NewProjectButton.Name = "NewProjectButton";
            this.NewProjectButton.Size = new System.Drawing.Size(48, 48);
            this.NewProjectButton.TabIndex = 0;
            this.NewProjectButton.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(48, 0);
            this.button1.Margin = new System.Windows.Forms.Padding(0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(48, 48);
            this.button1.TabIndex = 1;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button2.BackgroundImage")));
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(96, 0);
            this.button2.Margin = new System.Windows.Forms.Padding(0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(48, 48);
            this.button2.TabIndex = 2;
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button3.BackgroundImage")));
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Location = new System.Drawing.Point(144, 0);
            this.button3.Margin = new System.Windows.Forms.Padding(0);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(48, 48);
            this.button3.TabIndex = 3;
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button4.BackgroundImage")));
            this.button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Location = new System.Drawing.Point(192, 0);
            this.button4.Margin = new System.Windows.Forms.Padding(0);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(48, 48);
            this.button4.TabIndex = 4;
            this.button4.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 561);
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.BottomPanel);
            this.Controls.Add(this.MenuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Forge Mod Builder";
            this.MenuStrip.ResumeLayout(false);
            this.MenuStrip.PerformLayout();
            this.BottomPanel.ResumeLayout(false);
            this.BottomPanel.PerformLayout();
            this.MainPanel.ResumeLayout(false);
            this.TopPanel.ResumeLayout(false);
            this.CentrePanel.ResumeLayout(false);
            this.LeftTabControl.ResumeLayout(false);
            this.ProjectsTab.ResumeLayout(false);
            this.RightTabControl.ResumeLayout(false);
            this.ConsoleTab.ResumeLayout(false);
            this.ConsoleTab.PerformLayout();
            this.ConsoleBottomPanel.ResumeLayout(false);
            this.ConsoleBottomPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem projectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        public System.Windows.Forms.Label StatusLabel;
        public System.Windows.Forms.Panel BottomPanel;
        public System.Windows.Forms.Label LastConsoleMessageLabel;
        public System.Windows.Forms.TableLayoutPanel MainPanel;
        public System.Windows.Forms.TableLayoutPanel CentrePanel;
        private System.Windows.Forms.TabControl LeftTabControl;
        public System.Windows.Forms.TabPage ProjectsTab;
        private System.Windows.Forms.TabControl RightTabControl;
        private System.Windows.Forms.TabPage ConsoleTab;
        private System.Windows.Forms.Panel ConsoleBottomPanel;
        private System.Windows.Forms.Button ExecuteCommandButton;
        public System.Windows.Forms.TextBox CommandEntryTextBox;
        public System.Windows.Forms.Panel TopPanel;
        public System.Windows.Forms.TextBox ConsoleTextBox;
        private System.Windows.Forms.ListBox ProjectsListBox;
        private System.Windows.Forms.Button NewProjectButton;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button4;
    }
}

