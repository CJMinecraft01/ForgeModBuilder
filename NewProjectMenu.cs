using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using System.IO;
using System.IO.Compression;
using Newtonsoft.Json;
using System.Collections;

namespace ForgeModBuilder
{
    public partial class NewProjectMenu : Form
    {
        //Show the form
        public static DialogResult ShowNewProjectMenu()
        {
            NewProjectMenu menu = new NewProjectMenu();
            return menu.ShowDialog();
        }

        //The download URL for forge
        public static string ForgeDownloadsURL = "http://files.minecraftforge.net/maven/net/minecraftforge/forge/";
        //Contains the latest minecraft version once synced
        public static string LatestMinecraftVersion = "";
        //Stores all of the minecraft and forge versions once synced
        public static Dictionary<string, List<string>> Versions = new Dictionary<string, List<string>>();

        //The path to the versions.json file
        private static string VersionsFilePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/ForgeModBuilder/versions.json";

        //Whether to sync
        public static bool Sync = true;

        public NewProjectMenu()
        {
            InitializeComponent();
            LoadVersions();
            CancelButton = CancelSetupButton;
            AcceptButton = CreateProjectButton;
        }

        public static void SetupVersions()
        {
            UpdateVersions(false, true);
            if (Sync)
            {
                if (Versions.Keys.Count == 0)
                {
                    Console.WriteLine("Sycing versions!");
                    Program.INSTANCE.AddConsoleText("Sycing versions!");
                    SyncVersions();
                    UpdateVersions(true, false);
                }
                else
                {
                    HtmlWeb web = new HtmlWeb();
                    HtmlAgilityPack.HtmlDocument document = web.Load(ForgeDownloadsURL);
                    Dictionary<string, bool> checkVersions = new Dictionary<string, bool>();
                    if(Program.INSTANCE.Options.ContainsKey("sync_versions"))
                    {
                        checkVersions = (Dictionary<string, bool>)Program.INSTANCE.Options["sync_versions"];
                    }
                    foreach (string mcversion in Versions.Keys) {
                        if(checkVersions.Count != 0 && checkVersions.ContainsKey(mcversion))
                        {
                            if(!checkVersions[mcversion])
                            {
                                continue;
                            }
                        }
                        document =  web.Load(ForgeDownloadsURL + "index_" + mcversion + ".html");
                        HtmlNode node = document.DocumentNode.SelectSingleNode("//td[@class='download-version']");
                        string latestVersion = Regex.Replace(node.InnerHtml.Split('<')[0].Replace(" ", string.Empty).Replace(Environment.NewLine, string.Empty), @"\s+", "") + (node.InnerHtml.Contains("fa fa-star promo-recommended") ? "★" : "");
                        if (Versions[mcversion].First() != latestVersion)
                        {
                            Console.WriteLine("Outdated list of forge versions for minecraft " + mcversion);
                            Program.INSTANCE.AddConsoleText("Outdated list of forge versions for minecraft " + mcversion);
                            SyncVersion(mcversion);
                            UpdateVersions(true, false);
                        }
                        else
                        {
                            Console.WriteLine("No new forge versions found for minecraft " + mcversion);
                            Program.INSTANCE.AddConsoleText("No new forge versions found for minecraft " + mcversion);
                        }
                    }
                }
            }
            Sync = false;
        }

