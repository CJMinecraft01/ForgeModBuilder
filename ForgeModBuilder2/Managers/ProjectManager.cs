using ForgeModBuilder.Gradle;
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
            // TODO ensure each variable is correct type
            // e.g. Try Catch
            if (!Path.EndsWith("\\"))
            {
                Path += "\\";
            }
            if (Directory.Exists(Path) && File.Exists(Path + "gradlew.bat") && File.Exists(Path + "build.gradle"))
            {
                Projects.Add(new Project(Name, Path));
            }
        }

        public static void UpdateProjectInfo(Project originalProject)
        {
            if (Directory.Exists(originalProject.Path) && File.Exists(originalProject.Path + "gradlew.bat") && File.Exists(originalProject.Path + "build.gradle"))
            {
                Project newProject = new Project(originalProject.Name, originalProject.Path);
                Projects.Remove(originalProject);
                Projects.Add(newProject);
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
        public bool HasMCPMapping { get; private set; }
        public string ModVersion { get; private set; }
        public string ModGroup { get; private set; }
        public string ModArchivesBaseName { get; private set; }

        public Project(string name, string path)
        {
            Name = name;
            Path = path;
            GBlock file = GradleParser.ReadBuildFile(Path + "build.gradle");
            ModVersion = (string)file.SelectChild<GVariable>("version").Value;
            ModGroup = (string)file.SelectChild<GVariable>("group").Value;
            ModArchivesBaseName = (string)file.SelectChild<GVariable>("archivesBaseName").Value;
            string[] versionsDetails = GetVersionDetails((string)file.SelectChild<GBlock>("minecraft").SelectChild<GVariable>("version").Value);
            if (versionsDetails.Length != 2)
            {
                // this shouldn't happen! throw error
            }
            MinecraftVersion = versionsDetails[0];
            ForgeVersion = versionsDetails[1];
            if (file.SelectChild<GBlock>("minecraft").HasChild<GVariable>("mappings"))
            {
                HasMCPMapping = true;
                MCPMapping = (string)file.SelectChild<GBlock>("minecraft").SelectChild<GVariable>("mappings").Value;
            }
            else
            {
                HasMCPMapping = false;
            }
        }

        private static string[] GetVersionDetails(string data)
        {
            if (data.Contains("-"))
            {
                string[] versions = data.Split('-');
                if (versions.Length == 2)
                {
                    return versions;
                }
                else if (versions.Length == 3)
                {
                    return new string[] { versions[0], versions[1] };
                }
            }
            return new string[0];
        }
    }
}
