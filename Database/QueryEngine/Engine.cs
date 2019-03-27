using ConsoleTables;
using Database.SQLStatements;
using Database.SQLStatements.DDL;
using Database.SQLStatements.DML;
using Database.Structure;
using Database.TablePrinter;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.QueryEngine
{
    class Engine
    {
        /// <summary>
        /// Executes the select statement
        /// </summary>
        /// <param name="database"></param>
        /// <param name="select"></param>
        public static void ExecuteSelect(Db database, Select select)
        {
            ResultTable results = new ResultTable();
            int[] columnIndexes = new int[select.QueryColumnList.Count];

            if (select.Joins.Count >= 1)
            {
                foreach (Join join in select.Joins)
                {
                    // Get details about the join
                    join.GetStuff(database);

                    List<TableBase> tables = new List<TableBase>();
                    tables = CaculateJoin(join, select, results);
                    int[] indexes = new int[2];
                    indexes = CalculateJoinColumnIndexes(select, join, tables);

                    // Perform the nested loop join
                    // results = NestedLoopJoin(tables, indexes);

                    // Perform the hash join
                    results = HashJoin(tables, indexes);
                }
                columnIndexes = MapColumns(database, select, results);
            }
            else
                columnIndexes = MapColumns(database, select);

            Printer.PrintLine(columnIndexes);
            Printer.PrintRow(columnIndexes, results.ColumnList);
            Printer.PrintLine(columnIndexes);

            // Print it out
            foreach (Row row in results.Rows)
            {
                Printer.PrintRow(columnIndexes, row);
            }
        }

        /// <summary>
        /// Inserts rows into a table
        /// </summary>
        public static void ExecuteInsert(Db database, Insert insert)
        {
            Table table = database.GetTable(insert.TableName);

            Row newRow = new Row();
            newRow.Entries.AddRange(insert.values);
            table.AddRow(newRow);
        }

        /// <summary>/
        /// Perform a nested loop join.
        /// </summary>
        public static ResultTable NestedLoopJoin(List<TableBase> tables, int[] indexes)
        {
            foreach (Row row in tables[0].Rows)
            {
                foreach (Row row2 in tables[1].Rows)
                {
                    if (row.Entries[indexes[0]] == row2.Entries[indexes[1]])
                    {
                        Row satisfiedRow = new Row();

                        // Add the tuples to a new set of tuples.
                        satisfiedRow.Entries.AddRange(row.Entries);
                        satisfiedRow.Entries.AddRange(row2.Entries);
                        tables[2].Rows.Add(satisfiedRow);
                    }
                }
            }
            return (ResultTable)tables[2];
        }

        /// <summary>
        /// Simple Hash Join
        /// </summary>
        /// <param name="tables"></param>
        /// <param name="indexes"></param>
        /// <returns></returns>
        public static ResultTable HashJoin(List<TableBase> tables, int[] indexes)
        {
            Hashtable hash = new Hashtable();

            // Place the first relation into a hash table with the join column as the key
            foreach (Row row in tables[0].Rows)
            {
                // If the key isnt added already then add it.
                if (!hash.ContainsKey(row.Entries[indexes[0]]))
                {
                    hash.Add(row.Entries[indexes[0]], new List<Row>() { row });
                }
                else
                // If the key already exists, add this row to the list of rows with the same key.
                {
                    List<Row> rows = (List<Row>)hash[row.Entries[indexes[0]]];
                    rows.Add(row);
                }
            }

            // Probe 
            // For each row in the un-hashed table
            foreach (Row row1 in tables[1].Rows)
            {
                // If there is a key in the hastable that matches
                if (hash.ContainsKey(row1.Entries[indexes[1]]))
                {
                    // Cycle through the values, concatenate and add to the results table
                    List<Row> rows = (List<Row>)hash[row1.Entries[indexes[1]]];
                    foreach (Row row in rows)
                    {
                        Row satisfiedRow = new Row();

                        satisfiedRow.Entries.AddRange(row.Entries);
                        satisfiedRow.Entries.AddRange(row1.Entries);
                        tables[2].Rows.Add(satisfiedRow);
                    }
                }
            }
            return (ResultTable)tables[2];
        }

        /// <summary>
        /// Some pre processing of the join to simplify later.
        /// </summary>
        /// <param name="join"></param>
        /// <param name="select"></param>
        /// <param name="results"></param>
        /// <returns></returns>
        public static List<TableBase> CaculateJoin(Join join, Select select, ResultTable results)
        {
            TableBase table1;
            TableBase table2;
            ResultTable newResults = new ResultTable();

            if (select.processed == false)
            {
                table1 = join.Table1;
                newResults.ColumnList.AddRange(table1.ColumnList);
            }
            else
            {
                table1 = results;
                newResults.ColumnList.AddRange(results.ColumnList);
            }

            table2 = join.Table2;
            newResults.ColumnList.AddRange(table2.ColumnList);

            return new List<TableBase>()
            {
                table1,
                table2,
                newResults
            };
        }

        /// <summary>
        /// Gets the index of the columns to join the two tables.
        /// </summary>
        /// <param name="select"></param>
        /// <param name="join"></param>
        /// <param name="tables"></param>
        /// <returns></returns>
        public static int[] CalculateJoinColumnIndexes(Select select, Join join, List<TableBase> tables)
        {
            int table1Index;
            int table2Index;

            if (select.processed == false)
            {
                table1Index = join.Table1Column.ColumnIndex;
            }
            else
            {
                table1Index = tables[0].ColumnList.FindIndex(x => x.ColumnName == join.Table1Column.ColumnName);
            }
            table2Index = join.Table2Column.ColumnIndex;

            select.processed = true;
            return new int[] { table1Index, table2Index };
        }

        /// <summary>
        /// Maps the columns from the select query to columns in the results table.
        /// This retrieves the column indexes in order to print the results.
        /// </summary>
        /// <param name="db"></param>
        /// <param name="select"></param>
        /// <param name="results"></param>
        /// <returns></returns>
        public static int[] MapColumns(Db db, Select select, ResultTable results)
        {
            int[] indexes = new int[select.QueryColumnList.Count];
            int i = 0;

            foreach (Tuple<string, string> stuff in select.QueryColumnList)
            {
                Table table = db.GetTable(stuff.Item1);
                Column column = table.GetColumn(stuff.Item2);               
                indexes[i] = results.ColumnList.FindIndex(x => x.ColumnName.Equals(column.ColumnName));
                i++;
            }
            return indexes;
        }

        public static int[] MapColumns(Db db, Select select)
        {
            int[] indexes = new int[select.QueryColumnList.Count];
            int i = 0;

            foreach (Tuple<string, string> stuff in select.QueryColumnList)
            {
                Table table = db.GetTable(stuff.Item1);
                Column column = table.GetColumn(stuff.Item2);
                indexes[i] = column.ColumnIndex;
                i++;
            }
            return indexes;
        }
    }
}
