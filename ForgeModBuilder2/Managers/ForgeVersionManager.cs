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
using Newtonsoft.Json.Linq;
using ForgeModBuilder.Forms;
using System.Windows.Forms;
using System.Threading;

namespace ForgeModBuilder.Managers
{
    public static class ForgeVersionManager
    {

        public static string MCPVersionsURL { get; private set; } = "http://files.minecraftforge.net/maven/de/oceanlabs/mcp/versions.json";
        public static string ForgeVersionsURL { get; private set; } = "http://files.minecraftforge.net/maven/net/minecraftforge/forge/index_MCVERSION.json";
        public static string MCVersionsURL { get; private set; } = "https://raw.githubusercontent.com/CJMinecraft01/ForgeModBuilder/rewrite/mc_versions.json";
        public static string PromotionsSlimURL { get; private set; } = "http://files.minecraftforge.net/maven/net/minecraftforge/forge/promotions_slim.json";

        public static string MCPVersionsFileName { get; private set; } = "mcp_versions.json";
        public static string ForgeVersionsFileName { get; private set; } = "forge_versions.json";
        public static string MCVersionsFileName { get; private set; } = "mc_versions.json";
        public static string RecommendedVersionsFileName { get; private set; } = "recommended_versions.json";

        public static Dictionary<string, Dictionary<string, List<string>>> MCPVersions { get; private set; }
        public static Dictionary<string, List<string>> ForgeVersions { get; private set; }
        public static List<string> MCVersions { get; private set; }
        public static List<string> RecommendedVersions { get; private set; }

        public static void SaveVersions()
        {
            ClientManager.WriteCustomData<Dictionary<string, Dictionary<string, List<string>>>>(MCPVersions, MCPVersionsFileName);
            ClientManager.WriteCustomData<Dictionary<string, List<string>>>(ForgeVersions, ForgeVersionsFileName);
            ClientManager.WriteCustomData<List<string>>(MCVersions, MCVersionsFileName);
            ClientManager.WriteCustomData<List<string>>(RecommendedVersions, RecommendedVersionsFileName);
        }

        public static void LoadVersions()
        {
            ClientManager.CreateCustomDataFileIfNotFound(MCPVersionsFileName);
            ClientManager.CreateCustomDataFileIfNotFound(ForgeVersionsFileName);
            ClientManager.CreateCustomDataFileIfNotFound(MCVersionsFileName);
            ClientManager.CreateCustomDataFileIfNotFound(RecommendedVersionsFileName);
            MCPVersions = ClientManager.ReadCustomData<Dictionary<string, Dictionary<string, List<string>>>>(MCPVersionsFileName);
            ForgeVersions = ClientManager.ReadCustomData<Dictionary<string, List<string>>>(ForgeVersionsFileName);
            MCVersions = ClientManager.ReadCustomData<List<string>>(MCVersionsFileName);
            RecommendedVersions = ClientManager.ReadCustomData<List<string>>(RecommendedVersionsFileName);
        }

        public static void UpdateVersionLists()
        {
            ProgressBarForm form = new ProgressBarForm();
            form.ProgressBar.Maximum = 300;
            form.TaskDetailsLabel.Text = LanguageManager.CurrentLanguage.Localize("form.update.label.task_details.version_syncing");
            form.Paint += (sender, e) => {
                if(form.ProgressBar.Value == form.ProgressBar.Maximum)
                {
                    form.Close();
                }
            };
            form.CancelButton.Click += (sender, e) =>
            {
                form.Close();
            };
            Task<bool> task = UpdateLists(form);
            Application.Run(form);
        }

        private static async Task<bool> UpdateLists(ProgressBarForm form)
        {
            DownloadMCPVersionList(form);
            DownloadMCVersionList(form);
            DownloadRecommendedVersionList(form);
            DownloadForgeVersionList(form);
            return true;
        }

        private static void DownloadMCPVersionList(ProgressBarForm form)
        {
            JsonSerializer js = new JsonSerializer();
            using (StreamReader sr = new StreamReader(WebRequest.Create(MCPVersionsURL).GetResponse().GetResponseStream()))
            using (JsonReader jr = new JsonTextReader(sr))
            {
                MCPVersions = js.Deserialize<Dictionary<string, Dictionary<string, List<string>>>>(jr);
                jr.Close();
            }
            form.ProgressBar.Value += 100;
        }

        private static void DownloadMCVersionList(ProgressBarForm form)
        {
            JsonSerializer js = new JsonSerializer();
            using (StreamReader sr = new StreamReader(WebRequest.Create(MCVersionsURL).GetResponse().GetResponseStream()))
            using (JsonReader jr = new JsonTextReader(sr))
            {
                MCVersions = js.Deserialize<List<string>>(jr);
                jr.Close();
            }
            form.ProgressBar.Value += 100;
        }

        private static void DownloadRecommendedVersionList(ProgressBarForm form)
        {
            JsonSerializer js = new JsonSerializer();
            using (StreamReader sr = new StreamReader(WebRequest.Create(PromotionsSlimURL).GetResponse().GetResponseStream()))
            using (JsonReader jr = new JsonTextReader(sr))
            {
                JObject data = js.Deserialize<JObject>(jr);
                Dictionary<string, string> promos = JsonConvert.DeserializeObject<Dictionary<string, string>>(data.SelectToken("promos").ToString());
                RecommendedVersions = new List<string>();
                foreach (string MinecraftVersion in promos.Keys)
                {
                    if (MinecraftVersion.Contains("-recommended"))
                    {
                        RecommendedVersions.Add(promos[MinecraftVersion]);
                        continue;
                    }
                }
                jr.Close();
            }
            form.ProgressBar.Value += 100;
        }

        private static void DownloadForgeVersionList(ProgressBarForm form)
        {
            List<string> VersionsToSync = OptionsManager.GetOption<List<string>>("VersionsToSync", MCVersions);
            form.ProgressBar.Value = 0;
            form.ProgressBar.Maximum = VersionsToSync.Count;
            foreach (string Version in VersionsToSync)
            {
                JObject data;
                JsonSerializer js = new JsonSerializer();
                using (StreamReader sr = new StreamReader(WebRequest.Create(ForgeVersionsURL.Replace("MCVERSION", Version)).GetResponse().GetResponseStream()))
                using (JsonReader jr = new JsonTextReader(sr))
                {
                    data = js.Deserialize<JObject>(jr);
                    jr.Close();
                }
                JArray ForgeVersionsData = (JArray)data.SelectToken("md").SelectToken("versions");

                if (OptionsManager.ForcedCreate || (string)ForgeVersionsData.First().SelectToken("version") != ForgeVersions[Version].First())
                {
                    List<string> BuildVersions = new List<string>();
                    foreach (JObject ForgeVersionObject in ForgeVersionsData)
                    {
                        BuildVersions.Add((string)ForgeVersionObject.SelectToken("version"));
                    }
                    ForgeVersions.Remove(Version);
                    ForgeVersions.Add(Version, BuildVersions);
                }
                form.ProgressBar.Value += 1;
            }
        }

    }
}
