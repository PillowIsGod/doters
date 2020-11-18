using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Doters
{
    class TxtFileProcessing
    {
       



        public readonly string FilePath;
        public TxtFileProcessing(string filePath)
        {
            FilePath = filePath;
        }
        public List<string> GetAllFileLines()
        {
            List<string> lines = new List<string>();
            if (!File.Exists(FilePath))
            {
                return lines;
            }
            using (StreamReader sr = new StreamReader(FilePath))
            {
                string line = null;
                while ((line = sr.ReadLine()) != null)
                {
                    if (string.IsNullOrEmpty(line))
                    {
                        continue;
                    }
                    lines.Add(line);
                }
            }
            return lines;
        }
        public string LineToDeleteByNumber(int affirmation, int choiceofIntMMR, int number,string name)
        {
            List<string> lines = GetAllFileLines();
            if (affirmation == 1)
            {
                if (choiceofIntMMR == 1)
                {
                    string str;
                    if ((str = ErrorExistanceRange(lines, number)) != null)
                    {
                        return str;
                    }
                    lines.RemoveAt(number);
                    AppendFile(true, lines.ToArray());                   
                    return "Doter number " + (number + 1) + " has been deleted.";
                }
                else if (choiceofIntMMR == 2)
                {
                    string number1 = number.ToString();
                    for (int i = 0; i < lines.Count; i++)
                    {
                        if (lines[i].Contains(number1))
                        {
                            lines.Remove(lines[i]);
                            AppendFile(true, lines.ToArray());
                            return "Doter number " + (i + 1) + " has been deleted.";
                        }
                    }
                }
                else if (choiceofIntMMR == 3)
                {
                    for (int i = 0; i < lines.Count; i++)
                    {
                        if (lines[i].Contains(name))
                        {
                            lines.Remove(lines[i]);
                            AppendFile(true, lines.ToArray());
                            return "Doter number " + (i + 1) + " has been deleted.";
                        }
                    }
                }
            }
            else
            {
                return null;
            }
            return null;
        }
    


        
        public string GetFileLineByIndex(int index)
        {
            List<string> lines = GetAllFileLines();
            string str;
            if ((str = ErrorExistanceRange(lines, index)) != null)
            {
                return str;
            }
            return lines[index];
        }
        public void AppendFile(bool truncate = false, params string[] linesToAppend)
        {
            FileMode fm = FileMode.Append;
            if (truncate)
            {
                fm = FileMode.Truncate;
            }
            using (FileStream fs = new FileStream(FilePath, fm))
            using (StreamWriter write = new StreamWriter(fs))
            {
                foreach (var item in linesToAppend)
                {
                    write.WriteLine(item);
                }
            }
        }
        public string DeleteAllLinesFromFile(int affirmation)
        {
            List<string> lines = new List<string>();
            if (affirmation == 1)
            {
                AppendFile(true, lines.ToArray());   
                return "All doters were removed from file";
            }
            else
            {
                return null;
            }
            
        }
        public string LinesAddToFile(int amount)
        {
            List<string> lines = new List<string>();
                for (int i = 0; i < amount; i++)
                {
                Doter doter = new Doter(i+1);
                lines.Add(doter.DoterCharacteristics());

            }
                AppendFile(false, lines.ToArray());

          
            return amount+" doters were added to the file.";
        }
        private string ErrorExistanceRange(List<string> array, int? indexRow = null)
        {
            string line = null;
            if (array == null || array.Count == 0)
            {
                return "File is empty or does not exist";
            }
            if (indexRow != null && (indexRow < 0 || indexRow > array.Count - 1))
            {
                return "Your index is wrong, enter the number of range 1 - " + (array.Count);
            }
            return line;
        }
    }
}

