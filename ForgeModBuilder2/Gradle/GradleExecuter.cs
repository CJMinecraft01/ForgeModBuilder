using ForgeModBuilder.Managers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForgeModBuilder.Gradle
{
    public static class GradleExecuter
    {

        private static List<string> CommandQueue = new List<string>();
        private static Process CurrentProcess;

        public static bool Waiting { get; private set; } = true;

        public static void RunGradleCommand(string command)
        {
            CommandQueue.Add(command);
            if (Waiting)
            {
                Task<bool> task = ExecuteCommands();
            }
        }

        private static async Task<bool> ExecuteCommands()
        {
            if (ProjectManager.CurrentProject == null)
            {
                return false;
            }
            Waiting = false;
            while (CommandQueue.Count > 0)
            {
                CurrentProcess = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                startInfo.FileName = ProjectManager.CurrentProject.Path + "gradlew.bat";
                startInfo.Arguments = CommandQueue.First();
                startInfo.RedirectStandardOutput = true;
                startInfo.RedirectStandardError = true;
                startInfo.UseShellExecute = false;
                startInfo.CreateNoWindow = true;
                CurrentProcess.StartInfo = startInfo;
                CurrentProcess.OutputDataReceived += GradleOutputReceived;
                CurrentProcess.ErrorDataReceived += GradleOutputReceived;
                CurrentProcess.Start();
                CurrentProcess.BeginOutputReadLine();
                CurrentProcess.BeginErrorReadLine();
                CurrentProcess.WaitForExit();
                CurrentProcess.Close();
                CurrentProcess = null;
                CommandQueue.Remove(CommandQueue.First());
            }
            Waiting = true;
            return true;
        }

        private static void GradleOutputReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                ClientManager.Output(e.Data, true);
            }
            else
            {
                ClientManager.Output("");
            }
        }

        public static void ClearCommandQueue()
        {
            CommandQueue.Clear();
            if (CurrentProcess != null)
            {
                CurrentProcess.Close();
            }
        }

    }
}
