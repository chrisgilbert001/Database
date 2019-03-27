using Newtonsoft.Json;
using System.IO;
using Database.Structure;

namespace Database.Loading
{
    class Loader
    {
        public static Db Load(string filePath)
        {
            StreamReader file = File.OpenText(filePath);

            using (JsonTextReader reader = new JsonTextReader(file))
            {
                Db database = JsonConvert.DeserializeObject<Db>(File.ReadAllText(filePath));

                return database;
            }
        }
    }
}
