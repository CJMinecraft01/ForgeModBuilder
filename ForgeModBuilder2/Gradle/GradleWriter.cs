using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForgeModBuilder.Gradle
{
    public static class GradleWriter
    {
        public static void WriteFile(string Path, GBlock file)
        {
            // The only thing we change is the value of variables.
            // We should go through and read the file and check to see what has changed and only replace what has changed.

            GBlock originalFile = GradleParser.ReadFile(Path);
            List<GVariable> originalVariables = originalFile.SelectChildren<GVariable>(true);
            List<GVariable> newVariables = file.SelectChildren<GVariable>(true);
            List<GVariable> changedVariables = new List<GVariable>();
            foreach (GVariable originalVariable in originalVariables)
            {
                foreach (GVariable newVariable in newVariables)
                {
                    if (newVariable.Equals(originalVariable))
                    {
                        // The variable is exactly the same as the original one, we don't need to update it then
                        break;
                    }
                    else if (newVariable.Name == originalVariable.Name && newVariable.NestedLevel == originalVariable.NestedLevel)
                    {
                        // The value must have been changed so we should update it
                        changedVariables.Add(newVariable);
                        break;
                    }
                }
            }

            originalVariables.ForEach(v => Console.WriteLine(v));
            Console.WriteLine("=====");
            newVariables.ForEach(v => Console.WriteLine(v));
            Console.WriteLine("=====");
            changedVariables.ForEach(v => Console.WriteLine(v));

            // We now need to overwrite the variables which were changed
            string[] lines = File.ReadAllLines(Path);
            List<string> newLines = new List<string>();

            foreach (string line in lines)
            {
                List<string> dataChunk = GradleParser.GetDataFromLine(line, false, false);
                string newLine = "";
                bool comment = false;
                int numVariables = 0;
                for (int i = 0; i < dataChunk.Count; i++)
                {
                    string chunk = dataChunk[i];
                    if (comment)
                    {
                        newLine += chunk + " ";
                    }
                    else if (chunk == "//")
                    {
                        // Comment, should still be added. The rest of the lines is the comment
                        comment = true;
                        newLine += chunk + " ";
                    }
                    else if (i < dataChunk.Count - 2 && dataChunk[i + 1] == "=")
                    {
                        numVariables++;
                        string variableName = chunk;
                        bool neededToBeChanged = false;
                        for (int j = 0; j < changedVariables.Count; j++)
                        {
                            if (changedVariables[j].Name == variableName)
                            {
                                if (numVariables > 1)
                                {
                                    newLine += "\n" + changedVariables[j].GetTab();
                                }
                                if (changedVariables[j].Value is string)
                                {
                                    string variable = variableName + " = " + "\"" + changedVariables[j].Value + "\"";
                                    newLine += variable + " ";
                                }
                                else
                                {
                                    string variable = variableName + " = " + changedVariables[j].Value;
                                    newLine += variable + " ";
                                }
                                neededToBeChanged = true;
                                changedVariables.RemoveAt(j);
                                break;
                            }
                        }
                        if (!neededToBeChanged)
                        {
                            newLine += chunk + " = " + dataChunk[i + 2] + " ";
                        }
                    }
                    else if (chunk != "=")
                    {
                        if (i > 1 && dataChunk[i - 1] == "=")
                        {
                            continue;
                        }
                        newLine += chunk + " ";
                    }
                }
                newLines.Add(newLine.TrimEnd());
            }

            File.WriteAllLines(Path, newLines.ToArray());
        }
    }
}
