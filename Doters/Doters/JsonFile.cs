using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Doters
{
    class JsonFile
    {
        public readonly string JsonPath;
        public JsonFile(string jsonpath)
        {
            JsonPath = jsonpath;
        }
        public async void DataToJson()
        {
            JsonSerializerSettings jss = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.None,
                Formatting = Formatting.Indented,
            };
            List<Doter> doters = DoterStorage.GetDoters();
            using (FileStream fs = new FileStream(JsonPath, FileMode.Truncate))
            using (StreamWriter sw = new StreamWriter(fs))

                    sw.WriteLine(JsonConvert.SerializeObject(doters, jss));
            
        }
        //public List<string> GetAllLinesFromJson()
        //{
        //    List<string> lines = new List<string>();
        //    if (!File.Exists(JsonPath))
        //    {
        //        return lines;
        //    }
        //    using (StreamReader sr = new StreamReader(JsonPath))
        //    {
        //        string line = null;
        //        while ((line = sr.ReadLine(JsonConvert.Deseria)) != null)
        //        {
        //            if (string.IsNullOrEmpty(line))
        //            {
        //                continue;
        //            }
        //            lines.Add(line);
        //        }
        //    }
        //    return lines;
        //}
    }
}
