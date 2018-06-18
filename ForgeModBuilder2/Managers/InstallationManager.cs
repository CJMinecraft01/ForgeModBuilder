using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using System.Windows.Forms;

namespace ForgeModBuilder.Managers
{
    public static class InstallationManager
    {
        public static Version CurrentVersion = new Version(Application.ProductVersion);

        public static string UpdateURL { get; private set; } = "https://raw.githubusercontent.com/CJMinecraft01/ForgeModBuilder/rewrite/update.json";

        public static void CheckForUpdates()
        {
            WebClient client = new WebClient();

            string data = client.DownloadString(UpdateURL);
            Update update = JsonConvert.DeserializeObject<Update>(data);

            if (CurrentVersion.CompareTo(new Version(update.version)) < 0) {
                // The current version is old. We should update

                
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
