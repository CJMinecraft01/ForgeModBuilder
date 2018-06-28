using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ForgeModBuilder.Gradle
{
    // TODO Finish reader
    public static class GradleReader
    {
        public static GFile ReadBuildFile(string Path)
        {
            GFile file = new GFile();
            string[] data = File.ReadAllLines(Path);
            foreach (string line in data)
            {
                Console.WriteLine(line);
                if (string.IsNullOrWhiteSpace(line) || string.IsNullOrEmpty(line))
                {
                    continue;
                }
                if (line.Contains('=') && !line.Contains('{') && !line.Contains('}'))
                {
                    Console.WriteLine("Detected variable");
                    file.Variables.Add(new GVariable(line));
                    Console.WriteLine(file.Variables.Last().Name + " " + file.Variables.Last().Value);
                }
            }
            return file;
        }

        public static string RemoveInitialWhiteSpace(this string text)
        {
            return text.Substring(text.IndexOf(text.First(c => !char.IsWhiteSpace(c))));
        }

        public static string RemoveFinalWhiteSpace(this string text)
        {
            return text.Substring(0, text.IndexOf(text.Last(c => !char.IsWhiteSpace(c))));
        }
    }

    public class GFile
    {
        public List<GVariable> Variables { get; private set; } = new List<GVariable>();
        public List<GBlock> Blocks { get; private set; } = new List<GBlock>();
        public List<GTask> Tasks { get; private set; } = new List<GTask>();
    }

    public class GObject
    {
        public string Name { get; protected set; }
    }

    public class GVariable : GObject
    {
        public object Value { get; private set; }

        public GVariable(string line)
        {
            line = line.RemoveInitialWhiteSpace();
            string[] data = line.Split('=');

            Name = data[0].RemoveFinalWhiteSpace();
            int i;
            float j;
            bool k;
            if (data[1].Contains('\"') || data[1].Contains('\''))
            {
                string _string = data[1].RemoveInitialWhiteSpace().RemoveFinalWhiteSpace();
                _string = _string.Substring(1, _string.Length);
                Value = _string;
            }
            else if (int.TryParse(data[1].RemoveInitialWhiteSpace(), out i))
            {
                Value = i;
            }
            else if (float.TryParse(data[1].RemoveInitialWhiteSpace(), out j))
            {
                Value = j;
            }
            else if (bool.TryParse(data[1].RemoveInitialWhiteSpace(), out k))
            {
                Value = k;
            }
        }
    }

    public class GBlock : GObject
    {
        public List<GObject> Children { get; private set; } = new List<GObject>();
    }

    public class GTask : GBlock
    {

    }

}
