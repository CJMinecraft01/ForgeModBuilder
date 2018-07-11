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
    public partial class AddGroupForm : Form
    {
        public AddGroupForm()
        {
            InitializeComponent();
            GroupNameTextBox.TextChanged += GroupNameTextBoxTextChanged;
        }

        private void GroupNameTextBoxTextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(GroupNameTextBox.Text))
            {
                AddButton.Enabled = false;
                return;
            }
            foreach (ListViewGroup group in ForgeModBuilder.MainFormInstance.ProjectsListView.Groups)
            {
                if (group.Header == GroupNameTextBox.Text)
                {
                    AddButton.Enabled = false;
                    return;
                }
            }
            AddButton.Enabled = true;
        }
    }
}
