using System;
using System.Collections.Generic;

namespace ForgeModBuilder.Managers
{
    public static class ClientManager
    {
        public static string ClientDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\ForgeModBuilder\";

        public static Dictionary<string, object> Options = new Dictionary<string, object>();

        public static void LoadOptions()
        {

        }
    }
}
