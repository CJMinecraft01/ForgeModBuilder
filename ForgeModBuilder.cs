using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ForgeModBuilder
{
    public partial class FMB : Form
    {
        //The current project being used
        public Project CurrentProject;

        //The path to the projects.json file 
        private static string ProjectsFilePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/ForgeModBuilder/projects.json";
        //The path to the options.json file
        private static string OptionsFilePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/ForgeModBuilder/options.json";

        //The available projects loaded from the projects.json file
        private List<Project> Projects = new List<Project>();
        //The current options loaded from the options.json file
        private Dictionary<string, object> Options = new Dictionary<string, object>();

        public FMB()
        {
            InitializeComponent(); //Create the default form stuff
            Resize += ResizeWindow; //To allow for window resizing
            SetupLayout(); //Setup the layout of the form
            UpdateProjects(false, true); //Receive the projects from the projects.json file
            UpdateOptions(false, true); //Receive the options from the options.json file
            FormClosed += FMBClosed; //Handle when the form closes
            Load += (sender, e) =>
            {
                UpdateChecker.CheckForUpdates(UpdateChecker.UpdateURL); //When it loads the form, check for updates
            };
        }

        //Save the projects.json and options.json file when closing
        private void FMBClosed(object sender, FormClosedEventArgs e)
        {
            UpdateProjects(true, false); //Save the projects
            UpdateOptions(true, false); //Save the user's settings
        }

        #region Client Only Data
        //Update the options in the file or in the variable
        public void UpdateOptions(bool saveFile, bool readFromFile)
        {
            if(saveFile) //If you want to save
            {
                if(File.Exists(OptionsFilePath)) //Make sure you have the options file
                {
                    JsonSerializer js = new JsonSerializer();
                    js.NullValueHandling = NullValueHandling.Ignore;
                    using (StreamWriter sw = new StreamWriter(OptionsFilePath))
                    using (JsonWriter jw = new JsonTextWriter(sw))
                    {
                        js.Serialize(jw, Options); //Write the data in Options to the file
                    }
                }
                else //Create the options file
                {
                    if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/ForgeModBuilder"))
                    {
                        Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/ForgeModBuilder"); //Create the path if not found
                    }
                    File.Create(OptionsFilePath).Dispose();
                    UpdateOptions(saveFile, readFromFile); //Recall the method now that it has the file
                }
            }
            if (readFromFile) //Read the variables from the file
            {
                if (File.Exists(OptionsFilePath)) //If the options file exists
                {
                    JsonSerializer js = new JsonSerializer();
                    js.NullValueHandling = NullValueHandling.Ignore;
                    using (StreamReader sr = new StreamReader(OptionsFilePath))
                    using (JsonReader jr = new JsonTextReader(sr))
                    {
                        Options = js.Deserialize<Dictionary<string, object>>(jr); //Deserialise the file and place it in the options variable
                    }
                    if (Options == null) //If the file was empty
                    {
                        Options = new Dictionary<string, object>(); //Still initialise it but to a blank version
                    }
                }
                else //Create the options file
                {
                    if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/ForgeModBuilder"))
                    {
                        Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/ForgeModBuilder"); //Create the directory if it can't find it
                    }
                    File.Create(OptionsFilePath).Dispose();
                    UpdateOptions(saveFile, readFromFile); //Recall the method now that it has the file
                }
            }

            if (Options.ContainsKey("console_font")) //Deserialise the console font
            {
                if (Options["console_font"] is string) //If it is a string
                {
                    Console.Font = JsonConvert.DeserializeObject<Font>("\"" + (string)Options["console_font"] + "\""); //Load the console font as a string
                }
                else
                {
                    Console.Font = (Font)Options["console_font"]; //It must be a font so we can load it directly
                }
            }
        }

        //Update the projects in the file or in the variable
        public void UpdateProjects(bool saveFile, bool readFromFile)
        {
            if(saveFile) //If you want to save
            {
                if(File.Exists(ProjectsFilePath)) //Make sure the file exists
                {
                    JsonSerializer js = new JsonSerializer();
                    js.NullValueHandling = NullValueHandling.Ignore;
                    using (StreamWriter sw = new StreamWriter(ProjectsFilePath))
                    using (JsonWriter jw = new JsonTextWriter(sw))
                    {
                        js.Serialize(jw, Projects); //Write all the data in Projects to the file
                    }
                }
                else //The file does not exist so create it
                {
                    if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/ForgeModBuilder")) //Does the folder not exist?
                    {
                        Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/ForgeModBuilder"); //Create the folder
                    }
                    File.Create(ProjectsFilePath).Dispose();
                    UpdateProjects(saveFile, readFromFile); //Recall the method now that it can find the file
                }
            }
            if(readFromFile) //If you want to read data from the file
            {
                if(File.Exists(ProjectsFilePath)) //Make srue the file exists
                {
                    JsonSerializer js = new JsonSerializer();
                    js.NullValueHandling = NullValueHandling.Ignore;
                    using (StreamReader sr = new StreamReader(ProjectsFilePath))
                    using(JsonReader jr = new JsonTextReader(sr))
                    {
                        Projects = js.Deserialize<List<Project>>(jr); //Read the projects
                    }
                    if(Projects == null)
                    {
                        Projects = new List<Project>(); //If the file was empty, initialise Projects to a blank list
                    }
                }
                else //If the file did not exist
                {
                    if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/ForgeModBuilder")) //Does the folder not exist?
                    {
                        Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/ForgeModBuilder"); //Create the folder
                    }
                    File.Create(ProjectsFilePath).Dispose();
                    UpdateProjects(saveFile, readFromFile); //Recall the method now that it can find the file
                }
            }

            //Clear all the menu items which allow you to load that project and run the action as well
            OpenProjectMenuItem.DropDownItems.Clear();
            BuildProjectMenuItem.DropDownItems.Clear();
            SetupProjectMenuItem.DropDownItems.Clear();
            UpdateProjectMenuItem.DropDownItems.Clear();
            ChangeProjectVersionMenuItem.DropDownItems.Clear();
            RefreshProjectMenuItem.DropDownItems.Clear();
            foreach (Project p in Projects) //Add a dropdown for each project
            {
                ToolStripMenuItem i = new ToolStripMenuItem(p.path);
                i.Click += (sender, e) =>
                {
                    if(CurrentProject != p)
                        OpenProject(p.path); //Make it open the current project if it is not already open
                };
                OpenProjectMenuItem.DropDownItems.Add(i);
                i = new ToolStripMenuItem(p.path);
                i.Click += (sender, e) =>
                {
                    if (CurrentProject != p)
                    {
                        OpenProject(p.path); //Make it open the current project if it is not already open
                    }
                    BuildProject();
                };
                BuildProjectMenuItem.DropDownItems.Add(i);
                i = new ToolStripMenuItem(p.path);
                i.Click += (sender, e) =>
                {
                    if (CurrentProject != p)
                    {
                        OpenProject(p.path); //Make it open the current project if it is not already open
                    }
                    SetupProject("");
                };
                SetupProjectMenuItem.DropDownItems.Add(i);
                i = new ToolStripMenuItem(p.path);
                i.Click += (sender, e) =>
                {
                    if (CurrentProject != p)
                    {
                        OpenProject(p.path); //Make it open the current project if it is not already open
                    }
                    NewProjectMenu.SetupVersions();
                    UpdateProject(NewProjectMenu.Versions[CurrentProject.mcVersion].First(), true);
                };
                UpdateProjectMenuItem.DropDownItems.Add(i);
                i = new ToolStripMenuItem(p.path);
                i.Click += (sender, e) =>
                {
                    if (CurrentProject != p)
                    {
                        OpenProject(p.path); //Make it open the current project if it is not already open
                    }
                    ChangeProjectVersionMenu.ShowChangeProjectVersionMenu();
                };
                ChangeProjectVersionMenuItem.DropDownItems.Add(i);
                i = new ToolStripMenuItem(p.path);
                i.Click += (sender, e) =>
                {
                    if(CurrentProject != p)
                    {
                        OpenProject(p.path); //Make it open the current project if it is not already open
                    }
                    RefreshProject();
                };
                RefreshProjectMenuItem.DropDownItems.Add(i);
            }
        }
        #endregion

        #region Form Layout
        //Setup layout again when resizing
        private void ResizeWindow(object sender, EventArgs e)
        {
            SetupLayout();
        }

        //Make the form look beautiful
        private void SetupLayout()
        {
            int buttonWidth = ClientRectangle.Width / 7 - 9;
            int buttonY = Console.Height + 33;
            int buttonOffset = 9;
            NewProjectButton.Width = buttonWidth;
            NewProjectButton.Location = new Point(5, buttonY);
            OpenProjectButton.Width = buttonWidth;
            OpenProjectButton.Location = new Point(NewProjectButton.Location.X + buttonWidth + buttonOffset, buttonY);
            BuildProjectButton.Width = buttonWidth;
            BuildProjectButton.Location = new Point(OpenProjectButton.Location.X + buttonWidth + buttonOffset, buttonY);
            SetupProjectButton.Width = buttonWidth;
            SetupProjectButton.Location = new Point(BuildProjectButton.Location.X + buttonWidth + buttonOffset, buttonY);
            UpdateProjectButton.Width = buttonWidth;
            UpdateProjectButton.Location = new Point(SetupProjectButton.Location.X + buttonWidth + buttonOffset, buttonY);
            ChangeProjectVersionButton.Width = buttonWidth;
            ChangeProjectVersionButton.Location = new Point(UpdateProjectButton.Location.X + buttonWidth + buttonOffset, buttonY);
            RefreshProjectButton.Width = buttonWidth;
            RefreshProjectButton.Location = new Point(ChangeProjectVersionButton.Location.X + buttonWidth + buttonOffset, buttonY);
            ExitMenuItem.Click += (sender, args) => {
                Application.Exit();
            };
            //Console.TextChanged += ConsoleTextChanged;
            if(!Options.ContainsKey("console_font"))
                Options.Add("console_font", Console.Font);
        }
        #endregion

        #region Opening Projects
        //Open a new project from the given path
        public void OpenProject(string path)
        {
            string[] files = Directory.GetFiles(path);
            bool gradleFound = false;
            string mcVersion = "";
            string forgeVersion = "";
            foreach(string file in files)
            {
                if(file.Contains("gradlew.bat"))
                {
                    gradleFound = true;
                }
                if(file.Contains("build.gradle"))
                {
                    string[] data = File.ReadAllLines(file);
                    bool checkVersion = false;
                    foreach (string line in data)
                    {
                        if (checkVersion)
                        {
                            if (line.Contains("version") && line.Contains("="))
                            {
                                string version = line.Split('\"')[1];
                                mcVersion = version.Split('-')[0];
                                forgeVersion = version.Split('-')[1];
                            }
                        }
                        if(line.Contains("minecraft {") && !checkVersion)
                        {
                            checkVersion = true;
                        }
                        else if(line.Contains("}") && checkVersion)
                        {
                            checkVersion = false;
                            break;
                        }
                    }
                }
            }
            if(gradleFound && !string.IsNullOrWhiteSpace(mcVersion) && !string.IsNullOrWhiteSpace(forgeVersion))
            {
                Project p = new Project(path, mcVersion, forgeVersion);
                Text = "Forge Mod Builder - " + path;
                bool shouldadd = true;
                Project p2 = p;
                foreach(Project project in Projects)
                {
                    if(project.path.Equals(p.path))
                    {
                        shouldadd = false;
                        p2 = project;
                    }
                }
                if(!shouldadd)
                {
                    Projects.Remove(p2);
                }
                Projects.Insert(0, p);
                
                CurrentProject = p;
                UpdateProjects(true, false);
                Console.Text = string.Empty;
                System.Console.WriteLine("Loaded project: " + p);
                AddConsoleText("Loaded project: " + p);
                MessageBox.Show("Successfully loaded project!", "Loaded Project!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please choose a valid forge project!", "Invalid Project!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //Open a project showing a folder browser dialog
        public void OpenProject()
        {
            using(var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();
                if(result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    OpenProject(fbd.SelectedPath);
                }
            }
        }
        #endregion

        //Running any gradle command
        private void RunGradle(string command)
        {
            System.Console.WriteLine("Ran gradlew command: gradlew " + command);
            AddConsoleText("Ran gradlew command: gradlew " + command);
            Process p = new Process();
            p.StartInfo = new ProcessStartInfo(CurrentProject.path + "/gradlew.bat", command);
            p.StartInfo.WorkingDirectory = CurrentProject.path;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.CreateNoWindow = true;
            p.OutputDataReceived += GradleOutputReceived;
            p.Start();
            p.BeginOutputReadLine();
        }

        #region Adding Console Text
        //For multi-threading
        delegate void AddConsoleTextCallback(string text);

        //Add actual text to the console
        public void AddConsoleText(string text)
        {
            if(Console.InvokeRequired)
            {
                AddConsoleTextCallback d = new AddConsoleTextCallback(AddConsoleText);
                Invoke(d, new object[] { text });
            }
            else
            {
                string tokens = "(UP-TO-DATE|SKIPPED|BUILD SUCCESSFUL)";
                Regex rex = new Regex(tokens);
                MatchCollection mc = rex.Matches(text);
                int startcursorposition = Console.SelectionStart;
                int start = Console.TextLength;
                Console.AppendText(text + "\n");
                int end = Console.TextLength; // now longer by length of appended text
                foreach (Match m in mc)
                {
                    // Select text that was appended
                    Console.Select(start + m.Index, end - start);

                    Console.SelectionColor = Color.YellowGreen;
                    Console.SelectionFont = new Font(Console.Font, FontStyle.Bold);

                    // Unselect text
                    Console.SelectionLength = startcursorposition;
                    Console.SelectionStart = Console.Text.Length;
                    Console.SelectionColor = Color.Black;
                    Console.SelectionFont = new Font(Console.Font, FontStyle.Regular);
                }

                tokens = "(FAILED|BUILD FAILED)";
                rex = new Regex(tokens);
                mc = rex.Matches(text);
                foreach (Match m in mc)
                {
                    // Select text that was appended
                    Console.Select(start + m.Index, end - start);

                    Console.SelectionColor = Color.Red;
                    Console.SelectionFont = new Font(Console.Font, FontStyle.Bold);

                    // Unselect text
                    Console.SelectionLength = startcursorposition;
                    Console.SelectionStart = Console.Text.Length;
                    Console.SelectionColor = Color.Black;
                    Console.SelectionFont = new Font(Console.Font, FontStyle.Regular);
                }
                Console.ScrollToCaret();
                Console.DetectUrls = true;
            }
        }

        //Add all of gradle output to the console
        private void GradleOutputReceived(object sender, DataReceivedEventArgs e)
        {
            if(e.Data != null)
            {
                AddConsoleText(e.Data);
            } else
            {
                AddConsoleText("");
            }
        }
        #endregion

        #region Button Actions
        //Build the current project
        public void BuildProject()
        {
            if(CurrentProject != null)
            {
                System.Console.WriteLine("Building project: " + CurrentProject);
                AddConsoleText("Building project: " + CurrentProject);
                if(MessageBox.Show("Would you like to make the built file deobfuscated?", "Deobfuscate?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    RunGradle("jar");
                }
                else
                {
                    RunGradle("build");
                }
            }
            else
            {
                MessageBox.Show("Please open a project!", "No open project", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Setup the current project with arguments
        public void SetupProject(string args)
        {
            if(CurrentProject != null)
            {
                System.Console.WriteLine("Setting up project: " + CurrentProject);
                AddConsoleText("Setting up project: " + CurrentProject);
                string editor = SetupProjectMenu.Dialog.ShowDialog();
                //string editor = Microsoft.VisualBasic.Interaction.InputBox("Enter either idea or eclipse", "What editor are you using?");
                if(!string.IsNullOrEmpty(editor))
                {
                    if(editor.ToLower() == "eclipse" || editor.ToLower() == "idea")
                    {
                        System.Console.WriteLine("Setting editor to " + editor.ToLower());
                        AddConsoleText("Setting editor to " + editor.ToLower());
                        RunGradle("setupDecompWorkspace " + editor.ToLower() + args);
                    } else
                    {
                        MessageBox.Show("Please enter a valid editor. It must be either idea or eclipse", "Invalid Editor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                } else
                {
                    MessageBox.Show("Please enter an editor. Either idea or eclipse.", "No editor specified", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please open a project!", "No open project", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Update the current project
        public void UpdateProject(string forgeVersion, bool showDialog)
        {
            if(CurrentProject != null)
            {
                System.Console.WriteLine("Updating project: " + CurrentProject);
                AddConsoleText("Updating project: " + CurrentProject);
                string[] files = Directory.GetFiles(CurrentProject.path);
                foreach(string file in files)
                {
                    if(file.Contains("build.gradle"))
                    {
                        bool checkVersion = false;
                        bool update = false;
                        string newVersionLine;
                        string[] oldData = File.ReadAllLines(file);
                        string[] newData = new string[oldData.Length];
                        int index = 0;
                        foreach (string line in oldData)
                        {
                            if (checkVersion)
                            {
                                if (line.Contains("version") && line.Contains("="))
                                {
                                    WebClient client = new WebClient();
                                    System.Console.WriteLine("Checking updates at address: " + "http://files.minecraftforge.net/maven/net/minecraftforge/forge/index_" + CurrentProject.mcVersion + ".html");
                                    AddConsoleText("Checking updates at address: " + "http://files.minecraftforge.net/maven/net/minecraftforge/forge/index_" + CurrentProject.mcVersion + ".html");
                                    System.Console.WriteLine("Current Version: MC: " + CurrentProject.mcVersion + ", Forge: " + CurrentProject.forgeVersion);
                                    AddConsoleText("Current Version: MC: " + CurrentProject.mcVersion + ", Forge: " + CurrentProject.forgeVersion);
                                    System.Console.WriteLine("New Version: MC: " + CurrentProject.mcVersion + ", Forge: " + forgeVersion);
                                    AddConsoleText("New Version: MC: " + CurrentProject.mcVersion + ", Forge: " + forgeVersion);
                                    if (CurrentProject.forgeVersion.Contains(forgeVersion))
                                    {
                                        newVersionLine = line;
                                        AddConsoleText("No update available");
                                        MessageBox.Show("You are already up to date", "No update available", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    else
                                    {
                                        AddConsoleText("Update available!");
                                        if(showDialog)
                                        {
                                            if (MessageBox.Show("A new forge version is available. Would you like to update?", "Update available", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                            {
                                                update = true;
                                                newData[index] = "    version = \"" + CurrentProject.mcVersion + "-" + forgeVersion + "\"";
                                                index++;
                                                continue;
                                            }
                                        }
                                        else
                                        {
                                            update = true;
                                            newData[index] = "    version = \"" + CurrentProject.mcVersion + "-" + forgeVersion + "\"";
                                            index++;
                                            continue;
                                        }
                                    }
                                }
                            }
                            if (line == "minecraft {" && !checkVersion)
                            {
                                checkVersion = true;
                            }
                            else if (line == "}" && checkVersion)
                            {
                                checkVersion = false;
                            }
                            newData[index] = line;
                            index++;
                        }
                        if(update)
                        {
                            StreamWriter w = new StreamWriter(file);
                            foreach(string line in newData)
                            {
                                w.WriteLine(line);
                            }
                            w.Close();
                            SetupProject(" --refresh-dependencies");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please open a project!", "No open project", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Reload the current project
        public void RefreshProject()
        {
            if(CurrentProject != null)
            {
                System.Console.WriteLine("Refreshing project: " + CurrentProject);
                AddConsoleText("Refreshing project: " + CurrentProject);
                SetupProject(" --refresh-dependencies");
            }
            else
            {
                MessageBox.Show("Please open a project!", "No open project", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Create a new project
        public void NewProject()
        {
            System.Console.WriteLine("Creating a new project!");
            AddConsoleText("Creating a new project!");
            NewProjectMenu.ShowNewProjectMenu();
        }
        #endregion

        #region Menu Strip Buttons
        private void OpenProjectClick(object sender, EventArgs e)
        {
            OpenProject();
        }

        private void BuildProjectClick(object sender, EventArgs e)
        {
            BuildProject();
        }

        private void SetupProjectClick(object sender, EventArgs e)
        {
            SetupProject("");
        }

        private void UpdateProjectClick(object sender, EventArgs e)
        {
            NewProjectMenu.SetupVersions();
            UpdateProject(NewProjectMenu.Versions[CurrentProject.mcVersion].First(), true);
        }

        private void ChangeProjectVersionClick(object sender, EventArgs e)
        {
            ChangeProjectVersionMenu.ShowChangeProjectVersionMenu();
        }

        private void RefreshProjectClick(object sender, EventArgs e)
        {
            RefreshProject();
        }

        private void NewProjectClick(object sender, EventArgs e)
        {
            NewProject();
        }

        private void ConsoleFontMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            fd.Font = Console.Font;
            if (fd.ShowDialog() == DialogResult.OK)
            {
                Console.Font = fd.Font;
            }
            ConsoleTextChanged(sender, e);
            if (Options.ContainsKey("console_font"))
                Options["console_font"] = Console.Font;
            UpdateOptions(true, false);
            Console.DetectUrls = true;
        }

        private void ClearConsoleMenuItem_Click(object sender, EventArgs e)
        {
            Console.Text = "";
        }

        private void ClearProjectCacheMenuItem_Click(object sender, EventArgs e)
        {
            Projects.Clear();
            UpdateProjects(true, false);
        }

        private void ClearVersionsCacheMenuItem_Click(object sender, EventArgs e)
        {
            NewProjectMenu.Versions.Clear();
            NewProjectMenu.UpdateVersions(true, false);
            NewProjectMenu.Sync = true;
        }

        private void CheckForUpdatesMenuItem_Click(object sender, EventArgs e)
        {
            UpdateChecker.CheckForUpdates(UpdateChecker.UpdateURL);
        }

        private void CheckVersionsMenuItem_Click(object sender, EventArgs e)
        {
            NewProjectMenu.SyncVersions();
            NewProjectMenu.UpdateVersions(true, false);
            NewProjectMenu.Sync = false;
        }

        #endregion

        #region Console Text Handling
        //Handling console text
        private void ConsoleTextChanged(object sender, EventArgs e)
        {
            string tokens = "(UP-TO-DATE|SKIPPED|BUILD SUCCESSFUL)";
            Regex rex = new Regex(tokens);
            MatchCollection mc = rex.Matches(Console.Text);
            int StartCursorPosition = Console.SelectionStart;
            foreach(Match m in mc)
            {
                int StartIndex = m.Index;
                int StopIndex = m.Length;
                Console.Select(StartIndex, StopIndex);
                Console.SelectionColor = Color.YellowGreen;
                Console.SelectionFont = new Font(Console.Font, FontStyle.Bold);
                Console.SelectionStart = StartCursorPosition;
                Console.SelectionColor = Color.Black;
            }
            Console.DetectUrls = false;
        }

        private void ConsoleLinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }
        #endregion
    }
    //All the details needed for a project
    public class Project
    {
        public string path;
        public string mcVersion;
        public string forgeVersion;

        public Project(string path, string mcVersion, string forgeVersion)
        {
            this.path = path;
            this.mcVersion = mcVersion;
            this.forgeVersion = forgeVersion;
        }

        public override string ToString()
        {
            return this.path + ", MC: " + this.mcVersion + ", Forge: " + this.forgeVersion;
        }
    }
}
