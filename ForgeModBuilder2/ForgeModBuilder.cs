using ForgeModBuilder.Forms;
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
            PreInit();
            if (Init()) return;
            PostInit();
        }

        public static void PreInit()
        {
            //Setup values
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            MainFormInstance = new MainForm();
            MainFormInstance.FormClosed += CloseForm;
            OptionsManager.LoadOptions();
            LanguageManager.InitLanguages();
        }

        public static bool Init()
        {
            if(InstallationManager.CheckForUpdates()) return true;
            return false;
        }

        public static void PostInit()
        {
            Application.Run(MainFormInstance);
        }

        public static void CloseForm(object sender, EventArgs e)
        {
            OptionsManager.SaveOptions();
        }

    }
}
