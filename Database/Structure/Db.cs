using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Structure
{
    class Db 
    {
        public string Name { get; set; }
        public Dictionary<string, Table> TableDictionary = new Dictionary<string, Table>();

        #region Constructors
        /// <summary>
        /// Constructor to be used to load from file.
        /// </summary>
        [JsonConstructor]
        public Db()
        {
        }

        public Db(string name)
        {
            Name = name;
        }
# endregion

        /// <summary>
        /// Creates and returns a new Table.
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public Table AddAndCreateTable(string tableName)
        {
            Table table = new Table(tableName);
            TableDictionary.Add(tableName, table);
            return table;
        }

        /// <summary>
        /// Searches the database to return a table by its name.
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public Table GetTable(string tableName)
        {
            Table table;
            // Return the table object if it exists
            if (TableDictionary.TryGetValue(tableName, out table))
                return table;
            else 
                // Error - the table does not exist
                throw new Exception($"A table by the name '{tableName}' can not be found.");
        }
    }
}
