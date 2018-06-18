using ForgeModBuilder.Forms;
using ForgeModBuilder.Managers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
            Init();
            PostInit();
            Application.Run(MainFormInstance);
        }

        public static void PreInit()
        {
            MainFormInstance = new MainForm();
            MainFormInstance.FormClosed += CloseForm;
            OptionsManager.LoadOptions();
            LanguageManager.InitLanguages();
        }

        public static void Init()
        {

        }

        public static void PostInit()
        {

        }

        public static void CloseForm(object sender, EventArgs e)
        {
            OptionsManager.SaveOptions();
        }

    }
}
