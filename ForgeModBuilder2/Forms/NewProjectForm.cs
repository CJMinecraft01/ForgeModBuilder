using ForgeModBuilder.Managers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ForgeModBuilder.Forms
{
    public partial class NewProjectForm : Form
    {
        public NewProjectForm()
        {
            InitializeComponent();
            InitialiseEvents();
            MinecraftVerionsListBox.Items.AddRange(ForgeVersionManager.MCVersions.ToArray());
            MinecraftVerionsListBox.SelectedIndex = 0;
        }

        private void InitialiseEvents()
        {
            MinecraftVerionsListBox.SelectedIndexChanged += SelectMinecraftVersion;
            ArchiveBaseNameTextBox.TextChanged += GeneratedArchiveNameTextChanged;
            VersionTextBox.TextChanged += GeneratedArchiveNameTextChanged;
            GroupTextBox.TextChanged += CheckValidity;
            JavaVersionComboBox.TextChanged += CheckValidity;
            ForgeVersionsListBox.SelectedIndexChanged += CheckValidity;
        }

        private void GeneratedArchiveNameTextChanged(object sender, EventArgs e)
        {
            GeneratedArchiveNameLabel.Text = ArchiveBaseNameTextBox.Text + "-" + VersionTextBox.Text + ".jar";
            CheckValidity(sender, e);
        }

        private void CheckValidity(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ArchiveBaseNameTextBox.Text) || string.IsNullOrWhiteSpace(VersionTextBox.Text) || string.IsNullOrWhiteSpace(GroupTextBox.Text) || string.IsNullOrWhiteSpace(JavaVersionComboBox.Text) || ForgeVersionsListBox.SelectedItem == null)
            {
                CreateButton.Enabled = false;
            }
            else
            {
                CreateButton.Enabled = true;
            }
        }

        private void SelectMinecraftVersion(object sender, EventArgs e)
        {
            if (ForgeVersionManager.ForgeVersions.ContainsKey((string)MinecraftVerionsListBox.SelectedItem))
            {
                ForgeVersionsListBox.Items.Clear();
                string[] Versions = new string[ForgeVersionManager.ForgeVersions[(string)MinecraftVerionsListBox.SelectedItem].Count];
                for (int i = 0; i < ForgeVersionManager.ForgeVersions[(string)MinecraftVerionsListBox.SelectedItem].Count; i++)
                {
                    string ForgeVersion = ForgeVersionManager.ForgeVersions[(string)MinecraftVerionsListBox.SelectedItem][i];
                    if (ForgeVersionManager.RecommendedVersions.Contains(ForgeVersion))
                    {
                        Versions[i] = ForgeVersion + "★";
                    }
                    else
                    {
                        Versions[i] = ForgeVersion;
                    }
                }
                ForgeVersionsListBox.Items.AddRange(Versions);
            }
            CheckValidity(null, null);
        }
    }
}
