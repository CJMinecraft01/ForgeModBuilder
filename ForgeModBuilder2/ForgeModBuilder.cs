using ForgeModBuilder.Forms;
using ForgeModBuilder.Managers;
using System;
using System.Diagnostics;
using System.Net;
using System.Windows.Forms;

/// <summary>
/// The main namespace for the forge mod builder. 
/// All the code inside of this namespace has been wrote by CJMinecraft. 
/// Do not copy any of this code or claim it as your own without seeking permission from CJMinecraft himself via email
/// (cjminecraft.theclever.me@gmail.com)
/// FMB is the abbrieviation of Forge Mod Builder
/// </summary>
namespace ForgeModBuilder
{
    /// <summary>
    /// The main class which runs the initialisation methods and sets up all of the processes used by FMB.
    /// </summary>
    public static class ForgeModBuilder
    {

        /// <summary>
        /// The instance of the main form. 
        /// </summary>
        public static MainForm MainFormInstance { get; private set; }
        /// <summary>
        /// States whether the program is being currently be debugged (if so, add some extra console messages).
        /// </summary>
        public static bool Debugging = Debugger.IsAttached;

        /// <summary>
        /// The main method which sets up everything and runs the program!
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                if (PreInit()) return; 
                if (Init()) return;
                PostInit();
            }
            catch (Exception e)
            {
                // Crash handling
                Console.WriteLine(e.ToString());
                Close(true);
            }
        }

        public static bool PreInit()
        {
            //Setup values
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            Console.WriteLine("Redirecting output!");
            ConsoleManager.RedirectOutput();
            Console.WriteLine("Redirected output!");

            OptionsManager.LoadOptions();
            ForgeVersionManager.LoadVersions();
            if (LanguageManager.InitLanguages()) return true;

            MainFormInstance = new MainForm();
            MainFormInstance.FormClosed += CloseForm;

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
            Application.Run(MainFormInstance);
        }

        public static void Close(bool crashed = false)
        {
            OptionsManager.SaveOptions();
            ForgeVersionManager.SaveVersions();
            ProjectManager.SaveProjects();
            ConsoleManager.SaveConsole(crashed);
        }

        // Move into Form class
        public static void CloseForm(object sender, EventArgs e)
        {
            Close();
        }


    }
}
