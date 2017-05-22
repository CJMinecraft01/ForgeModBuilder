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
        private Project CurrentProject;

        private static string ProjectsFilePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/ForgeModBuilder/projects.json";
        private static string OptionsFilePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/ForgeModBuilder/options.json";

        private List<Project> Projects = new List<Project>();
        private Dictionary<string, object> Options = new Dictionary<string, object>();

        public FMB()
        {
            InitializeComponent();
            Resize += ResizeWindow;
            SetupLayout();
            UpdateProjects(false, true);
            UpdateOptions(false, true);
            FormClosed += FMBClosed;
            Load += (sender, e) =>
            {
                UpdateChecker.CheckForUpdates(UpdateChecker.UpdateURL);
            };
        }

        private void FMBClosed(object sender, FormClosedEventArgs e)
        {
            UpdateProjects(true, false);
            UpdateOptions(true, false);
        }

        public void UpdateOptions(bool saveFile, bool readFromFile)
        {
            if(saveFile)
            {
                if(File.Exists(OptionsFilePath))
                {
                    JsonSerializer js = new JsonSerializer();
                    js.NullValueHandling = NullValueHandling.Ignore;
                    using (StreamWriter sw = new StreamWriter(OptionsFilePath))
                    using (JsonWriter jw = new JsonTextWriter(sw))
                    {
                        js.Serialize(jw, Options);
                    }
                }
                else
                {
                    if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/ForgeModBuilder"))
                    {
                        Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/ForgeModBuilder");
                    }
                    File.Create(OptionsFilePath).Dispose();
                    UpdateOptions(saveFile, readFromFile);
                }
            }
            if (readFromFile)
            {
                if (File.Exists(OptionsFilePath))
                {
                    Projects.Clear();

                    JsonSerializer js = new JsonSerializer();
                    js.NullValueHandling = NullValueHandling.Ignore;
                    using (StreamReader sr = new StreamReader(OptionsFilePath))
                    using (JsonReader jr = new JsonTextReader(sr))
                    {
                        Options = js.Deserialize<Dictionary<string, object>>(jr);
                    }
                    if (Options == null)
                    {
                        Options = new Dictionary<string, object>();
                    }
                }
                else
                {
                    if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/ForgeModBuilder"))
                    {
                        Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/ForgeModBuilder");
                    }
                    File.Create(OptionsFilePath).Dispose();
                    UpdateOptions(saveFile, readFromFile);
                }
            }

            if (Options.ContainsKey("console_font"))
            {
                if (Options["console_font"] is string)
                {
                    Console.Font = JsonConvert.DeserializeObject<Font>("\"" + (string)Options["console_font"] + "\"");
                }
                else
                {
                    Console.Font = (Font)Options["console_font"];
                }
            }
        }

        public void UpdateProjects(bool saveFile, bool readFromFile)
        {
            if(saveFile)
            {
                if(File.Exists(ProjectsFilePath))
                {
                    JsonSerializer js = new JsonSerializer();
                    js.NullValueHandling = NullValueHandling.Ignore;
                    using (StreamWriter sw = new StreamWriter(ProjectsFilePath))
                    using (JsonWriter jw = new JsonTextWriter(sw))
                    {
                        js.Serialize(jw, Projects);
                    }
                }
                else {
                    if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/ForgeModBuilder"))
                    {
                        Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/ForgeModBuilder");
                    }
                    File.Create(ProjectsFilePath).Dispose();
                    UpdateProjects(saveFile, readFromFile);
                }
            }
            if(readFromFile)
            {
                if(File.Exists(ProjectsFilePath))
                {
                    Projects.Clear();

                    JsonSerializer js = new JsonSerializer();
                    js.NullValueHandling = NullValueHandling.Ignore;
                    using (StreamReader sr = new StreamReader(ProjectsFilePath))
                    using(JsonReader jr = new JsonTextReader(sr))
                    {
                        Projects = js.Deserialize<List<Project>>(jr);
                    }
                    if(Projects == null)
                    {
                        Projects = new List<Project>();
                    }
                } else
                {
                    if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/ForgeModBuilder"))
                    {
                        Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/ForgeModBuilder");
                    }
                    File.Create(ProjectsFilePath).Dispose();
                    UpdateProjects(saveFile, readFromFile);
                }
            }

            OpenProjectMenuItem.DropDownItems.Clear();
            BuildProjectMenuItem.DropDownItems.Clear();
            SetupProjectMenuItem.DropDownItems.Clear();
            UpdateProjectMenuItem.DropDownItems.Clear();
            RefreshProjectMenuItem.DropDownItems.Clear();
            foreach (Project p in Projects)
            {
                ToolStripMenuItem i = new ToolStripMenuItem(p.path);
                i.Click += (sender, e) =>
                {
                    if(CurrentProject != p)
                        OpenProject(p.path);
                };
                OpenProjectMenuItem.DropDownItems.Add(i);
                i = new ToolStripMenuItem(p.path);
                i.Click += (sender, e) =>
                {
                    if (CurrentProject != p)
                    {
                        OpenProject(p.path);
                    }
                    BuildProject();
                };
                BuildProjectMenuItem.DropDownItems.Add(i);
                i = new ToolStripMenuItem(p.path);
                i.Click += (sender, e) =>
                {
                    if (CurrentProject != p)
                    {
                        OpenProject(p.path);
                    }
                    SetupProject("");
                };
                SetupProjectMenuItem.DropDownItems.Add(i);
                i = new ToolStripMenuItem(p.path);
                i.Click += (sender, e) =>
                {
                    if (CurrentProject != p)
                    {
                        OpenProject(p.path);
                    }
                    UpdateProject();
                };
                UpdateProjectMenuItem.DropDownItems.Add(i);
                i = new ToolStripMenuItem(p.path);
                i.Click += (sender, e) =>
                {
                    if(CurrentProject != p)
                    {
                        OpenProject(p.path);
                        RefreshProject();
                    }
                };
                RefreshProjectMenuItem.DropDownItems.Add(i);
            }
        }

        private void ResizeWindow(object sender, EventArgs e)
        {
            SetupLayout();
        }

        private void SetupLayout()
        {
            int buttonWidth = ClientRectangle.Width / 5 - 7;
            int buttonY = Console.Height + 33;
            OpenProjectButton.Width = buttonWidth;
            OpenProjectButton.Location = new Point(8, buttonY);
            BuildProjectButton.Width = buttonWidth;
            BuildProjectButton.Location = new Point(OpenProjectButton.Location.X + buttonWidth + 5, buttonY);
            SetupProjectButton.Width = buttonWidth;
            SetupProjectButton.Location = new Point(BuildProjectButton.Location.X + buttonWidth + 5, buttonY);
            UpdateProjectButton.Width = buttonWidth;
            UpdateProjectButton.Location = new Point(SetupProjectButton.Location.X + buttonWidth + 5, buttonY);
            RefreshProjectButton.Width = buttonWidth;
            RefreshProjectButton.Location = new Point(UpdateProjectButton.Location.X + buttonWidth + 5, buttonY);
            ExitMenuItem.Click += (sender, args) => {
                Application.Exit();
            };
            //Console.TextChanged += ConsoleTextChanged;
            if(!Options.ContainsKey("console_font"))
                Options.Add("console_font", Console.Font);
        }

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
                AddConsoleText("Loaded project: " + p + "\n");
                MessageBox.Show("Successfully loaded project!", "Loaded Project!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please choose a valid forge project!", "Invalid Project!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

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

        private void RunGradle(string command)
        {
            System.Console.WriteLine("Ran gradlew command: gradlew " + command);
            AddConsoleText("Ran gradlew command: gradlew " + command + "\n");
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

        delegate void AddConsoleTextCallback(string text);

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
                if(mc.Count == 0)
                {
                    Console.AppendText(text);
                    Console.ScrollToCaret();
                    Console.DetectUrls = true;
                    return;
                }
                int startcursorposition = Console.SelectionStart;
                int start = Console.TextLength;
                Console.AppendText(text);
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
                Console.ScrollToCaret();
                Console.DetectUrls = true;
            }
        }

        private void GradleOutputReceived(object sender, DataReceivedEventArgs e)
        {
            if(e.Data != null)
            {
                AddConsoleText(e.Data + "\n");
            } else
            {
                AddConsoleText("\n");
            }
        }

        public void BuildProject()
        {
            if(CurrentProject != null)
            {
                System.Console.WriteLine("Building project: " + CurrentProject);
                AddConsoleText("Building project: " + CurrentProject + "\n");
                RunGradle("build");
            }
            else
            {
                MessageBox.Show("Please open a project!", "No open project", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void SetupProject(string args)
        {
            if(CurrentProject != null)
            {
                System.Console.WriteLine("Setting up project: " + CurrentProject);
                AddConsoleText("Setting up project: " + CurrentProject + "\n");
                string editor = SetupProjectMenu.Dialog.ShowDialog();
                //string editor = Microsoft.VisualBasic.Interaction.InputBox("Enter either idea or eclipse", "What editor are you using?");
                if(!string.IsNullOrEmpty(editor))
                {
                    if(editor.ToLower() == "eclipse" || editor.ToLower() == "idea")
                    {
                        System.Console.WriteLine("Setting editor to " + editor.ToLower());
                        AddConsoleText("Setting editor to " + editor.ToLower() + "\n");
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

        public void UpdateProject()
        {
            if(CurrentProject != null)
            {
                System.Console.WriteLine("Updating project: " + CurrentProject);
                AddConsoleText("Updating project: " + CurrentProject + "\n");
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
                                    AddConsoleText("Checking updates at address: " + "http://files.minecraftforge.net/maven/net/minecraftforge/forge/index_" + CurrentProject.mcVersion + ".html" + "\n");
                                    try
                                    {
                                        string downloadString = client.DownloadString("http://files.minecraftforge.net/maven/net/minecraftforge/forge/index_" + CurrentProject.mcVersion + ".html");
                                        string latest = downloadString.Substring(downloadString.IndexOf("<small>") + 7, downloadString.IndexOf("</small>") - downloadString.IndexOf("<small>") - 7);
                                        latest = latest.Substring(0, latest.IndexOf('<'));
                                        string latestforgeversion = latest.Split('-')[1].Substring(1);
                                        System.Console.WriteLine("Current Version: MC: " + CurrentProject.mcVersion + ", Forge: " + CurrentProject.forgeVersion);
                                        AddConsoleText("Current Version: MC: " + CurrentProject.mcVersion + ", Forge: " + CurrentProject.forgeVersion + "\n");
                                        System.Console.WriteLine("Latest Version: MC: " + latest.Split('-')[0].Substring(0, latest.Split('-')[0].Length-1) + ", Forge: " + latestforgeversion);
                                        AddConsoleText("Latest Version: MC: " + latest.Split('-')[0].Substring(0, latest.Split('-')[0].Length-1) + ", Forge: " + latestforgeversion + "\n");
                                        if (CurrentProject.forgeVersion.Contains(latestforgeversion))
                                        {
                                            newVersionLine = line;
                                            AddConsoleText("No update available" + "\n");
                                            MessageBox.Show("You are already up to date", "No update available", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }
                                        else
                                        {
                                            AddConsoleText("Update available!" + "\n");
                                            if (MessageBox.Show("A new forge version is available. Would you like to update?", "Update available", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                            {
                                                update = true;
                                                newData[index] = "    version = \"" + CurrentProject.mcVersion + "-" + latestforgeversion + "\"";
                                                index++;
                                                continue;
                                            }
                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        System.Console.WriteLine(e.Message);
                                        AddConsoleText("An error occurred" + e.Message + "\n");
                                        MessageBox.Show("An error occurred: " + e.Message, "An error occurred!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        public void RefreshProject()
        {
            if(CurrentProject != null)
            {
                System.Console.WriteLine("Refreshing project: " + CurrentProject);
                AddConsoleText("Refreshing project: " + CurrentProject + "\n");
                SetupProject(" --refresh-dependencies");
            }
            else
            {
                MessageBox.Show("Please open a project!", "No open project", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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
            UpdateProject();
        }

        private void RefreshProjectClick(object sender, EventArgs e)
        {
            RefreshProject();
        }

        private void ConsoleFontMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            fd.Font = Console.Font;
            if(fd.ShowDialog() == DialogResult.OK)
            {
                Console.Font = fd.Font;
            }
            ConsoleTextChanged(sender, e);
            if (Options.ContainsKey("console_font"))
                Options["console_font"] = Console.Font;
            UpdateOptions(true, false);
            Console.DetectUrls = true;
        }

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
    }

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
