﻿using ForgeModBuilder.Forms;
using ForgeModBuilder.Gradle;
using ForgeModBuilder.Managers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ForgeModBuilder
{
    public static class ForgeModBuilder
    {

        public static MainForm MainFormInstance { get; private set; }
        public static bool Debugging = Debugger.IsAttached;

        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (PreInit()) return;
            if (Init()) return;
            PostInit();
        }

        public static bool PreInit()
        {
            //Setup values
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            OptionsManager.LoadOptions();
            ForgeVersionManager.LoadVersions();
            if (LanguageManager.InitLanguages()) return true;

            return false;
        }

        public static bool Init()
        {
            if (InstallationManager.CheckForUpdates()) return true;
            ForgeVersionManager.UpdateVersionLists();

            ProjectManager.LoadProjects();            

            return false;
        }

        public static void PostInit()
        {
            MainFormInstance = new MainForm();
            MainFormInstance.FormClosed += CloseForm;

            MainFormInstance.OpenProjectButton.Click += (sender, e) => {
                ProjectManager.OpenProject("CJCore 1.12.2", @"C:\Users\Callum\Coding\Minecraft\Minecraft Mods\CJMinecraft Core\1.12.2\");
                ProjectManager.CurrentProject = ProjectManager.Projects[0];
                GradleExecuter.RunGradleCommand("build");
            };

            Application.Run(MainFormInstance);
        }

        public static void CloseForm(object sender, EventArgs e)
        {
            OptionsManager.SaveOptions();
            ForgeVersionManager.SaveVersions();
            ProjectManager.SaveProjects();
        }

    }
}
