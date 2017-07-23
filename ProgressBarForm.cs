using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ForgeModBuilder
{
    public partial class ProgressBarForm : Form
    {
        public static ProgressBarForm ShowProgressBar(string title, string progressBarText, Font progressBarFont)
        {
            ProgressBarForm form = new ProgressBarForm();
            form.Text = title;
            form.ProgressBar.Text = progressBarText;
            form.ProgressBar.Font = progressBarFont;
            form.Show();
            return form;
        }

        public ProgressBarForm()
        {
            InitializeComponent();
        }
    }
}
