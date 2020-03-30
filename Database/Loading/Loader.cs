using Newtonsoft.Json;
using System.IO;
using Database.Structure;

namespace Database.Loading
{
    class Loader
    {
        public static Db Load(string filePath)
        {
            using (StreamReader r = new StreamReader(filePath))
            {
                using (JsonReader reader = new JsonTextReader(r))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    Db database = serializer.Deserialize<Db>(reader);
                    return database;
                }
            } 
        }
    }
}
