using System;
using System.Collections.Generic;
using System.IO;

namespace ForgeModBuilder.Managers
{
    public static class LanguageManager
    {
        public static string LanguagesFilePath { get; private set; } = ClientManager.ClientDataPath + "Languages";

        public static Language CurrentLanguage { get; set; }

        private static List<string> AvailableLanguages = new List<string>();

        public static void InitLanguages()
        {
            if (ForgeModBuilder.Debugging)
            {
                LanguagesFilePath = @"..\..\Languages";
            }
            foreach(string FileName in Directory.GetFiles(LanguagesFilePath))
            {
                Console.WriteLine(FileName);
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
        private Dictionary<string, string> TranslationKeys;

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
