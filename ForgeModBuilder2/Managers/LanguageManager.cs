using ForgeModBuilder.Forms;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace ForgeModBuilder.Managers
{
    public static class LanguageManager
    {
        public static string LanguagesFilePath { get; private set; } = ClientManager.ClientDataPath + @"Languages\";

        public static Language CurrentLanguage { get; set; }

        private static Dictionary<string, string> AvailableLanguages = new Dictionary<string, string>();

        public static bool InitLanguages()
        {
            if (ForgeModBuilder.Debugging)
            {
                //LanguagesFilePath = @"..\..\..\Languages\";
            }

            if (!Directory.Exists(LanguagesFilePath))
            {
                Directory.CreateDirectory(LanguagesFilePath);
            }

            if (File.Exists(LanguagesFilePath + "languages.json"))
            {
                LoadAvailableLanguages();
            }
            else
            {
                // Language File does not exist, need to download languages
                // I.e. first setup
                return LanguagesSetup();
            }
            return false;
        }

        private static bool LanguagesSetup()
        {
            // Download the languages.json file

            WebClient client = new WebClient();
            client.DownloadFile(InstallationManager.UpdateLanguagesURL + "languages.json", LanguagesFilePath + "languages.json");

            LoadAvailableLanguages();

        SelectLanguage:

            LanguageSelectionForm form = new LanguageSelectionForm();
            form.LanguagesComboBox.Items.AddRange(AvailableLanguages.Keys.ToArray());

            if (form.ShowDialog() == DialogResult.OK)
            {

                if (form.LanguagesComboBox.SelectedItem == null || !AvailableLanguages.ContainsKey((string)form.LanguagesComboBox.SelectedItem))
                {
                    if (MessageBox.Show(CurrentLanguage.Localize("message_box.invalid_language.desc"), CurrentLanguage.Localize("message_box.invalid_language.title"), MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.OK)
                    {
                        goto SelectLanguage;
                    }
                    else
                    {
                        foreach (string FilePath in Directory.GetFiles(LanguagesFilePath))
                        {
                            File.Delete(FilePath);
                        }

                        Directory.Delete(LanguagesFilePath);
                        return true;
                    }
                }
                string SelectedLanguage = AvailableLanguages[(string)form.LanguagesComboBox.SelectedItem];
                Console.WriteLine(SelectedLanguage);
                OptionsManager.SetOption<string>("CurrentLanguage", (string)form.LanguagesComboBox.SelectedItem);

                client.DownloadFile(InstallationManager.UpdateLanguagesURL + SelectedLanguage + ".lang", LanguagesFilePath + SelectedLanguage + ".lang");
                client.Dispose();

                CurrentLanguage = new Language(SelectedLanguage);

                return false;
            }
            else
            {
                foreach (string FilePath in Directory.GetFiles(LanguagesFilePath))
                {
                    File.Delete(FilePath);
                }

                Directory.Delete(LanguagesFilePath);
                return true;
            }
        }


        private static void LoadAvailableLanguages()
        {
            // Read all of the languages
            JsonSerializer js = new JsonSerializer();
            js.NullValueHandling = NullValueHandling.Ignore;
            using (StreamReader sr = new StreamReader(LanguagesFilePath + "languages.json"))
            using (JsonReader jr = new JsonTextReader(sr))
            {
                AvailableLanguages = js.Deserialize<Dictionary<string, string>>(jr);
            }
            string CurrentLanguageName = OptionsManager.GetOption("CurrentLanguage", AvailableLanguages.Keys.First());
            if (AvailableLanguages.ContainsKey(CurrentLanguageName))
            {
                if (File.Exists(LanguagesFilePath + AvailableLanguages[CurrentLanguageName] + ".lang"))
                {
                    CurrentLanguage = new Language(AvailableLanguages[CurrentLanguageName]);
                }
                else
                {
                    // The language file doesn't exist
                    WebClient client = new WebClient();

                    client.DownloadFile(InstallationManager.UpdateLanguagesURL + AvailableLanguages[CurrentLanguageName] + ".lang", LanguagesFilePath + AvailableLanguages[CurrentLanguageName] + ".lang");
                    client.Dispose();

                    CurrentLanguage = new Language(AvailableLanguages[CurrentLanguageName]);
                }
            }
            else
            {
                // The currrent language name is invalid
                // This should never happen

            }
        }
    }

    public class Language
    {
        public string Name { get; private set; }
        public string Path { get; private set; }
        private Dictionary<string, string> TranslationKeys = new Dictionary<string, string>();

        public Language(string name)
        {
            this.Name = name;
            this.Path = LanguageManager.LanguagesFilePath + this.Name + ".lang";
            LoadLanguage();
        }

        public void LoadLanguage()
        {
            if (File.Exists(this.Path))
            {
                foreach (string line in File.ReadAllLines(this.Path))
                {
                    if (!line.StartsWith("##") && line.Contains('='))
                    {
                        int equalSignIndex = line.IndexOf('=') + 1;
                        string key = line.Substring(0, equalSignIndex - 1);
                        string value = line.Substring(equalSignIndex, line.Length - equalSignIndex);
                        TranslationKeys.Add(key, value);
                    }
                }
            }
        }

        public string Localize(string key, params object[] args)
        {
            if (TranslationKeys == null)
            {
                Console.WriteLine("Langauge " + this.Name + " not loaded!");
            }
            string value;
            if (TranslationKeys.TryGetValue(key, out value))
            {
                return value;
            }
            Console.WriteLine("Invalid key: " + key + " for language " + this.Name);
            return "";
        }
    }

}
