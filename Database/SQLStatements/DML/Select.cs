using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Structure;

namespace Database.SQLStatements.DML
{
    /*
     * Class for a simple select statement.
     */
    class Select : Query
    {
        public string FromTableName { get; set; }
        public List<Tuple<string, string>> ColumnList = new List<Tuple<string, string>>();
        public List<Join> Joins = new List<Join>();
        public Column[] columns;
        public int[] columnIndexes;
        List<Row> results = new List<Row>();
        public List<Column> ResultColumns = new List<Column>();

        public Select()
        {

        }

        /// <summary>
        /// Here we execute the joining of two tables.
        /// </summary>
        /// <param name="database"></param>
        /// <param name="join"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        public List<Row> ExecuteJoin(Db database, Join join, int joinIndex)
        {
            // TODO: This needs alot of work. So far just a VERY brute force nested loop join.
            // Currently alot of problems and no consideration for join order.
            Table table1;
            Table table2;
            List<Row> table1Rows;
            List<Row> table2Rows;
            string table1JoinColumn;
            string table2JoinColumn;
            int table1JoinColumnIndex;
            int table2JoinColumnIndex;
            List<Row> joinResults = new List<Row>();

            // This is my short term solution to handle multiple joins in the same query.
            // If this is the first join in the query then use the FROM table and the join.
            if (joinIndex == 0)
            {
                table1 = database.GetTable(FromTableName);
                table1Rows = table1.Rows;
                table2 = database.GetTable(join.JoinTable);
                table2Rows = table2.Rows;
                ResultColumns.AddRange(table1.Columns.Values);
                ResultColumns.AddRange(table2.Columns.Values);

                table1JoinColumn = join.JoinColumns.Find(x => x.Item1.Equals(table1.TableName)).Item2;
                table2JoinColumn = join.JoinColumns.Find(x => x.Item1.Equals(table2.TableName)).Item2;
                table1JoinColumnIndex = table1.GetColumn(table1JoinColumn).ColumnIndex;
                table2JoinColumnIndex = table2.GetColumn(table2JoinColumn).ColumnIndex;

            }
            else
            // If this is NOT the first join in the query then use the join and the already existing result set from the previous join.
            {
                int intermediate;

                table1Rows = results;       
                table2 = database.GetTable(join.JoinTable);
                table2Rows = table2.Rows;
                table2JoinColumn = join.JoinColumns.Find(x => x.Item1.Equals(table2.TableName)).Item2;
                intermediate = join.JoinColumns.IndexOf(join.JoinColumns.Find(x => x.Item1.Equals(table2.TableName)));
                intermediate = (intermediate == 0) ? 1 : 0; 
                table2JoinColumnIndex = table2.GetColumn(table2JoinColumn).ColumnIndex;           
                table1JoinColumnIndex = ResultColumns.IndexOf(ResultColumns.Find(x => x.ColumnName.Equals(join.JoinColumns[intermediate].Item2)));         

                ResultColumns.AddRange(table2.Columns.Values);
            }


            // Nested Loop Join
            foreach (Row row in table1Rows)
            {
                string entry = row.Entries[table1JoinColumnIndex];
                foreach (Row joinRow in table2Rows)
                {
                    if (entry == joinRow.Entries[table2JoinColumnIndex])
                    {
                        Row resultRow = new Row();
                        resultRow.Entries.AddRange(row.Entries);
                        resultRow.Entries.AddRange(joinRow.Entries);
                        joinResults.Add(resultRow);
                    }
                }
            }
            return joinResults;
        }

        /// <summary>
        /// Execute the query
        /// </summary>
        /// <param name="database"></param>
        public override void Execute(Db database)
        {
            // Find the table we are selecting from.
            Table table = database.GetTable(FromTableName);
            int joinCount = Joins.Count;
            int joinIndex = 0;

            // If we need to do a table join - Do it.
            if (joinCount > 0)
            {
                foreach (Join join in Joins)
                {
                    results = ExecuteJoin(database, join, joinIndex);
                    joinIndex++;
                }
            }
            else 
            {
                // If not - just select the rows from the table.
                ResultColumns.AddRange(table.Columns.Values);
                results.AddRange(table.Rows);
            }

            // Map the columns and the order of the columns from the select statement and print them.
            MapColumns();

            // Print the results of the query.
            foreach(Row resultRow in results)
            {
                foreach (int columnIndex in columnIndexes)
                {
                    Console.Write(resultRow.Entries[columnIndex] + " || ");
                }
                Console.WriteLine();
            }
        }

        public void MapColumns()
        {
            columns = new Column[ColumnList.Count];
            columnIndexes = new int[ColumnList.Count];
            int i = 0;

            foreach (Tuple<string, string> column in ColumnList)
            {
                Column column2 = ResultColumns.Find(x => x.TableName.Equals(column.Item1) && x.ColumnName.Equals(column.Item2));
                int columnIndex = ResultColumns.IndexOf(column2);
                columnIndexes[i] = columnIndex;
                i++;
                Console.Write(column.Item2 + " || ");
            }
            Console.WriteLine();
        }
    };
}
