using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ForgeModBuilder
{
    public partial class SetupProjectMenu : Form
    {

        public static string ShowSetupProjectMenu()
        {
            SetupProjectMenu menu = new SetupProjectMenu();
            return menu.ShowDialog() == DialogResult.OK ? menu.Editor.Text : "";
        }

        public SetupProjectMenu()
        {
            InitializeComponent();
            AcceptButton = OKButton;
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
