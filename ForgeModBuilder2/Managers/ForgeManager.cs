using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ForgeModBuilder.Managers;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace ForgeModBuilder.Managers
{
    public static class ForgeManager
    {

        public static string MCPVersionsURL { get; private set; } = "http://files.minecraftforge.net/maven/de/oceanlabs/mcp/versions.json";
        public static string ForgeVersionsURL { get; private set; } = "http://files.minecraftforge.net/maven/net/minecraftforge/forge/index_MCVERSION.json";
        public static string MCVersionsURL { get; private set; } = "https://raw.githubusercontent.com/CJMinecraft01/ForgeModBuilder/rewrite/mc_versions.json";

        public static string MCPVersionsFileName { get; private set; } = "mcp_versions.json";
        public static string ForgeVersionsFileName { get; private set; } = "forge_versions.json";
        public static string MCVersionsFileName { get; private set; } = "mc_versions.json";

        public static Dictionary<string, Dictionary<string, List<string>>> MCPVersions { get; private set; }
        public static Dictionary<string, List<string>> ForgeVersions { get; private set; }
        public static List<string> MCVersions { get; private set; }

        public static void SaveVersions()
        {
            ClientManager.WriteCustomData<Dictionary<string, Dictionary<string, List<string>>>>(MCPVersions, MCPVersionsFileName);
            ClientManager.WriteCustomData<Dictionary<string, List<string>>>(ForgeVersions, ForgeVersionsFileName);
            ClientManager.WriteCustomData<List<string>>(MCVersions, MCVersionsFileName);
        }

        public static void LoadVersions()
        {
            ClientManager.CreateCustomDataFileIfNotFound(MCPVersionsFileName);
            ClientManager.CreateCustomDataFileIfNotFound(ForgeVersionsFileName);
            ClientManager.CreateCustomDataFileIfNotFound(MCVersionsFileName);
            MCPVersions = ClientManager.ReadCustomData<Dictionary<string, Dictionary<string, List<string>>>>(MCPVersionsFileName);
            ForgeVersions = ClientManager.ReadCustomData<Dictionary<string, List<string>>>(ForgeVersionsFileName);
            MCVersions = ClientManager.ReadCustomData<List<string>>(MCVersionsFileName);
        }

        public static void UpdateVersionLists()
        {
            DownloadMCPVersionList();
            DownloadMCVersionList();
            DownloadForgeVersionList();
        }

        private static void DownloadMCPVersionList()
        {
            JsonSerializer js = new JsonSerializer();
            using (StreamReader sr = new StreamReader(WebRequest.Create(MCPVersionsURL).GetResponse().GetResponseStream()))
            using (JsonReader jr = new JsonTextReader(sr))
            {
                MCPVersions = js.Deserialize<Dictionary<string, Dictionary<string, List<string>>>>(jr);
                jr.Close();
            }
        }

        private static void DownloadMCVersionList()
        {
            JsonSerializer js = new JsonSerializer();
            using (StreamReader sr = new StreamReader(WebRequest.Create(MCVersionsURL).GetResponse().GetResponseStream()))
            using (JsonReader jr = new JsonTextReader(sr))
            {
                MCVersions = js.Deserialize<List<string>>(jr);
                jr.Close();
            }
            MCVersions.ForEach(i => Console.Write("{0}\t", i));
        }

        private static void DownloadForgeVersionList()
        {
            
        }

    }
}
