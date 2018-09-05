using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForgeModBuilder.Managers
{
    /// <summary>
    /// Manages everything to do with the console. Including redirecting the output to a file, and outputting to the console in the main form.
    /// </summary>
    public static class ConsoleManager
    {
        public static string ConsoleLogPath = ClientManager.ClientDataPath + @"Logs\";

        public static void RedirectOutput()
        {
            if (!Directory.Exists(ConsoleLogPath))
            {
                Directory.CreateDirectory(ConsoleLogPath);
            }

            FileStream stream = new FileStream(ConsoleLogPath + "latest.log", FileMode.OpenOrCreate, FileAccess.Write);
            ConsoleStreamWriter writer = new ConsoleStreamWriter(Console.Out, stream);
            writer.AutoFlush = true;
            Console.SetOut(writer);
        }

        public static void SaveConsole(bool crashed)
        {
            Console.Out.Close();
            string fileName;
            if (crashed)
                fileName = "crash-";
            else
                fileName = "log-";

            fileName += $"{DateTime.Now.ToString("yy.dd.MM-hh.mm.ss")}.log";
            File.Move(ConsoleLogPath + "latest.log", ConsoleLogPath + fileName);
        }

    }

    class ConsoleStreamWriter : StreamWriter
    {
        private TextWriter OldConsole;
        private string CurrentLine = "";
        private List<string> MissingLines = new List<string>();

        public ConsoleStreamWriter(TextWriter oldConsole, Stream stream) : base(stream)
        {
            this.OldConsole = oldConsole;
        }

        public override void WriteLine(string value)
        {
            if (value == NewLine)
            {
                if (ForgeModBuilder.MainFormInstance == null)
                {
                    MissingLines.Add(CurrentLine);
                }
                if (!string.IsNullOrWhiteSpace(CurrentLine))
                {
                    ChangeLastConsoleMessageLabelText(CurrentLine);
                    CurrentLine = "";
                }
            }
            else
            {
                CurrentLine += value;
            }
            
            if (ForgeModBuilder.MainFormInstance != null)
            {
                MissingLines.ForEach(line => AppendTextToFormConsole(line + "\n"));
                AppendTextToFormConsole(value + "\n");
                MissingLines.Clear();
            }
            OldConsole.WriteLine(value);
            base.WriteLine(value);
        }

        delegate void ChangeLastConsoleMessageLabelTextCallback(string text);

        private void ChangeLastConsoleMessageLabelText(string text)
        {
            if (ForgeModBuilder.MainFormInstance == null) return;
            if (ForgeModBuilder.MainFormInstance.LastConsoleMessageLabel.InvokeRequired)
            {
                ChangeLastConsoleMessageLabelTextCallback callback = new ChangeLastConsoleMessageLabelTextCallback(ChangeLastConsoleMessageLabelText);
                ForgeModBuilder.MainFormInstance.Invoke(callback, text);
            }
            else
            {
                ForgeModBuilder.MainFormInstance.LastConsoleMessageLabel.Text = text;
            }
        }

        delegate void AppendTextToFormConsoleCallback(string text);

        private void AppendTextToFormConsole(string text)
        {
            if (ForgeModBuilder.MainFormInstance == null) return;
            if (ForgeModBuilder.MainFormInstance.ConsoleTextBox.InvokeRequired)
            {
                AppendTextToFormConsoleCallback callback = new AppendTextToFormConsoleCallback(AppendTextToFormConsole);
                ForgeModBuilder.MainFormInstance.Invoke(callback, text);
            }
            else
            {
                ForgeModBuilder.MainFormInstance.ConsoleTextBox.ReadOnly = false;
                ForgeModBuilder.MainFormInstance.ConsoleTextBox.AppendText(text);
                ForgeModBuilder.MainFormInstance.ConsoleTextBox.ReadOnly = true;
                ForgeModBuilder.MainFormInstance.ConsoleTextBox.ScrollToCaret();
            }
        }
    }
}
