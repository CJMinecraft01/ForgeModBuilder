using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForgeModBuilder.Managers
{
    public static class OptionsManager
    {
        public static string OptionsFileName { get; private set; } = "options.json";

        public static bool ForcedCreate { get; private set; } = false;

        private static Dictionary<string, object> Options = new Dictionary<string, object>();

        public static void SaveOptions()
        {
            ClientManager.WriteCustomData<Dictionary<string, object>>(Options, OptionsFileName);
        }

        public static void LoadOptions()
        {
            ForcedCreate = ClientManager.CreateCustomDataFileIfNotFound(OptionsFileName);
            Options = ClientManager.ReadCustomData<Dictionary<string, object>>(OptionsFileName);
        }

        public static T GetOption<T>(string Option, T DefaultValue)
        {
            if (Options.ContainsKey(Option))
            {
                if (Options[Option] is JArray)
                {
                    return ((JArray)Options[Option]).ToObject<T>();
                }
                return (T)Options[Option];
            }
            Options.Add(Option, DefaultValue);
            return DefaultValue;
        }

        public static void SetOption<T>(string Option, T Value)
        {
            if (Options.ContainsKey(Option))
            {
                Options[Option] = Value;
            }
            else
            {
                Options.Add(Option, Value);
            }
        }

    }

}
