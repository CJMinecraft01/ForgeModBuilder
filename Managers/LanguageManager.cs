using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace ForgeModBuilder.Managers
{
    public static class LanguageManager
    {
        public static string LanguagesFilePath { get; private set; } = ClientManager.ClientDataPath + "Languages";


        public static Language CurrentLanguage { get; set; }

        private static Dictionary<string, string> AvailableLanguages = new Dictionary<string, string>();

        public static void InitLanguages()
        {
            if (ForgeModBuilder.Debugging)
            {
                LanguagesFilePath = @"..\..\Languages";
            }
            
            if (File.Exists(LanguagesFilePath + @"\languages.json"))
            {
                // Read all of the languages
                JsonSerializer js = new JsonSerializer();
                js.NullValueHandling = NullValueHandling.Ignore;
                using (StreamReader sr = new StreamReader(LanguagesFilePath + @"\languages.json"))
                using (JsonReader jr = new JsonTextReader(sr))
                {
                    AvailableLanguages = js.Deserialize<Dictionary<string, string>>(jr);
                }
            }
            else
            {
                // Language File does not exist, need to download languages
                // I.e. first setup
            }
        }

        public static void LoadLanguages()
        {
            
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
            this.Path = LanguageManager.LanguagesFilePath + @"\" + this.Name + ".lang";
            LoadLanguage();
        }

        public void LoadLanguage()
        {
            if (Directory.Exists(LanguageManager.LanguagesFilePath))
            {
                if (File.Exists(this.Path))
                {
                    foreach (string line in File.ReadAllLines(this.Path))
                    {
                        int colonIndex = line.IndexOf(':');
                        string key = line.Substring(0, colonIndex);
                        string value = line.Substring(colonIndex - 1, line.Length);
                        TranslationKeys.Add(key, value);
                    }
                }
            }
        }

        public string Localize(string key, params object[] args)
        {
            if(TranslationKeys == null)
            {
                Console.WriteLine("Langauge " + this.Name + " not loaded!");
            }
            string value;
            if(TranslationKeys.TryGetValue(key, out value))
            {
                return value;
            }
            Console.WriteLine("Invalid key: " + key + " for language " + this.Name);
            return "";
        }
    }

}
