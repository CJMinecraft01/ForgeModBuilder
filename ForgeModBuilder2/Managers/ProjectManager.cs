using ForgeModBuilder.Forms;
using ForgeModBuilder.Gradle;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ForgeModBuilder.Managers
{
    public static class ProjectManager
    {
        public static string NewProjectDownloadURL { get; private set; } = "https://files.minecraftforge.net/maven/net/minecraftforge/forge/MC-FORGE/forge-MC-FORGE-SRC.zip";

        public static List<Project> Projects { get; private set; } = new List<Project>();

        public static Project CurrentProject { get; set; }

        public static string ProjectsFileName { get; private set; } = "projects.json";

        public static void LoadProjects()
        {
            ClientManager.CreateCustomDataFileIfNotFound(ProjectsFileName);
            Projects = ClientManager.ReadCustomData<List<Project>>(ProjectsFileName);
            Dictionary<string, ListViewGroup> groups = new Dictionary<string, ListViewGroup>();
            List<Project> UpdatedProjectList = new List<Project>();
            foreach (Project project in Projects)
            {
                if (File.Exists(project.Path + "build.gradle"))
                {
                    UpdatedProjectList.Add(project);
                }
                else
                {
                    continue;
                }
                ListViewItem item;
                if (!string.IsNullOrEmpty(project.GroupName))
                {
                    if (groups.ContainsKey(project.GroupName))
                    {
                        item = new ListViewItem(project.ToArray());
                        item.Tag = project;
                        item.Group = groups[project.GroupName];
                        ForgeModBuilder.MainFormInstance.ProjectsListView.Items.Add(item);
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
                    }
                    continue;
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
                    for (int i = 0; i < count; i++)
                    {
                        ToolStripMenuItem item = (ToolStripMenuItem)ForgeModBuilder.MainFormInstance.groupToolStripMenuItem.DropDownItems[0];
                        if (item.Tag != null && item.Tag is string && (string)item.Tag == "NewGroupButton")
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
            Projects = UpdatedProjectList;
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
            fbd.ShowNewFolderButton = false;
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                OpenProject(fbd.SelectedPath);
            }
        }

        public static void BuildProject()
        {
            GradleExecuter.RunGradleCommand("build");
            // option to add extra bits to the command
        }

        public static void NewProject()
        {
            NewProjectForm form1 = new NewProjectForm();
            if (form1.ShowDialog() != DialogResult.Cancel)
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                // add description
                if (fbd.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                if (Directory.EnumerateFileSystemEntries(fbd.SelectedPath).Any())
                {
                    // Make sure the folder is empty
                    // TODO localise
                    MessageBox.Show("The folder you selected has files in, please select another folder", "Invalid Folder", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                ProgressBarForm form2 = new ProgressBarForm();
                form2.ProgressBar.Maximum = 100;
                form2.TaskDetailsLabel.Text = LanguageManager.CurrentLanguage.Localize("form.new_project_download.label.task_details");
                form2.TitleLabel.Text = LanguageManager.CurrentLanguage.Localize("form.new_project_download.label.title");
                form2.Text = LanguageManager.CurrentLanguage.Localize("form.new_project_download.label.title");
                form2.CancelButton.Click += (sender, e) =>
                {
                    form2.Close();
                };
                Task<bool> task = DownloadNewProject(form1, form2, fbd.SelectedPath);
                if (form2.ShowDialog() == DialogResult.Cancel)
                {

                }
            }
        }

        private static async Task<bool> DownloadNewProject(NewProjectForm newProjectForm, ProgressBarForm progressBarForm, string path)
        {
            string url;
            if ((string)newProjectForm.MinecraftVerionsListBox.SelectedItem == "1.7.10")
            {
                url = NewProjectDownloadURL.Replace("MC", (string)newProjectForm.MinecraftVerionsListBox.SelectedItem).Replace("FORGE", ((string)newProjectForm.ForgeVersionsListBox.SelectedItem).Replace("★", string.Empty) + "-" + (string)newProjectForm.MinecraftVerionsListBox.SelectedItem);
            }
            else
            {
                url = NewProjectDownloadURL.Replace("MC", (string)newProjectForm.MinecraftVerionsListBox.SelectedItem).Replace("FORGE", ((string)newProjectForm.ForgeVersionsListBox.SelectedItem).Replace("★", string.Empty));
            }
            if (new Version((string)newProjectForm.MinecraftVerionsListBox.SelectedItem).CompareTo(new Version("1.8")) < 0)
            {
                url = url.Replace("SRC", "src");
            }
            else
            {
                url = url.Replace("SRC", "mdk");
            }

            WebClient client = new WebClient();
            client.DownloadProgressChanged += (sender, e) => progressBarForm.ProgressBar.Value = e.ProgressPercentage;
            DirectoryInfo ParentDirectory = Directory.GetParent(path);
            client.DownloadFileCompleted += (sender, e) =>
            {
                ZipFile.ExtractToDirectory(ParentDirectory.FullName + "\\temp.zip", path);

                File.Delete(ParentDirectory.FullName + "\\temp.zip");

                GBlock file = GradleParser.ReadFile(path + "\\build.gradle");
                file.SelectChild<GVariable>("version").Value = newProjectForm.VersionTextBox.Text;
                file.SelectChild<GVariable>("archivesBaseName").Value = newProjectForm.ArchivesBaseNameTextBox.Text;
                file.SelectChild<GVariable>("group").Value = newProjectForm.GroupTextBox.Text;
                file.SelectChild<GVariable>("targetCompatibility").Value = "1." + newProjectForm.JavaVersionComboBox.Text;
                file.SelectChild<GVariable>("sourceCompatibility").Value = "1." + newProjectForm.JavaVersionComboBox.Text;

                file.SelectChild<GBlock>("compileJava").SelectChild<GVariable>("sourceCompatibility").Value = "1." + newProjectForm.JavaVersionComboBox.Text;
                file.SelectChild<GBlock>("compileJava").SelectChild<GVariable>("targetCompatibility").Value = "1." + newProjectForm.JavaVersionComboBox.Text;

                GradleWriter.WriteFile(path + "\\build.gradle", file);

                OpenProject(path);

                progressBarForm.Close();
            };
            client.DownloadFileAsync(new Uri(url), ParentDirectory.FullName + "\\temp.zip");

            return true;
        }

        public static void UpdateProject(Project project)
        {
            GBlock file = GradleParser.ReadFile(project.Path + "build.gradle");
            if (new Version(project.ForgeVersion).CompareTo(new Version(ForgeVersionManager.ForgeVersions[project.MinecraftVersion].First())) < 0)
            {
                // Out of date forge version
                // Notify?
                Console.WriteLine("Out of date forge version, setting it to the latest!");
                GVariable versionVariable = file.SelectChild<GBlock>("minecraft").SelectChild<GVariable>("version");
                if (new Version(project.MinecraftVersion).CompareTo(new Version("1.8")) < 0)
                {
                    versionVariable.Value = project.MinecraftVersion + "-" + ForgeVersionManager.ForgeVersions[project.MinecraftVersion].First() + "-" + project.MinecraftVersion;
                }
                else
                {
                    versionVariable.Value = project.MinecraftVersion + "-" + ForgeVersionManager.ForgeVersions[project.MinecraftVersion].First();
                }
                project.ForgeVersion = ForgeVersionManager.ForgeVersions[project.MinecraftVersion].First();
            }
            if (project.HasMCPMapping)
            {
                // Use option to choose whether they want the latest stable version or snapshot
                // Will default to snapshot
                if (project.MCPMapping.Contains("snapshot"))
                {
                    int year, month, day;
                    if (int.TryParse(project.MCPMapping.Substring("snapshot_".Length, 4), out year) && int.TryParse(project.MCPMapping.Substring("snapshot_".Length + 4, 2), out month) && int.TryParse(project.MCPMapping.Substring("snapshot_".Length + 6, 2), out day))
                    {
                        DateTime projectSnapshotDate = new DateTime(year, month, day);
                        string mcversion = project.MinecraftVersion;
                        Version projectMC = new Version(project.MinecraftVersion);
                        bool foundCorrectMCVersion = false;
                        if (!ForgeVersionManager.MCPVersions.ContainsKey(mcversion))
                        {
                            foreach (string MinecraftVersion in ForgeVersionManager.MCPVersions.Keys)
                            {
                                Version version = new Version(MinecraftVersion);
                                if (version.Minor == projectMC.Minor && version.Major == projectMC.Major)
                                {
                                    mcversion = MinecraftVersion;
                                    foundCorrectMCVersion = true;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            foundCorrectMCVersion = true;
                        }
                        if (foundCorrectMCVersion)
                        {
                            string latestSnapshotDateString = ForgeVersionManager.MCPVersions[mcversion]["snapshot"].First();
                            if (int.TryParse(latestSnapshotDateString.Substring(0, 4), out year) && int.TryParse(latestSnapshotDateString.Substring(4, 2), out month) && int.TryParse(latestSnapshotDateString.Substring(6, 2), out day))
                            {
                                DateTime latestSnapshotDate = new DateTime(year, month, day);
                                if (projectSnapshotDate.CompareTo(latestSnapshotDate) < 0)
                                {
                                    Console.WriteLine("Out of date mcp mapping, setting it to the latest snapshot!");
                                    file.SelectChild<GBlock>("minecraft").SelectChild<GVariable>("mappings").Value = "snapshot_" + latestSnapshotDateString;
                                    project.MCPMapping = "snapshot_" + latestSnapshotDateString;
                                }
                            }
                        }
                    }
                }
                else
                {
                    string latestSnapshotDateString = ForgeVersionManager.MCPVersions[project.MinecraftVersion]["snapshot"].First();
                    file.SelectChild<GBlock>("minecraft").SelectChild<GVariable>("mappings").Value = "snapshot_" + latestSnapshotDateString;
                    project.MCPMapping = "snapshot_" + latestSnapshotDateString;
                }
            }
            GradleWriter.WriteFile(project.Path + "build.gradle", file);
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

        public static void OpenProject(string Path)
        {
            string[] pathFolders = Path.Split('\\');
            OpenProject(pathFolders[pathFolders.Length - 1], Path);
        }

        public static void OpenProject(string Name, string Path)
        {
            // TODO ensure each variable is correct type
            // e.g. Try Catch
            if (!Path.EndsWith("\\"))
            {
                Path += "\\";
            }
            if (ProjectExists(Name, Path))
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
                    ForgeModBuilder.MainFormInstance.ProjectsListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
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
            if (Directory.Exists(originalProject.Path.Substring(0, originalProject.Path.Length - 2)) && File.Exists(originalProject.Path + "gradlew.bat") && File.Exists(originalProject.Path + "build.gradle"))
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
        public string ForgeVersion { get; set; }
        public string MCPMapping { get; set; } = string.Empty;
        public bool HasMCPMapping { get; private set; }
        public string ModVersion { get; set; }
        public string ModGroup { get; set; }
        public string ModArchivesBaseName { get; set; }
        public string GroupName { get; set; } = string.Empty;

        public Project(string name, string path)
        {
            Name = name;
            Path = path;
            if (!File.Exists(Path + "build.gradle"))
            {
                return;
            }
            GBlock file = GradleParser.ReadFile(Path + "build.gradle");
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
            return new string[] { Name, MinecraftVersion, ForgeVersion, MCPMapping, ModVersion, ModArchivesBaseName, ModGroup, Path };
        }
    }
}
