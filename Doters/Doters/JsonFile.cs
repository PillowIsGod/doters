using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Doters
{
    public class JsonFile : Storages.Storage
    {
        public JsonFile(string filePath) : base(filePath)
        {
        }
        public async void DataToJson()
        {
            List<Doter> doters = DoterStorage.GetDoters();
            using (FileStream fs = new FileStream(FilePath, FileMode.Truncate))
            using (StreamWriter sw = new StreamWriter(fs))
            {
                var str = JObject.FromObject(new { doters = doters }).ToString();
                sw.WriteLine(str);
            }

        }
        public override void Append(bool trunc = false, params Doter[] doters)
        {

            var jObject = new { doters = doters };

            if (!trunc)
            {
                List<Doter> doters2 = DoterStorage.GetDoters();
                foreach (var item in doters)
                {
                    doters2.Add(item);
                }

                jObject = new { doters = doters2.ToArray() };
            }
            

            using (FileStream fs = new FileStream(FilePath, FileMode.Truncate))
            using (StreamWriter sw = new StreamWriter(fs))
            {
                var str = JObject.FromObject(jObject).ToString();
                sw.WriteLine(str);
            }
        }

        public override List<string> GetAllFileLines()
        {
            List<string> lines = new List<string>();
            string content = File.ReadAllText(FilePath);

            if (string.IsNullOrEmpty(content))
            {
                return lines;
            }

            var text = JsonConvert.DeserializeObject<DotersListJson>(content);
            foreach (var item in text.Doters)
            {
                string line = item.MMR + "," + item.Hours + "," + item.Name + "," + item.Games + "," + item.Wins;
                lines.Add(line);
            }
            return lines;
        }
    }
    public class DotersListJson
    {
        [JsonProperty("doters")]
        public List<Doter> Doters { get; set; }
        public DotersListJson()
        {

        }
    }
}
