using ForgeModBuilder.Gradle;
using ForgeModBuilder.Managers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace ForgeModBuilder.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            InitialiseEventHandlers();
            SizeChanged += ResizeForm;
        }

        private void ResizeForm(object sender, EventArgs e)
        {
            CommandEntryTextBox.Width = ConsoleBottomPanel.Width - ExecuteCommandButton.Width - 4;
            ExecuteCommandButton.Location = new Point(CommandEntryTextBox.Width + 4, ExecuteCommandButton.Location.Y);
        }

        private void InitialiseEventHandlers()
        {
            LastConsoleMessageLabel.TextChanged += LastConsoleMessageLabelTextChanged;
            ExecuteCommandButton.Click += ExecuteCommandButtonClick;
            ConsoleTextBox.LinkClicked += ConsoleTextBoxLinkClicked;
        }

        private void LastConsoleMessageLabelTextChanged(object sender, EventArgs e)
        {
            Image fakeImage = new Bitmap(1, 1);
            Graphics graphics = Graphics.FromImage(fakeImage);
            SizeF size = graphics.MeasureString(LastConsoleMessageLabel.Text, LastConsoleMessageLabel.Font);
            LastConsoleMessageLabel.Location = new Point(ClientRectangle.Width - (int)size.Width - 5, LastConsoleMessageLabel.Location.Y);
        }

        private void ExecuteCommandButtonClick(object sender, EventArgs e)
        {
            GradleExecuter.RunGradleCommand(CommandEntryTextBox.Text);
            CommandEntryTextBox.Text = "";
        }

        private void ConsoleTextBoxLinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }
    }
}