        public static void UpdateVersions(bool saveFile, bool readFromFile)
        {
            if (saveFile)
            {
                if(File.Exists(VersionsFilePath))
                {
                    JsonSerializer js = new JsonSerializer();
                    js.NullValueHandling = NullValueHandling.Ignore;
                    using (StreamWriter sw = new StreamWriter(VersionsFilePath))
                    using (JsonWriter jw = new JsonTextWriter(sw))
                    {
                        js.Serialize(jw, Versions);
                    }
                }
                else
                {
                    if(!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/ForgeModBuilder"))
                    {
                        Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/ForgeModBuilder");
                    }
                    File.Create(VersionsFilePath).Dispose();
                    UpdateVersions(saveFile, readFromFile);
                }
            }
            if (readFromFile)
            {
                if(File.Exists(VersionsFilePath))
                {
                    JsonSerializer js = new JsonSerializer();
                    js.NullValueHandling = NullValueHandling.Ignore;
                    using (StreamReader sr = new StreamReader(VersionsFilePath))
                    using (JsonReader jr = new JsonTextReader(sr))
                    {
                        Versions = js.Deserialize<Dictionary<string, List<string>>>(jr);
                    }
                    if(Versions == null)
                    {
                        Versions = new Dictionary<string, List<string>>();
                    }
                    if (Versions.Keys.Count > 0)
                    {
                        Version latest = new Version(Versions.Keys.First());
                        LatestMinecraftVersion = Versions.Keys.First();
                        Version current;
                        foreach (string version in Versions.Keys)
                        {
                            current = new Version(version.Contains("_") ? version.Substring(0, version.IndexOf("_") - 1) : version);
                            if (latest.CompareTo(current) < 0)
                            {
                                LatestMinecraftVersion = version;
                                latest = new Version(LatestMinecraftVersion);
                            }
                        }
                        Console.WriteLine("Latest version: " + LatestMinecraftVersion);
                    }
                }
                else
                {
                    if(!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/ForgeModBuilder")) {
                        Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/ForgeModBuilder");
                    }
                    File.Create(VersionsFilePath).Dispose();
                    UpdateVersions(saveFile, readFromFile);
                }
            }
            Program.INSTANCE.SelectVersionsToCheckMenuItem.DropDownItems.Clear();
            Dictionary<string, bool> checkVersions = new Dictionary<string, bool>();
            if (Program.INSTANCE.Options.ContainsKey("sync_versions"))
            {
                checkVersions = (Dictionary<string, bool>)Program.INSTANCE.Options["sync_versions"];
            }
            foreach (string mcversion in Versions.Keys)
            {
                ToolStripMenuItem menuItem = new ToolStripMenuItem(mcversion);
                if (checkVersions.Count != 0 && checkVersions.ContainsKey(mcversion))
                {
                    menuItem.Checked = checkVersions[mcversion];
                }
                menuItem.Click += (sender, args) => {
                    menuItem.Checked = !menuItem.Checked;
                };
                Program.INSTANCE.SelectVersionsToCheckMenuItem.DropDownItems.Add(menuItem);
            }
        }

        //Load the versions from the synced versions. Syncsing should have been done previously
        public void LoadVersions()
        {
            //Reset the holders of the versions
            if(Versions.Count == 0)
            {
                return;
            }
            MinecraftVersions.Items.Clear();
            ForgeVersions.Items.Clear();
            if(string.IsNullOrWhiteSpace(LatestMinecraftVersion))
            {
                LatestMinecraftVersion = Versions.Keys.First();
            }
            foreach(string mcversion in Versions.Keys)
            {
                MinecraftVersions.Items.Add(mcversion); //Add each minecraft version
            }
            if (Versions.ContainsKey(LatestMinecraftVersion))
            {
                foreach (string forgeVersion in Versions[LatestMinecraftVersion])
                {
                    ForgeVersions.Items.Add(forgeVersion); //Add each forge version
                }
            }
            else
            {
                foreach (string forgeVersion in Versions[Versions.Keys.First()])
                {
                    ForgeVersions.Items.Add(forgeVersion); //Add each forge version
                }
            }
            MinecraftVersions.SelectedIndex = 0; //Select the first minecraft version
        }

        //Load the versions from the forge web server
        public static void SyncVersions()
        {
            Versions.Clear();
            LatestMinecraftVersion = "";
            try
            {
                List<string> MinecraftVersions = new List<string>();
                //Create a nice looking progress bar to show how much has been got off the website
                ProgressBarForm ProgressBar = ProgressBarForm.ShowProgressBar("Syncing Versions", "Syncing Versions", new Font(new FontFamily("Microsoft Sans Serif"), 16, FontStyle.Regular));
                //Create something which will access the website
                HtmlWeb web = new HtmlWeb();
                HtmlAgilityPack.HtmlDocument document = web.Load(ForgeDownloadsURL);
                LatestMinecraftVersion = document.DocumentNode.SelectSingleNode("//li[@class='elem-active']").InnerHtml; //Get the latest minecraft version
                //MinecraftVersions.Add(LatestMinecraftVersion);
                foreach (HtmlNode mcVersionNode in document.DocumentNode.SelectNodes("//li[@class='li-version-list']"))
                {
                    foreach (HtmlNode subMcVersionNode in mcVersionNode.SelectNodes(".//li"))
                    {
                        if (subMcVersionNode.HasChildNodes)
                        {
                            foreach(HtmlNode node in subMcVersionNode.ChildNodes)
                            {
                                if(node.NodeType == HtmlNodeType.Element)
                                {
                                    MinecraftVersions.Add(node.InnerHtml); //Add each minecraft version
                                }
                            }
                        }
                    }
                }
                Version latestVersion = new Version(LatestMinecraftVersion);
                Version mc;
                foreach(string mcversion in MinecraftVersions.ToList())
                {
                    mc = new Version(mcversion.Contains("_") ? mcversion.Substring(0, mcversion.IndexOf("_") - 1) : mcversion);
                    if(latestVersion.CompareTo(mc) < 0)
                    {
                        MinecraftVersions.Insert(MinecraftVersions.IndexOf(mcversion), LatestMinecraftVersion);
                        MinecraftVersions.Remove(mcversion);
                        LatestMinecraftVersion = mcversion;
                        MinecraftVersions.Insert(0, mcversion);
                    }
                }
                if(latestVersion.CompareTo(new Version(LatestMinecraftVersion)) == 0)
                {
                    MinecraftVersions.Insert(0, LatestMinecraftVersion);
                }
                Console.WriteLine("Loaded minecraft versions from " + ForgeDownloadsURL);
                ForgeModBuilder.Program.INSTANCE.AddConsoleText("Loaded minecraft versions from " + ForgeDownloadsURL);
                foreach (string mcversion in MinecraftVersions)
                {
                    List<string> forgeVersions = new List<string>();
                    document = web.Load(ForgeDownloadsURL + "index_" + mcversion + ".html");
                    foreach (HtmlNode forgeVersionNode in document.DocumentNode.SelectNodes("//td[@class='download-version']"))
                    {
                        //Load each forge version for every minecraft version
                        ProgressBar.ProgressBar.Value = (int)((document.DocumentNode.SelectNodes("//td[@class='download-version']").IndexOf(forgeVersionNode) + 1F) / document.DocumentNode.SelectNodes("//td[@class='download-version']").Count + (int)((MinecraftVersions.IndexOf(mcversion) + 1F) / MinecraftVersions.Count * 100)) - 1;
                        forgeVersions.Add(Regex.Replace(forgeVersionNode.InnerHtml.Split('<')[0].Replace(" ", string.Empty).Replace(Environment.NewLine, string.Empty), @"\s+", "") + (forgeVersionNode.InnerHtml.Contains("fa fa-star promo-recommended") ? "★" : ""));
                    }
                    if(Versions.ContainsKey(mcversion))
                    {
                        Versions.Remove(mcversion);
                    }
                    Versions.Add(mcversion, forgeVersions);
                    Console.WriteLine("Loaded forge versions from " + ForgeDownloadsURL + "index_" + mcversion + ".html");
                    ForgeModBuilder.Program.INSTANCE.AddConsoleText("Loaded forge versions from " + ForgeDownloadsURL + "index_" + mcversion + ".html");
                }
                ProgressBar.Close(); //Close the progress bar
            }
            catch(Exception e)
            {
                Console.WriteLine("An error occurred:\n" + e.Message);
                Program.INSTANCE.AddConsoleText("An error occurred:\n" + e.Message);
            }
        }

        public static void SyncVersion(string mcversion)
        {
            if(!Versions.ContainsKey(mcversion))
            {
                Versions.Add(mcversion, new List<string>());
            }
            try
            {
                Versions[mcversion].Clear();
                HtmlWeb web = new HtmlWeb();
                HtmlAgilityPack.HtmlDocument document = web.Load(ForgeDownloadsURL + "index_" + mcversion + ".html");
                foreach (HtmlNode forgeVersionNode in document.DocumentNode.SelectNodes("//td[@class='download-version']"))
                {
                    string version = Regex.Replace(forgeVersionNode.InnerHtml.Split('<')[0].Replace(" ", string.Empty).Replace(Environment.NewLine, string.Empty), @"\s+", "") + (forgeVersionNode.InnerHtml.Contains("fa fa-star promo-recommended") ? "★" : "");
                    if (Versions[mcversion].Contains(version))
                        break;
                    Versions[mcversion].Add(version);
                }
                Console.WriteLine("Loaded forge versions from " + ForgeDownloadsURL + "index_" + mcversion + ".html");
                ForgeModBuilder.Program.INSTANCE.AddConsoleText("Loaded forge versions from " + ForgeDownloadsURL + "index_" + mcversion + ".html");
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred:\n" + e.Message);
                Program.INSTANCE.AddConsoleText("An error occurred:\n" + e.Message);
            }
        }

        //Download the selected forge version and setup automatically
        public void DownloadSelectedVersion()
        {
            if(ForgeVersions.SelectedItem == null) //Make sure they selected a forge version
            {
                MessageBox.Show("You need to select a forge version for the selected minecraft version to download", "Please choose a forge version", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(ProjectName.Text == "Project Name")
            {
                MessageBox.Show("You need to set a name for your project", "Invalid Project Name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(ProjectVersion.Text == "Project Version")
            {
                MessageBox.Show("You need to set the initial version for your project", "Invalid Version", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(ProjectGroupName.Text == "Project Group Name")
            {
                MessageBox.Show("You need to set a group name for your project", "Invalid Group Project Name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(JavaVersion.Text == "Java Version")
            {
                MessageBox.Show("You need to set a java version for your project", "Invalid Java Version", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                HtmlWeb web = new HtmlWeb();
                HtmlAgilityPack.HtmlDocument document = web.Load(ForgeDownloadsURL + "index_" + MinecraftVersions.SelectedItem + ".html"); //Find the download link from the slected version
                foreach(HtmlNode downloadNode in document.DocumentNode.SelectNodes("//tr"))
                {
                    if(downloadNode.HasChildNodes && downloadNode.ChildNodes[1].InnerText.Contains(((string) ForgeVersions.SelectedItem).Split('★')[0])) //If it is the correct version
                    {
                        HtmlNode downloadLinkNode = null; //Find the correct download link
                        try
                        {
                            downloadLinkNode = downloadNode.SelectSingleNode(".//a[.='Mdk']");
                        }
                        catch
                        {

                        }
                        if(downloadLinkNode == null)
                        {
                            try
                            {
                                downloadLinkNode = downloadNode.SelectSingleNode(".//a[.='Src']");
                            }
                            catch
                            {

                            }
                        }
                        if(downloadLinkNode != null)
                        {
                            string DownloadLink = downloadLinkNode.Attributes["href"].Value.Substring("http://adfoc.us/serve/sitelinks/?id=271228&url=".Length); //Remove the adfocus link as it breaks the download - find the direct download link
                            Console.WriteLine("Found download link: " + DownloadLink);
                            Program.INSTANCE.AddConsoleText("Found download link: " + DownloadLink);
                            FolderBrowserDialog fbd = new FolderBrowserDialog(); //Ask where they want to put the mod
                            if(fbd.ShowDialog() == DialogResult.OK)
                            {
                                if (System.IO.Directory.EnumerateFileSystemEntries(fbd.SelectedPath).Any()) //Make sure the folder is empty
                                {
                                    MessageBox.Show("The folder you selected has files in, please select another folder", "Invalid Folder", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                                ProgressBarForm form = ProgressBarForm.ShowProgressBar("Downloading Project Src", "Downloading Src", new Font(Font.FontFamily, 16, FontStyle.Regular));
                                using (WebClient wc = new WebClient())
                                {
                                    string[] Directory = fbd.SelectedPath.Split('\\');
                                    Directory[Directory.Length - 1] = ""; //Get the folder above the chosen folder
                                    wc.DownloadProgressChanged += (sender, e) =>
                                    {
                                        form.ProgressBar.Value = e.ProgressPercentage; //Update progress bar based on how much has been downloaded
                                    };
                                    wc.DownloadFileCompleted += (sender, e) =>
                                    {
                                        form.Close();
                                        Close();
                                        Console.WriteLine("Downloaded file to " + string.Join("\\", Directory) + "temp.zip");
                                        Program.INSTANCE.AddConsoleText("Downloaded file to " + string.Join("\\", Directory) + "temp.zip");
                                        ZipFile.ExtractToDirectory(string.Join("\\", Directory) + "temp.zip", fbd.SelectedPath); //Extract the file
                                        Console.WriteLine("Extracting file to " + fbd.SelectedPath);
                                        Program.INSTANCE.AddConsoleText("Extracting file to " + fbd.SelectedPath);
                                        File.Delete(string.Join("\\", Directory) + "temp.zip"); //Delete the zip file
                                        SetupBuildFile(fbd.SelectedPath); //Setup the build.gradle file
                                        Program.INSTANCE.OpenProject(fbd.SelectedPath); //Open the project
                                        if (MessageBox.Show("Would you like to setup now?", "Setup now?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                        {
                                            Program.INSTANCE.SetupProject(""); //Setup the project
                                        }
                                    };
                                    wc.DownloadFileAsync(new Uri(DownloadLink), string.Join("\\", Directory) + "temp.zip"); //Start the download
                                }
                            }
                            else
                            {
                                return;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred:\n" + (string)e.Message);
                Program.INSTANCE.AddConsoleText("An error occurred:\n" + (string)e.Message);
            }
        }

        //Setup all of the values in the build.gradle file
        private void SetupBuildFile(string path)
        {
            Console.WriteLine("Setting up build file!");
            Program.INSTANCE.AddConsoleText("Setting up build file!");
            foreach (string file in Directory.GetFiles(path)) //Find the build.gradle file in the directory
            {
                if (file.Contains("build.gradle"))
                {
                    string[] oldData = File.ReadAllLines(file); //Read all of the data in the file
                    string[] newData = new string[oldData.Length];
                    bool firstVersion = true;
                    bool firstGroup = true;
                    int index = 0;
                    foreach(string line in oldData)
                    {
                        if (line.Contains("sourceCompatibility") && line.Contains("targetCompatibility"))
                        {
                            newData[index] = Regex.Replace(line, "\'.*\'", "\'" + JavaVersion.Text + "\'"); //Set the java version
                            index++;
                            continue;
                        }
                        if (line.Contains("version") && firstVersion)
                        {
                            newData[index] = Regex.Replace(line, "\".*\"", "\"" + ProjectVersion.Text + "\""); //Set the project version
                            index++;
                            firstVersion = false;
                            continue;
                        }
                        if (line.Contains("archivesBaseName"))
                        {
                            newData[index] = Regex.Replace(line, "\".*\"", "\"" + ProjectName.Text + "\""); //Set the project name
                            index++;
                            continue;
                        }
                        if (line.Contains("group") && firstGroup)
                        {
                            newData[index] = Regex.Replace(line, "\".*\"", "\"" + ProjectGroupName.Text + "\""); //Set the project group name
                            index++;
                            firstGroup = false;
                            continue;
                        }
                        newData[index] = line;
                        index++;
                    }
                    StreamWriter w = new StreamWriter(file);
                    foreach (string line in newData)
                    {
                        w.WriteLine(line); //Write the file
                    }
                    w.Close();
                }
            }
        }

        //Show the correct forge versions for the given minecraft version
        private void MinecraftVersions_SelectedIndexChanged(object sender, EventArgs e)
        {
            ForgeVersions.Items.Clear();
            foreach(string forgeVersion in Versions[MinecraftVersions.SelectedItem.ToString()])
            {
                ForgeVersions.Items.Add(forgeVersion);
            }
        }

        private void CreateProjectButton_Click(object sender, EventArgs e)
        {
            DownloadSelectedVersion();
        }
    }
}
