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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.MenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.projectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buildToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BottomPanel = new System.Windows.Forms.Panel();
            this.LastConsoleMessageLabel = new System.Windows.Forms.Label();
            this.StatusLabel = new System.Windows.Forms.Label();
            this.MainPanel = new System.Windows.Forms.TableLayoutPanel();
            this.CentrePanel = new System.Windows.Forms.TableLayoutPanel();
            this.LeftTabControl = new System.Windows.Forms.TabControl();
            this.ProjectsTab = new System.Windows.Forms.TabPage();
            this.ProjectsListView = new System.Windows.Forms.ListView();
            this.ProjectName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.MinecraftVersion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ForgeVersion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.MCPMapping = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Version = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ArchiveBaseName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Group = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Path = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ProjectsContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.configureToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.updateToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.buildToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.renameToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.groupToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.newGroupToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.RightTabControl = new System.Windows.Forms.TabControl();
            this.ConsoleTab = new System.Windows.Forms.TabPage();
            this.ConsoleTextBoxPanel = new System.Windows.Forms.Panel();
            this.ConsoleTextBox = new System.Windows.Forms.RichTextBox();
            this.ConsoleBottomPanel = new System.Windows.Forms.Panel();
            this.ExecuteCommandButton = new System.Windows.Forms.Button();
            this.CommandEntryTextBox = new System.Windows.Forms.TextBox();
            this.TopPanel = new System.Windows.Forms.Panel();
            this.BuildProjectButton = new System.Windows.Forms.Button();
            this.OpenProjectButton = new System.Windows.Forms.Button();
            this.ConfigureProjectButton = new System.Windows.Forms.Button();
            this.UpdateProjectButton = new System.Windows.Forms.Button();
            this.NewProjectButton = new System.Windows.Forms.Button();
            this.MenuStrip.SuspendLayout();
            this.BottomPanel.SuspendLayout();
            this.MainPanel.SuspendLayout();
            this.CentrePanel.SuspendLayout();
            this.LeftTabControl.SuspendLayout();
            this.ProjectsTab.SuspendLayout();
            this.ProjectsContextMenuStrip.SuspendLayout();
            this.RightTabControl.SuspendLayout();
            this.ConsoleTab.SuspendLayout();
            this.ConsoleTextBoxPanel.SuspendLayout();
            this.ConsoleBottomPanel.SuspendLayout();
            this.TopPanel.SuspendLayout();
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
            // projectToolStripMenuItem
            // 
            this.projectToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.configureToolStripMenuItem,
            this.updateToolStripMenuItem,
            this.buildToolStripMenuItem,
            this.toolStripSeparator1,
            this.renameToolStripMenuItem,
            this.removeToolStripMenuItem,
            this.groupToolStripMenuItem});
            this.projectToolStripMenuItem.Name = "projectToolStripMenuItem";
            this.projectToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.projectToolStripMenuItem.Text = "Project";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.newToolStripMenuItem.Text = "New";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.openToolStripMenuItem.Text = "Open";
            // 
            // configureToolStripMenuItem
            // 
            this.configureToolStripMenuItem.Name = "configureToolStripMenuItem";
            this.configureToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.configureToolStripMenuItem.Text = "Configure";
            // 
            // updateToolStripMenuItem
            // 
            this.updateToolStripMenuItem.Name = "updateToolStripMenuItem";
            this.updateToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.updateToolStripMenuItem.Text = "Update";
            // 
            // buildToolStripMenuItem
            // 
            this.buildToolStripMenuItem.Name = "buildToolStripMenuItem";
            this.buildToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.buildToolStripMenuItem.Text = "Build";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(124, 6);
            // 
            // renameToolStripMenuItem
            // 
            this.renameToolStripMenuItem.Enabled = false;
            this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            this.renameToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.renameToolStripMenuItem.Text = "Rename";
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Enabled = false;
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.removeToolStripMenuItem.Text = "Remove";
            // 
            // groupToolStripMenuItem
            // 
            this.groupToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newGroupToolStripMenuItem});
            this.groupToolStripMenuItem.Enabled = false;
            this.groupToolStripMenuItem.Name = "groupToolStripMenuItem";
            this.groupToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.groupToolStripMenuItem.Text = "Group";
            // 
            // newGroupToolStripMenuItem
            // 
            this.newGroupToolStripMenuItem.Name = "newGroupToolStripMenuItem";
            this.newGroupToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.newGroupToolStripMenuItem.Tag = "NewGroupButton";
            this.newGroupToolStripMenuItem.Text = "New Group...";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.optionsToolStripMenuItem.Text = "Options";
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
            // LastConsoleMessageLabel
            // 
            this.LastConsoleMessageLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LastConsoleMessageLabel.AutoSize = true;
            this.LastConsoleMessageLabel.Location = new System.Drawing.Point(608, 6);
            this.LastConsoleMessageLabel.Name = "LastConsoleMessageLabel";
            this.LastConsoleMessageLabel.Size = new System.Drawing.Size(114, 13);
            this.LastConsoleMessageLabel.TabIndex = 2;
            this.LastConsoleMessageLabel.Text = "Last Console Message";
            this.LastConsoleMessageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            // CentrePanel
            // 
            this.CentrePanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.CentrePanel.ColumnCount = 2;
            this.CentrePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.78854F));
            this.CentrePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65.21146F));
            this.CentrePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
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
            this.LeftTabControl.Size = new System.Drawing.Size(248, 454);
            this.LeftTabControl.TabIndex = 0;
            // 
            // ProjectsTab
            // 
            this.ProjectsTab.Controls.Add(this.ProjectsListView);
            this.ProjectsTab.Location = new System.Drawing.Point(4, 22);
            this.ProjectsTab.Name = "ProjectsTab";
            this.ProjectsTab.Padding = new System.Windows.Forms.Padding(3);
            this.ProjectsTab.Size = new System.Drawing.Size(240, 428);
            this.ProjectsTab.TabIndex = 0;
            this.ProjectsTab.Text = "Projects";
            this.ProjectsTab.UseVisualStyleBackColor = true;
            // 
            // ProjectsListView
            // 
            this.ProjectsListView.AllowDrop = true;
            this.ProjectsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ProjectName,
            this.MinecraftVersion,
            this.ForgeVersion,
            this.MCPMapping,
            this.Version,
            this.ArchiveBaseName,
            this.Group,
            this.Path});
            this.ProjectsListView.ContextMenuStrip = this.ProjectsContextMenuStrip;
            this.ProjectsListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProjectsListView.LabelEdit = true;
            this.ProjectsListView.Location = new System.Drawing.Point(3, 3);
            this.ProjectsListView.Name = "ProjectsListView";
            this.ProjectsListView.ShowItemToolTips = true;
            this.ProjectsListView.Size = new System.Drawing.Size(234, 422);
            this.ProjectsListView.TabIndex = 0;
            this.ProjectsListView.UseCompatibleStateImageBehavior = false;
            this.ProjectsListView.View = System.Windows.Forms.View.Details;
            // 
            // ProjectName
            // 
            this.ProjectName.Text = "Name";
            this.ProjectName.Width = 46;
            // 
            // MinecraftVersion
            // 
            this.MinecraftVersion.Text = "Minecraft Version";
            this.MinecraftVersion.Width = 98;
            // 
            // ForgeVersion
            // 
            this.ForgeVersion.Text = "ForgeVersion";
            this.ForgeVersion.Width = 75;
            // 
            // MCPMapping
            // 
            this.MCPMapping.Text = "MCP Mapping";
            // 
            // Version
            // 
            this.Version.Text = "Version";
            // 
            // ArchiveBaseName
            // 
            this.ArchiveBaseName.Text = "Archive Base Name";
            // 
            // Group
            // 
            this.Group.Text = "Group";
            // 
            // Path
            // 
            this.Path.Text = "Path";
            // 
            // ProjectsContextMenuStrip
            // 
            this.ProjectsContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem1,
            this.openToolStripMenuItem1,
            this.configureToolStripMenuItem1,
            this.updateToolStripMenuItem1,
            this.buildToolStripMenuItem1,
            this.toolStripSeparator2,
            this.renameToolStripMenuItem1,
            this.removeToolStripMenuItem1,
            this.groupToolStripMenuItem1});
            this.ProjectsContextMenuStrip.Name = "ProjectsContextMenuStrip";
            this.ProjectsContextMenuStrip.Size = new System.Drawing.Size(128, 186);
            // 
            // newToolStripMenuItem1
            // 
            this.newToolStripMenuItem1.Name = "newToolStripMenuItem1";
            this.newToolStripMenuItem1.Size = new System.Drawing.Size(127, 22);
            this.newToolStripMenuItem1.Text = "New";
            // 
            // openToolStripMenuItem1
            // 
            this.openToolStripMenuItem1.Name = "openToolStripMenuItem1";
            this.openToolStripMenuItem1.Size = new System.Drawing.Size(127, 22);
            this.openToolStripMenuItem1.Text = "Open";
            // 
            // configureToolStripMenuItem1
            // 
            this.configureToolStripMenuItem1.Name = "configureToolStripMenuItem1";
            this.configureToolStripMenuItem1.Size = new System.Drawing.Size(127, 22);
            this.configureToolStripMenuItem1.Text = "Configure";
            // 
            // updateToolStripMenuItem1
            // 
            this.updateToolStripMenuItem1.Name = "updateToolStripMenuItem1";
            this.updateToolStripMenuItem1.Size = new System.Drawing.Size(127, 22);
            this.updateToolStripMenuItem1.Text = "Update";
            // 
            // buildToolStripMenuItem1
            // 
            this.buildToolStripMenuItem1.Name = "buildToolStripMenuItem1";
            this.buildToolStripMenuItem1.Size = new System.Drawing.Size(127, 22);
            this.buildToolStripMenuItem1.Text = "Build";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(124, 6);
            // 
            // renameToolStripMenuItem1
            // 
            this.renameToolStripMenuItem1.Enabled = false;
            this.renameToolStripMenuItem1.Name = "renameToolStripMenuItem1";
            this.renameToolStripMenuItem1.Size = new System.Drawing.Size(127, 22);
            this.renameToolStripMenuItem1.Text = "Rename";
            // 
            // removeToolStripMenuItem1
            // 
            this.removeToolStripMenuItem1.Enabled = false;
            this.removeToolStripMenuItem1.Name = "removeToolStripMenuItem1";
            this.removeToolStripMenuItem1.Size = new System.Drawing.Size(127, 22);
            this.removeToolStripMenuItem1.Text = "Remove";
            // 
            // groupToolStripMenuItem1
            // 
            this.groupToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newGroupToolStripMenuItem1});
            this.groupToolStripMenuItem1.Enabled = false;
            this.groupToolStripMenuItem1.Name = "groupToolStripMenuItem1";
            this.groupToolStripMenuItem1.Size = new System.Drawing.Size(127, 22);
            this.groupToolStripMenuItem1.Text = "Group";
            // 
            // newGroupToolStripMenuItem1
            // 
            this.newGroupToolStripMenuItem1.Name = "newGroupToolStripMenuItem1";
            this.newGroupToolStripMenuItem1.Size = new System.Drawing.Size(143, 22);
            this.newGroupToolStripMenuItem1.Tag = "NewGroupButton";
            this.newGroupToolStripMenuItem1.Text = "New Group...";
            // 
            // RightTabControl
            // 
            this.RightTabControl.Controls.Add(this.ConsoleTab);
            this.RightTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RightTabControl.Location = new System.Drawing.Point(259, 4);
            this.RightTabControl.Name = "RightTabControl";
            this.RightTabControl.SelectedIndex = 0;
            this.RightTabControl.Size = new System.Drawing.Size(471, 454);
            this.RightTabControl.TabIndex = 1;
            // 
            // ConsoleTab
            // 
            this.ConsoleTab.Controls.Add(this.ConsoleTextBoxPanel);
            this.ConsoleTab.Controls.Add(this.ConsoleBottomPanel);
            this.ConsoleTab.Location = new System.Drawing.Point(4, 22);
            this.ConsoleTab.Name = "ConsoleTab";
            this.ConsoleTab.Padding = new System.Windows.Forms.Padding(3);
            this.ConsoleTab.Size = new System.Drawing.Size(463, 428);
            this.ConsoleTab.TabIndex = 0;
            this.ConsoleTab.Text = "Console";
            this.ConsoleTab.UseVisualStyleBackColor = true;
            // 
            // ConsoleTextBoxPanel
            // 
            this.ConsoleTextBoxPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ConsoleTextBoxPanel.Controls.Add(this.ConsoleTextBox);
            this.ConsoleTextBoxPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ConsoleTextBoxPanel.Location = new System.Drawing.Point(3, 3);
            this.ConsoleTextBoxPanel.Name = "ConsoleTextBoxPanel";
            this.ConsoleTextBoxPanel.Size = new System.Drawing.Size(457, 397);
            this.ConsoleTextBoxPanel.TabIndex = 2;
            // 
            // ConsoleTextBox
            // 
            this.ConsoleTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ConsoleTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ConsoleTextBox.Location = new System.Drawing.Point(0, 0);
            this.ConsoleTextBox.Name = "ConsoleTextBox";
            this.ConsoleTextBox.ReadOnly = true;
            this.ConsoleTextBox.Size = new System.Drawing.Size(455, 395);
            this.ConsoleTextBox.TabIndex = 2;
            this.ConsoleTextBox.Text = "";
            // 
            // ConsoleBottomPanel
            // 
            this.ConsoleBottomPanel.Controls.Add(this.ExecuteCommandButton);
            this.ConsoleBottomPanel.Controls.Add(this.CommandEntryTextBox);
            this.ConsoleBottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ConsoleBottomPanel.Location = new System.Drawing.Point(3, 400);
            this.ConsoleBottomPanel.Margin = new System.Windows.Forms.Padding(0);
            this.ConsoleBottomPanel.Name = "ConsoleBottomPanel";
            this.ConsoleBottomPanel.Size = new System.Drawing.Size(457, 25);
            this.ConsoleBottomPanel.TabIndex = 0;
            // 
            // ExecuteCommandButton
            // 
            this.ExecuteCommandButton.Location = new System.Drawing.Point(379, 2);
            this.ExecuteCommandButton.Name = "ExecuteCommandButton";
            this.ExecuteCommandButton.Size = new System.Drawing.Size(75, 22);
            this.ExecuteCommandButton.TabIndex = 1;
            this.ExecuteCommandButton.Text = "Execute";
            this.ExecuteCommandButton.UseVisualStyleBackColor = true;
            // 
            // CommandEntryTextBox
            // 
            this.CommandEntryTextBox.Location = new System.Drawing.Point(0, 3);
            this.CommandEntryTextBox.Name = "CommandEntryTextBox";
            this.CommandEntryTextBox.Size = new System.Drawing.Size(373, 20);
            this.CommandEntryTextBox.TabIndex = 0;
            // 
            // TopPanel
            // 
            this.TopPanel.Controls.Add(this.BuildProjectButton);
            this.TopPanel.Controls.Add(this.OpenProjectButton);
            this.TopPanel.Controls.Add(this.ConfigureProjectButton);
            this.TopPanel.Controls.Add(this.UpdateProjectButton);
            this.TopPanel.Controls.Add(this.NewProjectButton);
            this.TopPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TopPanel.Location = new System.Drawing.Point(0, 0);
            this.TopPanel.Margin = new System.Windows.Forms.Padding(0);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new System.Drawing.Size(734, 50);
            this.TopPanel.TabIndex = 0;
            // 
            // BuildProjectButton
            // 
            this.BuildProjectButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BuildProjectButton.BackgroundImage")));
            this.BuildProjectButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.BuildProjectButton.FlatAppearance.BorderSize = 0;
            this.BuildProjectButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BuildProjectButton.Location = new System.Drawing.Point(192, 0);
            this.BuildProjectButton.Margin = new System.Windows.Forms.Padding(0);
            this.BuildProjectButton.Name = "BuildProjectButton";
            this.BuildProjectButton.Size = new System.Drawing.Size(48, 48);
            this.BuildProjectButton.TabIndex = 4;
            this.BuildProjectButton.UseVisualStyleBackColor = true;
            // 
            // OpenProjectButton
            // 
            this.OpenProjectButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("OpenProjectButton.BackgroundImage")));
            this.OpenProjectButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.OpenProjectButton.FlatAppearance.BorderSize = 0;
            this.OpenProjectButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OpenProjectButton.Location = new System.Drawing.Point(48, 0);
            this.OpenProjectButton.Margin = new System.Windows.Forms.Padding(0);
            this.OpenProjectButton.Name = "OpenProjectButton";
            this.OpenProjectButton.Size = new System.Drawing.Size(48, 48);
            this.OpenProjectButton.TabIndex = 3;
            this.OpenProjectButton.UseVisualStyleBackColor = true;
            // 
            // ConfigureProjectButton
            // 
            this.ConfigureProjectButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ConfigureProjectButton.BackgroundImage")));
            this.ConfigureProjectButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ConfigureProjectButton.FlatAppearance.BorderSize = 0;
            this.ConfigureProjectButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ConfigureProjectButton.Location = new System.Drawing.Point(96, 0);
            this.ConfigureProjectButton.Margin = new System.Windows.Forms.Padding(0);
            this.ConfigureProjectButton.Name = "ConfigureProjectButton";
            this.ConfigureProjectButton.Size = new System.Drawing.Size(48, 48);
            this.ConfigureProjectButton.TabIndex = 2;
            this.ConfigureProjectButton.UseVisualStyleBackColor = true;
            // 
            // UpdateProjectButton
            // 
            this.UpdateProjectButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("UpdateProjectButton.BackgroundImage")));
            this.UpdateProjectButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.UpdateProjectButton.FlatAppearance.BorderSize = 0;
            this.UpdateProjectButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UpdateProjectButton.Location = new System.Drawing.Point(144, 0);
            this.UpdateProjectButton.Margin = new System.Windows.Forms.Padding(0);
            this.UpdateProjectButton.Name = "UpdateProjectButton";
            this.UpdateProjectButton.Size = new System.Drawing.Size(48, 48);
            this.UpdateProjectButton.TabIndex = 1;
            this.UpdateProjectButton.UseVisualStyleBackColor = true;
            // 
            // NewProjectButton
            // 
            this.NewProjectButton.BackColor = System.Drawing.SystemColors.Control;
            this.NewProjectButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("NewProjectButton.BackgroundImage")));
            this.NewProjectButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.NewProjectButton.FlatAppearance.BorderSize = 0;
            this.NewProjectButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.NewProjectButton.Location = new System.Drawing.Point(0, 0);
            this.NewProjectButton.Margin = new System.Windows.Forms.Padding(0);
            this.NewProjectButton.Name = "NewProjectButton";
            this.NewProjectButton.Size = new System.Drawing.Size(48, 48);
            this.NewProjectButton.TabIndex = 0;
            this.NewProjectButton.UseVisualStyleBackColor = false;
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
            this.MinimumSize = new System.Drawing.Size(750, 600);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Forge Mod Builder";
            this.MenuStrip.ResumeLayout(false);
            this.MenuStrip.PerformLayout();
            this.BottomPanel.ResumeLayout(false);
            this.BottomPanel.PerformLayout();
            this.MainPanel.ResumeLayout(false);
            this.CentrePanel.ResumeLayout(false);
            this.LeftTabControl.ResumeLayout(false);
            this.ProjectsTab.ResumeLayout(false);
            this.ProjectsContextMenuStrip.ResumeLayout(false);
            this.RightTabControl.ResumeLayout(false);
            this.ConsoleTab.ResumeLayout(false);
            this.ConsoleTextBoxPanel.ResumeLayout(false);
            this.ConsoleBottomPanel.ResumeLayout(false);
            this.ConsoleBottomPanel.PerformLayout();
            this.TopPanel.ResumeLayout(false);
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
        public System.Windows.Forms.TextBox CommandEntryTextBox;
        public System.Windows.Forms.Panel TopPanel;
        public System.Windows.Forms.Button ConfigureProjectButton;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem buildToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        public System.Windows.Forms.Panel ConsoleTextBoxPanel;
        public System.Windows.Forms.RichTextBox ConsoleTextBox;
        public System.Windows.Forms.Button NewProjectButton;
        public System.Windows.Forms.Button OpenProjectButton;
        public System.Windows.Forms.Button UpdateProjectButton;
        public System.Windows.Forms.Button BuildProjectButton;
        public System.Windows.Forms.Button ExecuteCommandButton;
        public System.Windows.Forms.ListView ProjectsListView;
        private System.Windows.Forms.ColumnHeader ProjectName;
        private System.Windows.Forms.ColumnHeader Path;
        private System.Windows.Forms.ColumnHeader MinecraftVersion;
        private System.Windows.Forms.ColumnHeader ForgeVersion;
        private System.Windows.Forms.ColumnHeader MCPMapping;
        private System.Windows.Forms.ColumnHeader Version;
        private System.Windows.Forms.ColumnHeader ArchiveBaseName;
        private System.Windows.Forms.ColumnHeader Group;
        private System.Windows.Forms.ContextMenuStrip ProjectsContextMenuStrip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem configureToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem updateToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem buildToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem1;
        public System.Windows.Forms.ToolStripMenuItem groupToolStripMenuItem1;
        public System.Windows.Forms.ToolStripMenuItem groupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newGroupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newGroupToolStripMenuItem1;
    }
}

