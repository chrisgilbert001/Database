using Database.Structure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Loading
{
    class Saver
    {
        public static void Save(Db database, string filePath)
        {
            using (FileStream fs = File.Open(filePath, FileMode.Truncate))
            using (StreamWriter sw = new StreamWriter(fs))
            using (JsonWriter jw = new JsonTextWriter(sw))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
                serializer.Serialize(jw, database);
            }
        }
    }
}
