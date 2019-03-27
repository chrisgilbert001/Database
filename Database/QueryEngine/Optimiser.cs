using Database.SQLStatements.DML;
using Database.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.QueryEngine
{
    class Optimiser
    {

        /// <summary>
        /// Get all the tables needed by the query
        /// </summary>
        /// <param name="database"></param>
        /// <param name="select"></param>
        public static void GetTables(Db database, Select select)
        {
            select.QueryTables.Add(database.GetTable(select.FromTableName));

            foreach (Join join in select.Joins)
            {
                select.QueryTables.Add(database.GetTable(join.TableName));
            }
        }

        /// <summary>
        /// Sort the tables in order of size - Least Tuples to most Tuples.
        /// </summary>
        /// <param name="database"></param>
        /// <param name="select"></param>
        public static void SortTablesBySizeAsc(Select select)
        {
            select.QueryTables.Sort((a, b) => a.Rows.Count.CompareTo(b.Rows.Count));
        }
    }
}
