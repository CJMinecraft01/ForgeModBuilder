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
            List<GVariable> originalVariables = originalFile.SelectChildren<GVariable>();
            List<GVariable> newVariables = file.SelectChildren<GVariable>();
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

            // We now need to overwrite the variables which were changed
            string[] lines = File.ReadAllLines(Path);
            List<string> newLines = new List<string>();

            foreach (string line in lines)
            {
                if (line.Contains('='))
                {
                    int beginVariableIndex = -1;
                    int equalSignIndex = line.IndexOf('=');
                    string variableName = "";
                    for (int i = equalSignIndex - 1; i > 0; i--)
                    {
                        if (char.IsWhiteSpace(line[i]) && i != equalSignIndex - 1)
                        {
                            variableName = line.Substring(i, equalSignIndex - i - 1);
                            beginVariableIndex = i;
                            break;
                        }
                    }
                    if (string.IsNullOrWhiteSpace(variableName) || beginVariableIndex == -1)
                    {
                        newLines.Add(line);
                        continue;
                    }
                    for (int i = 0; i < changedVariables.Count; i++)
                    {
                        if (changedVariables[i].Name == variableName)
                        {
                            if (changedVariables[i].Value is string)
                            {
                                string newLine = line.Substring(0, beginVariableIndex);
                                string variable = variableName + " = " + "\"" + changedVariables[i].Value + "\"";
                                Console.WriteLine(newLine + variable);
                                newLines.Add(newLine + variable);
                            }
                            else
                            {
                                string newLine = line.Substring(0, beginVariableIndex);
                                string variable = variableName + " = " + changedVariables[i].Value;
                                Console.WriteLine(newLine + variable);
                                newLines.Add(newLine + variable);
                            }
                            break;
                        }
                    }
                }
                else
                {
                    newLines.Add(line);
                }
            }

            File.WriteAllLines(Path, newLines.ToArray());
        }
    }
}
