using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace ForgeModBuilder.Managers
{
    public static class ClientManager
    {
        public static string ClientDataPath { get; private set; } = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\ForgeModBuilder2\";

        public const int DelayOnRetry = 1000;
        public const int NumberOfRetries = 3;

        public static T ReadCustomData<T>(string DataPath) where T: new()
        {
            CheckClientDataDirectory();

            T data = new T();

            if (File.Exists(ClientDataPath + DataPath))
            {
                // Found the file, so lets read it!
                JsonSerializer js = new JsonSerializer();
                using (StreamReader sr = new StreamReader(ClientDataPath + DataPath))
                using (JsonReader jr = new JsonTextReader(sr))
                {
                    data = js.Deserialize<T>(jr);
                    if (data == null) {
                        data = new T();
                    }
                    jr.Close();
                }
            }
            else
            {
                // The file does not exist so the data cannot be read
                throw new FileNotFoundException("Cannot read custom data from file " + ClientDataPath + DataPath + " as the file does not exist!");
            }

            return data;
        }

        public static void WriteCustomData<T>(T Data, string DataPath)
        {
            CheckClientDataDirectory();
            CreateCustomDataFileIfNotFound(DataPath);

            JsonSerializer js = new JsonSerializer();
            using (StreamWriter sw = new StreamWriter(ClientDataPath + DataPath))
            using (JsonWriter jw = new JsonTextWriter(sw))
            {
                js.Serialize(jw, Data);
                jw.Close();
            }
        }

        public static void CreateCustomDataFileIfNotFound(string DataPath)
        {
            if (!File.Exists(ClientDataPath + DataPath))
            {
                File.CreateText(ClientDataPath + DataPath).Close();
            }
        }

        public static void CheckClientDataDirectory()
        {
            if (!Directory.Exists(ClientDataPath))
            {
                // If we don't have the custom data directory
                Directory.CreateDirectory(ClientDataPath);
                // Create it!
            }
        }

    }
}
