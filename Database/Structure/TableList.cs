using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Structure
{   /// <summary>
    /// Contains a collection of Tables
    /// </summary>
    class TableList
    {
        private List<Table> _tables = new List<Table>();
        private Database _database;

        public List<Table> Tables
        {
            get { return _tables; }
        }

        public Database Database
        {
            get { return _database; }
        }

        public TableList()
        {
        }

        /// <summary>
        /// Find a table in a List of tables from its name. 
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public int FindTable(string tableName)
        {
            return this.Tables.FindIndex(x => x.TableName.Equals(tableName));
        }

        public static Table FindTable(TableList tableList, string tableName)
        {
            tableList.Tables.Find(x => x.TableName.Equals(tableName));
            return null;
        }
    }
}
