using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ForgeModBuilder.Gradle
{
    public static class GradleParser
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
                bool insideApostropheQuotes = false;
                bool insideSpeechQuotes = false;
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
                        continue;
                    }
                    if (i > 0 && !(insideApostropheQuotes || insideSpeechQuotes))
                    {
                        if (c == '{' && !char.IsWhiteSpace(_line[i - 1]))
                        {
                            dataChunk.Add(_line.Substring(dataBegin, i - dataBegin));
                            dataChunk.Add("{");
                            dataBegin = i + 1;
                            if (i + 1 == _line.Length - 1)
                            {
                                break;
                            }
                        }
                        else if (c == '}' && !char.IsWhiteSpace(_line[i - 1]))
                        {
                            dataChunk.Add(_line.Substring(dataBegin, i - dataBegin));
                            dataChunk.Add("}");
                            dataBegin = i + 1;
                            if (i + 1 == _line.Length - 1)
                            {
                                break;
                            }
                        }
                        else if (c == '=' && !char.IsWhiteSpace(_line[i - 1]))
                        {
                            dataChunk.Add(_line.Substring(dataBegin, i - dataBegin));
                            dataChunk.Add("=");
                            dataBegin = i + 1;
                            if (i + 1 == _line.Length - 1)
                            {
                                break;
                            }
                        }
                        else if (_line[i - 1] == '{' && !char.IsWhiteSpace(c))
                        {
                            if (dataChunk.Last() != "{")
                            {
                                dataChunk.Add("{");
                            }
                        }
                        else if (_line[i - 1] == '}' && !char.IsWhiteSpace(c))
                        {
                            if (dataChunk.Last() != "}")
                            {
                                dataChunk.Add("}");
                            }
                        }
                        else if (_line[i - 1] == '=' && !char.IsWhiteSpace(c))
                        {
                            if (dataChunk.Last() != "=")
                            {
                                dataChunk.Add("=");
                            }
                        }
                    }
                    if (c == '\'')
                    {
                        insideApostropheQuotes = !insideApostropheQuotes;
                    }
                    else if (c == '\"')
                    {
                        insideSpeechQuotes = !insideSpeechQuotes;
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
                                if (nestedName.Contains(")"))
                                {
                                    // This must be a function / task with parameters
                                    for (int k = j - 1; k > 0; k--)
                                    {
                                        if (dataChunk[k].Contains("("))
                                        {
                                            if (k > 0 && dataChunk[k - 1] == "task")
                                            {
                                                nestedName = "task " + dataChunk[k].Substring(0, dataChunk[k].IndexOf('('));
                                            }
                                            else
                                            {
                                                nestedName = dataChunk[k].Substring(0, dataChunk[k].IndexOf('('));
                                            }
                                            break;
                                        }
                                    }
                                }
                                if (j < dataChunk.Count)
                                {
                                    nestedData.Add(dataChunk.Skip(j + 1).ToList());
                                }
                            }
                            else
                            {
                                nestedName = data[i - 1].Last();
                                if (nestedName.Contains(")"))
                                {
                                    // This must be a function / task with parameters
                                    for (int k = j - 1; k > 0; k--)
                                    {
                                        if (dataChunk[k].Contains("("))
                                        {
                                            if (k > 0 && dataChunk[k - 1] == "task")
                                            {
                                                nestedName = "task " + dataChunk[k].Substring(0, dataChunk[k].IndexOf('('));
                                            }
                                            else
                                            {
                                                nestedName = dataChunk[k].Substring(0, dataChunk[k].IndexOf('('));
                                            }
                                            break;
                                        }
                                    }
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
                            if (nestedName.StartsWith("task "))
                            {
                                GTask newTask = new GTask(newBlock);
                                newTask.Name = nestedName.Replace("task ", "").TrimStart();
                                block.Children.Add(newTask);
                            }
                            else
                            {
                                newBlock.Name = nestedName;
                                block.Children.Add(newBlock);
                            }
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
                            if (variable != null)
                            {
                                variable.NestedLevel = NestedLevel + 1;
                                if (!block.Children.ContainsObject(variable.Name))
                                {
                                    block.Children.Add(variable);
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
            else if (parentBlock.Children.ContainsObject(dataChunk[equalChunkIndex + 1]) && parentBlock.Children.GetObject(dataChunk[equalChunkIndex + 1]) is GVariable)
            {
                return new GVariable(dataChunk[equalChunkIndex - 1], ((GVariable)parentBlock.Children.GetObject(dataChunk[equalChunkIndex + 1])).Value);
            }
            else if (dataChunk.Count > equalChunkIndex + 3 && dataChunk[equalChunkIndex + 2] == "=")
            {
                if (!parentBlock.Children.ContainsObject(dataChunk[equalChunkIndex - 1]))
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

}
