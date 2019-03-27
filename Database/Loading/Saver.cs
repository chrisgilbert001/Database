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
            string output = JsonConvert.SerializeObject(database);
            File.WriteAllText(filePath, String.Empty);
            File.WriteAllText(filePath, output);
        }
    }
}
