using ForgeModBuilder.Gradle;
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
        public static GBlock ReadBuildFile(string Path)
        {
            return Decode(GetDataFromLines(File.ReadAllLines(Path)));
        }

        private static List<List<string>> GetDataFromLines(string[] lines)
        {
            List<List<string>> data = new List<List<string>>();
            foreach (string line in lines)
            {
                if (string.IsNullOrEmpty(line) || string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }
                string _line = line.TrimStart();
                List<string> dataChunk = new List<string>();
                int dataBegin = 0;
                for (int i = 0; i < _line.Length; i++)
                {
                    char c = _line[i];
                    if (char.IsWhiteSpace(c))
                    {
                        string info = _line.Substring(dataBegin, i - dataBegin);
                        if (info.StartsWith("//"))
                        {
                            // A comment so contains no useful data for the program
                            break;
                        }
                        dataChunk.Add(info);
                        dataBegin = i + 1;
                    }
                    if (i == _line.Length - 1)
                    {
                        dataChunk.Add(_line.Substring(dataBegin).TrimEnd());
                    }
                }
                data.Add(dataChunk);
            }
            return data;
        }

        private static GBlock Decode(List<List<string>> data, int NestedLevel = -1)
        {
            GBlock block = new GBlock();
            block.NestedLevel = NestedLevel;
            string nestedName = "";
            int nestedLevel = 0;
            List<List<string>> nestedData = new List<List<string>>();
            for (int i = 0; i < data.Count; i++)
            {
                List<string> dataChunk = data[i];
                if (nestedLevel != 0)
                {
                    nestedData.Add(dataChunk);
                }
                for (int j = 0; j < dataChunk.Count; j++)
                {
                    string chunk = dataChunk[j];
                    // Decode the data
                    if (chunk == "{")
                    {
                        // Opening a block or a task
                        if (nestedLevel == 0)
                        {
                            if (j > 0)
                            {
                                nestedName = dataChunk[j - 1];
                                if (nestedName.Contains("(") && nestedName.Contains(")"))
                                {
                                    // This must be using a function, meaning this is not a block
                                    continue;
                                }
                                if (j < dataChunk.Count)
                                {
                                    nestedData.Add(dataChunk.Skip(j + 1).ToList());
                                }
                            }
                            else
                            {
                                nestedName = data[i - 1].Last();
                                if (nestedName.Contains("(") && nestedName.Contains(")"))
                                {
                                    // This must be using a function, meaning this is not a block
                                    continue;
                                }
                            }
                        }
                        nestedLevel++;
                    }
                    else if (chunk == "}")
                    {
                        // Closing a block or a task
                        nestedLevel--;

                        if (nestedLevel == 0)
                        {
                            GBlock newBlock = Decode(nestedData, NestedLevel + 1);
                            Console.WriteLine(nestedName + " " + (NestedLevel + 1));
                            newBlock.Name = nestedName;
                            block.Children.Add(nestedName, newBlock);
                            nestedData.Clear();
                            nestedName = string.Empty;
                        }
                    }
                    else if (nestedLevel != 0)
                    {
                        continue;
                    }
                    else if (chunk == "=")
                    {
                        // Assigning a variable
                        if (j > 0)
                        {
                            GVariable variable = DecodeVariable(j, dataChunk, block);
                            variable.NestedLevel = NestedLevel + 1;
                            if (variable != null)
                            {
                                if (!block.Children.ContainsKey(variable.Name))
                                {
                                    block.Children.Add(variable.Name, variable);
                                }
                            }
                        }
                        else
                        {
                            // You can't begin data with an equals sign!
                            // This means there is an error in the format of the file!
                        }
                    }
                }
            }
            return block;
        }

        private static GVariable DecodeVariable(int equalChunkIndex, List<string> dataChunk, GBlock parentBlock)
        {
            object value = DecodeVariableValue(dataChunk[equalChunkIndex + 1]);
            if (value != null)
            {
                return new GVariable(dataChunk[equalChunkIndex - 1], value);
            }
            else if (parentBlock.Children.ContainsKey(dataChunk[equalChunkIndex + 1]) && parentBlock.Children[dataChunk[equalChunkIndex + 1]] is GVariable)
            {
                return new GVariable(dataChunk[equalChunkIndex - 1], ((GVariable)parentBlock.Children[dataChunk[equalChunkIndex + 1]]).Value);
            }
            else if (dataChunk.Count > equalChunkIndex + 3 && dataChunk[equalChunkIndex + 2] == "=")
            {
                if (!parentBlock.Children.ContainsKey(dataChunk[equalChunkIndex - 1]))
                    return new GVariable(dataChunk[equalChunkIndex - 1], DecodeVariable(equalChunkIndex + 2, dataChunk, parentBlock).Value);
            }
            return null;
        }

        private static object DecodeVariableValue(string chunk)
        {
            int _int;
            if (int.TryParse(chunk, out _int))
            {
                return _int;
            }
            float _float;
            if (float.TryParse(chunk, out _float))
            {
                return _float;
            }
            bool _bool;
            if (bool.TryParse(chunk, out _bool))
            {
                return _bool;
            }
            if (chunk.StartsWith("\"") && chunk.EndsWith("\""))
            {
                return chunk.Substring(1, chunk.Length - 2);
            }
            else if (chunk.StartsWith("\'") && chunk.EndsWith("\'"))
            {
                return chunk.Substring(1, chunk.Length - 2);
            }
            return null;
        }

    }

    public class GObject
    {
        public string Name { get; set; }
        public int NestedLevel { get; set; } = -1;

        public override string ToString()
        {
            return GetTab() + Name;
        }

        protected string GetTab()
        {
            string tab = "";
            if (NestedLevel > 0)
            {
                tab = new string(' ', NestedLevel * 4);
            }
            return tab;
        }
    }

    public class GVariable : GObject
    {
        public object Value { get; private set; }

        public GVariable(string name, object value)
        {
            Name = name;
            Value = value;
        }

        public override string ToString()
        {
            return GetTab() + Name + " = " + Value;
        }
    }

    public class GBlock : GObject
    {
        public Dictionary<string, GObject> Children { get; private set; } = new Dictionary<string, GObject>();

        public override string ToString()
        {
            string tab = GetTab();
            string text = tab + Name + " {\n";
            foreach (GObject child in Children.Values)
            {
                text += child + "\n";
            }
            text += tab + "}";
            return text;
        }
    }

    public class GTask : GBlock
    {

    }

}
