using ForgeModBuilder.Managers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ForgeModBuilder.Gradle
{
    public static class GradleExecuter
    {
        public static bool CurrentlyExecuting { get; private set; } = false;
        private static Process CurrentProcess;
        private static bool UsingDelegate = false;

        public static void RunGradleCommand(string command)
        {
            if (ProjectManager.CurrentProject == null)
            {
                MessageBox.Show("You don't have a project selected!", "Please select a project", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (command.StartsWith("gradlew"))
            {
                command = command.Replace("gradlew","").Trim();
            }
            if (!CurrentlyExecuting)
            {
                CurrentProcess = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                startInfo.FileName = ProjectManager.CurrentProject.Path + "gradlew.bat";
                startInfo.Arguments = command;
                startInfo.WorkingDirectory = ProjectManager.CurrentProject.Path;
                startInfo.RedirectStandardOutput = true;
                startInfo.RedirectStandardError = true;
                startInfo.UseShellExecute = false;
                startInfo.CreateNoWindow = true;
                CurrentProcess.StartInfo = startInfo;
                CurrentProcess.OutputDataReceived += GradleOutputReceived;
                CurrentProcess.ErrorDataReceived += GradleOutputReceived;
                CurrentProcess.Start();
                CurrentlyExecuting = true;
                if (ForgeModBuilder.MainFormInstance != null)
                {
                    ForgeModBuilder.MainFormInstance.ExecuteCommandButton.Enabled = false;
                }
                CurrentProcess.BeginOutputReadLine();
                CurrentProcess.BeginErrorReadLine();
            }
            else
            {
                if (MessageBox.Show("A gradle process is already running", "Gradle process already running", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
                {
                    RunGradleCommand(command);
                }
            }
        }

        delegate void GradleOutputReceivedCallback(object sender, DataReceivedEventArgs e);

        private static void GradleOutputReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                if (!UsingDelegate)
                {
                    ClientManager.Output(e.Data, true);
                }
                if (e.Data.Contains("BUILD SUCCESSFUL") || e.Data.Contains("BUILD FAILED"))
                {
                    CurrentlyExecuting = false;
                    if (ForgeModBuilder.MainFormInstance != null)
                    {
                        if (ForgeModBuilder.MainFormInstance.ExecuteCommandButton.InvokeRequired)
                        {
                            GradleOutputReceivedCallback callback = new GradleOutputReceivedCallback(GradleOutputReceived);
                            UsingDelegate = true;
                            ForgeModBuilder.MainFormInstance.Invoke(callback, new object[] { sender, e });
                        }
                        else
                        {
                            UsingDelegate = false;
                            ForgeModBuilder.MainFormInstance.ExecuteCommandButton.Enabled = true;
                        }
                    }
                }
            }
            else
            {
                ClientManager.Output("");
            }
        }

    }
}
