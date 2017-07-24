using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ForgeModBuilder
{
    public static class UpdateChecker
    {
        //The url where the update file is found
        public static string UpdateURL = "https://raw.githubusercontent.com/CJMinecraft01/ForgeModBuilder/master/update.json";

        //Print out whether there is an update and if there is, ask the user if they want to download
        public static void CheckForUpdates(string url)
        {
            WebClient client = new WebClient();
            Console.WriteLine("Checking for updates at URL: " + url);
            Program.INSTANCE.AddConsoleText("Checking for updates at URL: " + url);
            try
            {
                string data = client.DownloadString(url); //Get the data from the url
                Update update = JsonConvert.DeserializeObject<Update>(data); //Convert the data to the Update object
                if(Application.ProductVersion != update.version) //If there is an update
                {
                    Console.WriteLine("An update is available!");
                    Program.INSTANCE.AddConsoleText("An update is available!");
                    //Say there is an update
                    Console.WriteLine("Current Version: " + Application.ProductVersion + ", Newest Version: " + update.version);
                    Program.INSTANCE.AddConsoleText("Current Version: " + Application.ProductVersion + ", Newest Version: " + update.version);
                    string changelog = ""; //Get the changelog
                    foreach(string line in update.changelog)
                    {
                        changelog += line + "\n"; //Add the changelog
                    }
                    if(MessageBox.Show("An update is available! \n" + update.name + "\n" + update.version + "\n" + changelog + "\nWould you like to update now?", "Update available", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        //Download the update
                        SaveFileDialog sfd = new SaveFileDialog();
                        sfd.FileName = "ForgeModBuilder-" + update.version + ".exe";
                        sfd.InitialDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                        sfd.Filter = "Executable Files|*.exe";
                        sfd.Title = "Select the download path";
                        sfd.ShowDialog();
                        client.DownloadFile(update.download, sfd.FileName);
                        Process.Start(sfd.FileName);
                        Application.Exit();
                    }
                }
                else
                {
                    Console.WriteLine("No update found");
                    Program.INSTANCE.AddConsoleText("No update found");
                }
            }
            catch(Exception e)
            {
                System.Console.WriteLine(e.Message);
                Program.INSTANCE.AddConsoleText("An error occurred\n" + e.Message);
                MessageBox.Show("An error occurred: " + e.Message, "An error occurred!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public class Update
        {
            public string name;
            public string version;
            public List<string> changelog;
            public string download;

            public Update(string name, string version, List<string> changelog, string download)
            {
                this.name = name;
                this.version = version;
                this.changelog = changelog;
                this.download = download;
            }
        }
    }
}
