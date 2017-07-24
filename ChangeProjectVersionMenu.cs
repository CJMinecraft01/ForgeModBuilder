using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ForgeModBuilder
{
    public partial class ChangeProjectVersionMenu : Form
    {

        public static ChangeProjectVersionMenu ShowChangeProjectVersionMenu()
        {
            if (Program.INSTANCE.CurrentProject != null)
            {
                ChangeProjectVersionMenu menu = new ChangeProjectVersionMenu();
                menu.Show();
                return menu;
            }
            else
            {
                MessageBox.Show("Please open a project!", "No open project", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public ChangeProjectVersionMenu()
        {
            InitializeComponent();
            NewProjectMenu.SetupVersions();
            LoadVersions();
        }

        //Load the versions from the synced versions. Syncsing should have been done previously
        public void LoadVersions()
        {
            //Reset the holders of the versions
            ForgeVersions.Items.Clear();
            foreach (string forgeVersion in NewProjectMenu.Versions[Program.INSTANCE.CurrentProject.mcVersion])
            {
                ForgeVersions.Items.Add(forgeVersion); //Add each forge version
            }
            ForgeVersions.SelectedIndex = 0;
        }

        private void ChangeProjectVersionButton_Click(object sender, EventArgs e)
        {
            Program.INSTANCE.UpdateProject(((string)ForgeVersions.SelectedItem).Split('★')[0], false);
        }
    }
}
