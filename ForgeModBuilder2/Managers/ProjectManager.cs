using ForgeModBuilder.Gradle;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace ForgeModBuilder.Managers
{
    public static class ProjectManager
    {
        public static List<Project> Projects { get; private set; } = new List<Project>();

        public static Project CurrentProject { get; set; }

        public static string ProjectsFileName { get; private set; } = "projects.json";

        public static void LoadProjects()
        {
            ClientManager.CreateCustomDataFileIfNotFound(ProjectsFileName);
            Projects = ClientManager.ReadCustomData<List<Project>>(ProjectsFileName);
            Dictionary<string, ListViewGroup> groups = new Dictionary<string, ListViewGroup>();
            foreach (Project project in Projects)
            {
                ListViewItem item;
                if (!string.IsNullOrEmpty(project.GroupName))
                {
                    if (groups.ContainsKey(project.GroupName))
                    {
                        item = new ListViewItem(project.ToArray());
                        item.Tag = project;
                        item.Group = groups[project.GroupName];
                        ForgeModBuilder.MainFormInstance.ProjectsListView.Items.Add(item);
                        continue;
                    }
                    else
                    {
                        ListViewGroup group = new ListViewGroup(project.GroupName);
                        ForgeModBuilder.MainFormInstance.ProjectsListView.Groups.Add(group);
                        item = new ListViewItem(project.ToArray());
                        item.Tag = project;
                        item.Group = group;
                        ForgeModBuilder.MainFormInstance.ProjectsListView.Items.Add(item);
                        groups.Add(project.GroupName, group);
                        continue;
                    }
                }
                item = new ListViewItem(project.ToArray());
                item.Tag = project;
                ForgeModBuilder.MainFormInstance.ProjectsListView.Items.Add(item);
            }
            ToolStripMenuItem menuItem;
            foreach (ListViewGroup group in groups.Values)
            {
                menuItem = new ToolStripMenuItem();
                menuItem.Text = group.Header;
                menuItem.Click += (sender, e) =>
                {
                    if (ForgeModBuilder.MainFormInstance.ProjectsListView.SelectedItems.Count > 0)
                    {
                        foreach (ListViewItem item in ForgeModBuilder.MainFormInstance.ProjectsListView.SelectedItems)
                        {
                            item.Group = group;
                        }
                    }
                };
                ForgeModBuilder.MainFormInstance.groupToolStripMenuItem.DropDownItems.Insert(0, menuItem);
                ForgeModBuilder.MainFormInstance.groupToolStripMenuItem1.DropDownItems.Insert(0, menuItem);
            }
            menuItem = new ToolStripMenuItem();
            menuItem.Text = "No group";
            menuItem.Click += (sender1, e1) =>
            {
                if (ForgeModBuilder.MainFormInstance.ProjectsListView.SelectedItems.Count > 0)
                {
                    foreach (ListViewItem item in ForgeModBuilder.MainFormInstance.ProjectsListView.SelectedItems)
                    {
                        item.Group = null;
                    }
                }
                bool allHaveNoGroup = true;
                foreach (ListViewItem item in ForgeModBuilder.MainFormInstance.ProjectsListView.Items)
                {
                    if (item.Group != null)
                    {
                        allHaveNoGroup = false;
                    }
                }
                if (allHaveNoGroup)
                {
                    ForgeModBuilder.MainFormInstance.ProjectsListView.Groups.Clear();
                    int count = ForgeModBuilder.MainFormInstance.groupToolStripMenuItem.DropDownItems.Count;
                    for(int i = 0; i < count; i++)
                    {
                        ToolStripMenuItem item = (ToolStripMenuItem) ForgeModBuilder.MainFormInstance.groupToolStripMenuItem.DropDownItems[0];
                        if (item.Tag != null && item.Tag is string && (string) item.Tag == "NewGroupButton")
                        {
                            continue;
                        }
                        ForgeModBuilder.MainFormInstance.groupToolStripMenuItem.DropDownItems.Remove(item);
                    }
                    count = ForgeModBuilder.MainFormInstance.groupToolStripMenuItem1.DropDownItems.Count;
                    for (int i = 0; i < count; i++)
                    {
                        ToolStripMenuItem item = (ToolStripMenuItem)ForgeModBuilder.MainFormInstance.groupToolStripMenuItem1.DropDownItems[0];
                        if (item.Tag != null && item.Tag is string && (string)item.Tag == "NewGroupButton")
                        {
                            continue;
                        }
                        ForgeModBuilder.MainFormInstance.groupToolStripMenuItem1.DropDownItems.Remove(item);
                    }
                }
            };
            if (ForgeModBuilder.MainFormInstance.groupToolStripMenuItem.DropDownItems.Count > 1)
            {
                // TODO localise
                ForgeModBuilder.MainFormInstance.groupToolStripMenuItem.DropDownItems.Insert(ForgeModBuilder.MainFormInstance.groupToolStripMenuItem.DropDownItems.Count - 1, new ToolStripSeparator());
                ForgeModBuilder.MainFormInstance.groupToolStripMenuItem.DropDownItems.Insert(ForgeModBuilder.MainFormInstance.groupToolStripMenuItem.DropDownItems.Count - 1, menuItem);
            }
            if (ForgeModBuilder.MainFormInstance.groupToolStripMenuItem1.DropDownItems.Count > 1)
            {
                // TODO localise
                ForgeModBuilder.MainFormInstance.groupToolStripMenuItem1.DropDownItems.Insert(ForgeModBuilder.MainFormInstance.groupToolStripMenuItem1.DropDownItems.Count - 1, new ToolStripSeparator());
                ForgeModBuilder.MainFormInstance.groupToolStripMenuItem1.DropDownItems.Insert(ForgeModBuilder.MainFormInstance.groupToolStripMenuItem1.DropDownItems.Count - 1, menuItem);
            }
            ForgeModBuilder.MainFormInstance.ProjectsListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        public static void SaveProjects()
        {
            foreach (ListViewItem item in ForgeModBuilder.MainFormInstance.ProjectsListView.Items)
            {
                foreach (Project project in Projects)
                {
                    if (((Project)item.Tag).Name == project.Name && ((Project)item.Tag).Path == project.Path)
                    {
                        project.Name = item.Text;
                        if (item.Group != null)
                        {
                            project.GroupName = item.Group.Header;
                        }
                        else
                        {
                            project.GroupName = string.Empty;
                        }
                        break;
                    }
                }
            }
            ClientManager.WriteCustomData<List<Project>>(Projects, ProjectsFileName);
        }

        public static void OpenProject()
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            // TODO Add description
            fbd.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                string[] pathFolders = fbd.SelectedPath.Split('\\');
                OpenProject(pathFolders[pathFolders.Length - 1], fbd.SelectedPath);
            }
        }

        public static Project GetProject(string Name, string Path)
        {
            foreach (Project project in Projects)
            {
                if (Name == project.Name || Path == project.Path)
                {
                    return project;
                }
            }
            return null;
        }

        public static bool ProjectExists(string Name, string Path)
        {
            return GetProject(Name, Path) != null;
        }

        public static void OpenProject(string Name, string Path)
        {
            // TODO ensure each variable is correct type
            // e.g. Try Catch
            if (!Path.EndsWith("\\"))
            {
                Path += "\\";
            }
            if(ProjectExists(Name, Path))
            {
                MessageBox.Show("This project is already open!", "Already open project!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Directory.Exists(Path) && File.Exists(Path + "gradlew.bat") && File.Exists(Path + "build.gradle"))
            {
                Project project = new Project(Name, Path);
                Projects.Add(project);
                if (ForgeModBuilder.MainFormInstance != null)
                {
                    ListViewItem item = new ListViewItem(project.ToArray());
                    item.Tag = project;
                    ForgeModBuilder.MainFormInstance.ProjectsListView.Items.Add(item);
                }
            }
            else
            {
                if (MessageBox.Show("Your project must have a build.gradle file and gradlew.bat file!", "Gradle not present!", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning) == DialogResult.Retry)
                {
                    OpenProject(Name, Path);
                }
            }
        }

        public static void UpdateProjectInfo(Project originalProject)
        {
            if (Directory.Exists(originalProject.Path) && File.Exists(originalProject.Path + "gradlew.bat") && File.Exists(originalProject.Path + "build.gradle"))
            {
                Project newProject = new Project(originalProject.Name, originalProject.Path);
                Projects.Remove(originalProject);
                Projects.Add(newProject);
            }
        }

    }

    public class Project
    {
        public string Name { get; set; }
        public string Path { get; private set; }
        public string MinecraftVersion { get; private set; }
        public string ForgeVersion { get; private set; }
        public string MCPMapping { get; private set; } = string.Empty;
        public bool HasMCPMapping { get; private set; }
        public string ModVersion { get; private set; }
        public string ModGroup { get; private set; }
        public string ModArchivesBaseName { get; private set; }
        public string GroupName { get; set; } = string.Empty;

        public Project(string name, string path)
        {
            Name = name;
            Path = path;
            GBlock file = GradleParser.ReadBuildFile(Path + "build.gradle");
            ModVersion = (string)file.SelectChild<GVariable>("version").Value;
            ModGroup = (string)file.SelectChild<GVariable>("group").Value;
            ModArchivesBaseName = (string)file.SelectChild<GVariable>("archivesBaseName").Value;
            string[] versionsDetails = GetVersionDetails((string)file.SelectChild<GBlock>("minecraft").SelectChild<GVariable>("version").Value);
            if (versionsDetails.Length != 2)
            {
                // this shouldn't happen! throw error
            }
            MinecraftVersion = versionsDetails[0];
            ForgeVersion = versionsDetails[1];
            if (file.SelectChild<GBlock>("minecraft").HasChild<GVariable>("mappings"))
            {
                HasMCPMapping = true;
                MCPMapping = (string)file.SelectChild<GBlock>("minecraft").SelectChild<GVariable>("mappings").Value;
            }
            else
            {
                HasMCPMapping = false;
            }
        }

        private static string[] GetVersionDetails(string data)
        {
            if (data.Contains("-"))
            {
                string[] versions = data.Split('-');
                if (versions.Length == 2)
                {
                    return versions;
                }
                else if (versions.Length == 3)
                {
                    return new string[] { versions[0], versions[1] };
                }
            }
            return new string[0];
        }

        public string[] ToArray()
        {
            return new string[] { Name, MinecraftVersion, ForgeVersion, MCPMapping, ModVersion, ModArchivesBaseName, ModGroup, Path};
        }
    }
}
