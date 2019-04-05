﻿using Newtonsoft.Json;
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

        public Table AddAndCreateTable(string tableName)
        {
            Table table = new Table(tableName);
            TableDictionary.Add(tableName, table);
            return table;
        }

        /// <summary>
        /// Searches the database tables to retrieve a table by table name.
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public Table GetTable(string tableName)
        {
            Table table;
            if (TableDictionary.TryGetValue(tableName, out table))
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
