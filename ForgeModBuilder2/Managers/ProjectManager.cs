using System.Collections.Generic;
using System.IO;

namespace ForgeModBuilder.Managers
{
    public static class ProjectManager
    {

        public static List<Project> Projects { get; private set; } = new List<Project>();

        public static string ProjectsFileName { get; private set; } = "projects.json";

        public static void LoadProjects()
        {
            ClientManager.CreateCustomDataFileIfNotFound(ProjectsFileName);
            Projects = ClientManager.ReadCustomData<List<Project>>(ProjectsFileName);
        }

        public static void SaveProjects()
        {
            ClientManager.WriteCustomData<List<Project>>(Projects, ProjectsFileName);
        }

        public static void OpenProject(string Name, string Path)
        {
            if (Directory.Exists(Path) && File.Exists(Path + "gradlew.bat") && File.Exists(Path + "build.gradle"))
            {
                string[] data = File.ReadAllLines(Path + "build.gradle");
            }
        }

    }

    public class Project
    {
        public string Name { get; private set; }
        public string Path { get; private set; }
        public string MinecraftVersion { get; private set; }
        public string ForgeVersion { get; private set; }
        public string MCPMapping { get; private set; }

        public Project(string name, string path, string minecraftVersion, string forgeVersion, string mcpMapping)
        {
            Name = name;
            Path = path;
            MinecraftVersion = minecraftVersion;
            ForgeVersion = forgeVersion;
            MCPMapping = mcpMapping;
        }
    }
}
