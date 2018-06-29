﻿using ForgeModBuilder.Gradle;
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

        private static GBlock Decode(List<List<string>> data)
        {
            GBlock block = new GBlock();
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
                    if (chunk.StartsWith("//"))
                    {
                        // A comment so rest of the data should be ignored
                        break;
                    }
                    else if (chunk == "{")
                    {
                        // Opening a block or a task
                        if (nestedLevel == 0)
                        {
                            if (j > 0)
                            {
                                nestedName = dataChunk[j - 1];
                            }
                            else
                            {
                                nestedName = data[i - 1].Last();
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
                            GBlock newBlock = Decode(nestedData);
                            newBlock.Name = nestedName;
                            block.Children.Add(nestedName, newBlock);
                            nestedData.Clear();
                            nestedName = string.Empty;
                        }
                    }
                    else if (chunk == "=")
                    {
                        // Assigning a variable
                        if (j > 0)
                        {
                            int _int;
                            if (int.TryParse(dataChunk[j + 1], out _int))
                            {
                                if (!block.Children.ContainsKey(dataChunk[j - 1]))
                                    block.Children.Add(dataChunk[j - 1], new GVariable(dataChunk[j - 1], _int));
                                continue;
                            }
                            float _float;
                            if (float.TryParse(dataChunk[j + 1], out _float))
                            {
                                if (!block.Children.ContainsKey(dataChunk[j - 1]))
                                    block.Children.Add(dataChunk[j - 1], new GVariable(dataChunk[j - 1], _float));
                                continue;
                            }
                            bool _bool;
                            if (bool.TryParse(dataChunk[j + 1], out _bool))
                            {
                                if (!block.Children.ContainsKey(dataChunk[j - 1]))
                                    block.Children.Add(dataChunk[j - 1], new GVariable(dataChunk[j - 1], _bool));
                                continue;
                            }
                            if (dataChunk[j + 1].StartsWith("\"") && dataChunk[j + 1].EndsWith("\""))
                            {
                                if (!block.Children.ContainsKey(dataChunk[j - 1]))
                                    block.Children.Add(dataChunk[j - 1], new GVariable(dataChunk[j - 1], dataChunk[j + 1].Substring(1, dataChunk[j + 1].Length - 2)));
                                continue;
                            }
                            else if (dataChunk[j + 1].StartsWith("\'") && dataChunk[j + 1].EndsWith("\'"))
                            {
                                if (!block.Children.ContainsKey(dataChunk[j - 1]))
                                    block.Children.Add(dataChunk[j - 1], new GVariable(dataChunk[j - 1], dataChunk[j + 1].Substring(1, dataChunk[j + 1].Length - 2)));
                                continue;
                            }
                            else if (block.Children.ContainsKey(dataChunk[j + 1]))
                            {
                                if (block.Children[dataChunk[j + 1]] is GVariable)
                                    if (!block.Children.ContainsKey(dataChunk[j - 1]))
                                        block.Children.Add(dataChunk[j - 1], new GVariable(dataChunk[j - 1], ((GVariable)block.Children[dataChunk[j + 1]]).Value));
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
            Console.WriteLine(block);

            return block;
        }
    }

    public class GObject
    {
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
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
            return Name + " = " + Value;
        }
    }

    public class GBlock : GObject
    {
        public Dictionary<string, GObject> Children { get; private set; } = new Dictionary<string, GObject>();

        public override string ToString()
        {
            string text = "";
            foreach (GObject child in Children.Values)
            {
                text += child + "\n";
            }
            return text;
        }
    }

    public class GTask : GBlock
    {

    }

}
