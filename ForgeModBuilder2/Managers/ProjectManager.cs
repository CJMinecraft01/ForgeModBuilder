using System.IO;
using System.Windows.Forms;

namespace ForgeModBuilder.Managers
{
    public static class ProjectManager
    {

        public static Project CurrentProject = null;

        public static void LoadProject(string path, bool successDialog)
        {
            if(!Directory.Exists(path))
            {
                MessageBox.Show(LanguageManager.CurrentLanguage.Localize("message_box.project.non_existant.desc"), LanguageManager.CurrentLanguage.Localize("message_box.project.non_existant.title"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

    }

    public class Project
    {
        public string Path;
        public string MinecraftVersion;
        public string ForgeVersion;
        public string MCPMapping;

        public Project(string path, string minecraftVersion, string forgeVersion, string mcpMapping)
        {
            this.Path = path;
            this.MinecraftVersion = minecraftVersion;
            this.ForgeVersion = forgeVersion;
            this.MCPMapping = mcpMapping;
        }
    }
}
