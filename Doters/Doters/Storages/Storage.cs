using System;
using System.Collections.Generic;
using System.Text;

namespace Doters.Storages
{
    public abstract class Storage
    {
        public string FilePath { get; private set; }


        public abstract void Append(bool truncate = false, params Doter[] linesToAppend);
        public abstract List<string> GetAllFileLines();


        public Storage(string filePath)
        {
            FilePath = filePath; 
        }
    }
}
