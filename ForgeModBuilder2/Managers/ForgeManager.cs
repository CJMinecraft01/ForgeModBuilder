using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ForgeModBuilder.Managers;

namespace ForgeModBuilder.Managers
{
    public static class ForgeManager
    {

        public static string MCPVersionsFileName { get; private set; } = "mcp_versions.json";
        public static string ForgeVersionsFileName { get; private set; } = "forge_versions.json";

        public static Dictionary<string, Dictionary<string, List<string>>> MCPVersions { get; private set; }
        public static Dictionary<string, List<string>> ForgeVersions { get; private set; }

        public static void SaveVersions()
        {
            ClientManager.WriteCustomData<Dictionary<string, Dictionary<string, List<string>>>>(MCPVersions, MCPVersionsFileName);
            ClientManager.WriteCustomData<Dictionary<string, List<string>>>(ForgeVersions, ForgeVersionsFileName);
        }

        public static void LoadVersions()
        {
            ClientManager.CreateCustomDataFileIfNotFound(MCPVersionsFileName);
            ClientManager.CreateCustomDataFileIfNotFound(ForgeVersionsFileName);
            MCPVersions = ClientManager.ReadCustomData<Dictionary<string, Dictionary<string, List<string>>>>(MCPVersionsFileName);
            ForgeVersions = ClientManager.ReadCustomData<Dictionary<string, List<string>>>(ForgeVersionsFileName);
        }

    }
}
