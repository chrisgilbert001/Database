using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Structure
{
    class Db
    {
        public string Name { get; set; }
        public Dictionary<string, Table> TableDict = new Dictionary<string, Table>();

        public Db()
        {

        }

        public Table AddAndCreateTable(string tableName)
        {
            Table table = new Table(tableName);
            TableDict.Add(tableName, table);
            return table;
        }

        public Table GetTable(string tableName)
        {
            Table table;
            if (TableDict.TryGetValue(tableName, out table))
            {
                return table;
            }
            else
            {
                // TODO: Actually create an error class and error properly.
                throw new NotImplementedException();
            }
        }

    }
}
