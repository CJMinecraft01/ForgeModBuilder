using System;
using System.Net;
using Newtonsoft.Json;
using System.Windows.Forms;
using ForgeModBuilder.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace ForgeModBuilder.Managers
{
    public static class InstallationManager
    {
        public static Version CurrentVersion = new Version(Application.ProductVersion);

        public static string UpdateURL { get; private set; } = "https://raw.githubusercontent.com/CJMinecraft01/ForgeModBuilder/rewrite/update.json";
        public static string UpdateLanguagesURL { get; private set; } = "https://raw.githubusercontent.com/CJMinecraft01/ForgeModBuilder/rewrite/Languages/";

        // Checks for any updates that are available and tries to download and install them
        public static bool CheckForUpdates()
        {
            WebClient client = new WebClient();

            string data = client.DownloadString(UpdateURL);
            Update update = JsonConvert.DeserializeObject<Update>(data);

            if (CurrentVersion.CompareTo(new Version(update.version)) < 0) {
                // The current version is old. We should update

                if (MessageBox.Show(LanguageManager.CurrentLanguage.Localize("message_box.update.desc"), LanguageManager.CurrentLanguage.Localize("message_box.update.title"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // The user wants to update so let's do it!

                    ProgressBarForm form = new ProgressBarForm();
                    form.ProgressBar.Maximum = 200;
                    form.CancelButton.Click += (sender, e) =>
                    {
                        form.Close();
                    };
                    Task<bool> task = InstallUpdates(form, client, update);
                    Application.Run(form);
                    return true;
                }
            }
            return false;
        }

        private static async Task<bool> InstallUpdates(ProgressBarForm form, WebClient client, Update update)
        {
            try
            {
                // Install the actual update and update the progress bar

                string directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\";
                string fileName = "ForgeModBuilder-" + update.version + ".exe";
                client.DownloadProgressChanged += (sender, e) =>
                {
                    form.ProgressBar.Value = e.ProgressPercentage;
                };
                client.DownloadFileCompleted += (sender, e) =>
                {
                    client.Dispose();
                    Task<bool> task = InstallLanguages(form);
                    task.Wait();

                    // Start updated version
                    Process.Start(directory + fileName);
                    Application.Exit();
                };
                client.DownloadFileAsync(new Uri(update.download), directory + fileName);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return true;
        }

        private static async Task<bool> InstallLanguages(ProgressBarForm form)
        {
            // Update languages
            WebClient client = new WebClient();
            foreach (string file in Directory.GetFiles(LanguageManager.LanguagesFilePath))
            {
                try
                {
                    client.DownloadFile(UpdateLanguagesURL + Path.GetFileName(file), file);
                    form.ProgressBar.Value += 100 / Directory.GetFiles(LanguageManager.LanguagesFilePath).Length;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return true;
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
