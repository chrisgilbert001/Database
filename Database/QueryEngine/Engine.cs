using Database.SQLStatements;
using Database.SQLStatements.DDL;
using Database.SQLStatements.DML;
using Database.Structure;
using Database.TablePrinter;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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
        ///      

        /// <summary>
        /// Execute a select statement with no joins.
        /// </summary>
        /// <param name="db"></param>
        /// <param name="select"></param>
        public static void ExecuteSelectWithoutJoins(Db db, Select select)
        {
            ResultTable results = new ResultTable();

            // Execute the where clause if there is one
            if (select.Where != null)
            {
                select.Where.Execute(db);
                results.Rows = select.Where.GetMatchingRows(db);
                results.ColumnList.AddRange(select.Where.Table.ColumnList);
            }
            // If there is no where clause
            else
            {
                select.FromTable = db.GetTable(select.FromTableName);
                results.ColumnList.AddRange(select.FromTable.ColumnList);
                results.Rows = select.FromTable.Rows;
            }

            // Print the results
            results.ColumnIndexes = MapColumns(db, select, results);
            Printer.PrintTable(results);
        }

        /// <summary>
        /// Executes a select statement with joins
        /// </summary>
        /// <param name="db"></param>
        /// <param name="select"></param>
        public static void ExecuteSelectWithJoins(Db db, Select select)
        {
            ResultTable results = new ResultTable();
            int[] columnIndexes = new int[select.QueryColumnList.Count];
            Table whereTable = new Table();

            List<Statement> statements = new Optimiser().OptimiseSelectWithJoins(db, select);
            Table person = db.GetTable("Person");
            int i = 0;
            foreach (Statement state in statements)
            {
                i++;
                if (state.GetType() == typeof(Where))
                {
                    Where where = (Where)state;
                    Console.WriteLine("WHERE " + where.Column_Column.ColumnName);
                    if (i == 1)
                    {
                        whereTable.ColumnList.AddRange(where.Table.ColumnList);
                        whereTable.TableName = where.Table.TableName;
                        whereTable.Rows = select.Where.GetMatchingRows(db);
                    }
                    else
                    {
                        results.Rows = select.Where.GetMatchingRows(db, results);
                    }
                }
                else
                {
                    Join join = (Join)state;
                    Console.WriteLine("JOIN " + join.Table1.TableName + " - " + join.Table2.TableName);
                    List<TableBase> tables = new List<TableBase>();
                    tables = CaculateJoin(join, select, results, whereTable);
                    int[] indexes = new int[2];
                    indexes = CalculateJoinColumnIndexes(select, join, tables);

                    // Perform the hash join
                    results = HashJoin(tables, indexes);
                }
            }
            results.ColumnIndexes = MapColumns(db, select, results);

            // Print the table to the console.
            Printer.PrintTable(results);
        }

        /// <summary>
        /// Inserts rows into a table
        /// </summary>
        public static void ExecuteInsert(Db database, Insert insert)
        {
            Table table = database.GetTable(insert.TableName);

            foreach (List<string> row in insert.Rows)
            {
                table.AddAndCreateRow(row);
            }
        }
        
        /// <summary>
        /// Creates a new table
        /// </summary>
        public static void ExecuteCreateTable(Db database, CreateTable create)
        {
            Table table = database.AddAndCreateTable(create.TableName);
            foreach (string column in create.ColumnNames)
            {
                table.AddAndCreateColumn(column);
            }
            // Set the unique column
            if (create.UniqueColumn != null)
            {
                Column uniqueColumn = table.GetColumn(create.UniqueColumn);
                table.UniqueIndex = new BTree<int, Row>(10);
                table.UniqueColumnIndex = uniqueColumn.ColumnIndex;
                uniqueColumn.IsUnique = true;
            }
            Console.WriteLine("Table Created Successfully");
        }

        /// <summary>
        /// Delete statement
        /// </summary>
        public static void ExecuteDelete(Db db, Delete delete)
        {
            
            Table table = db.GetTable(delete.TableName);

            // Delete the rows that match
            if (delete.where != null)
            {
                delete.where.SetColumn(db);
                List<Row> rows = delete.where.GetMatchingRows(db);
                foreach (Row row in rows)
                {
                    table.Rows.Remove(row);

                    // If there is a unique index delete the record in that too.
                    if (table.UniqueIndex != null)
                    {
                        table.UniqueIndex.DeleteFromTree(Convert.ToInt32(row.Entries[delete.where.Column_Column.ColumnIndex]), table.UniqueIndex.RootNode);
                    }
                }
            }
            // Delete all rows
            else
            {
                table.Rows.Clear();

                // If there is a unique index delete that too
                if (table.UniqueIndex != null)
                {
                    table.UniqueIndex = null;
                }
            }
        }

        /// <summary>/
        /// Perform a nested loop join.
        /// </summary>
        public static ResultTable NestedLoopJoin(List<TableBase> tables, int[] indexes)
        {
            // For each row in the first table
            foreach (Row row in tables[0].Rows)
            {
                // For each row in the first table
                foreach (Row row2 in tables[1].Rows)
                {
                    // If the rows satisfy the join condition add it to the output.
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
        /// Simple Hash Join
        /// </summary>
        /// <param name="tables"></param>
        /// <param name="indexes"></param>
        /// <returns></returns>
        public static ResultTable HashJoin(List<TableBase> tables, int[] indexes, bool isit)
        {
            Hashtable hash = new Hashtable();

            // Place the first relation into a hash table with the join column as the key
            foreach (Row row in tables[0].Rows)
            {
                CustomString stringy = new CustomString (row.Entries[indexes[0]]);
                // If the key isnt added already then add it.
                if (!hash.ContainsKey(stringy))
                {
                    hash.Add(stringy, new List<Row>() { row });
                }
                else
                // If the key already exists, add this row to the list of rows with the same key.
                {
                    List<Row> rows = (List<Row>)hash[stringy];
                    rows.Add(row);
                }
            }

            // Probe 
            // For each row in the un-hashed table
            foreach (Row row1 in tables[1].Rows)
            {
                CustomString stringy = new CustomString(row1.Entries[indexes[1]]);

                // If there is a key in the hastable that matches
                if (hash.ContainsKey(stringy))
                {
                    // Cycle through the values, concatenate and add to the results table
                    List<Row> rows = (List<Row>)hash[stringy];
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
        public static List<TableBase> CaculateJoin(Join join, Select select, ResultTable results, Table whereTable)
        {
            if (whereTable != null)
            {
                if (join.Table1.TableName.Equals(whereTable.TableName))
                {
                    join.Table1 = whereTable;
                }
                else if (join.Table2.TableName.Equals(whereTable.TableName))
                {
                    join.Table2 = whereTable;
                }
            }

            TableBase table1 = null;
            TableBase table2 = null;
            ResultTable newResults = new ResultTable();

            if (select.processed == false)
            {
                table1 = join.Table1;
                //newResults.ColumnList.AddRange(table1.ColumnList);
            }
            else
            {
                table1 = results;
                //newResults.ColumnList.AddRange(results.ColumnList);
            }
            newResults.ColumnList = results.ColumnList;

            foreach(Column column in table1.ColumnList)
            if (!results.ColumnList.Contains(column))
            {
                    newResults.ColumnList.Add(column);
            }

            table2 = join.Table2;
            foreach (Column column in table2.ColumnList)
            if (!results.ColumnList.Contains(column))
            {
                newResults.ColumnList.Add(column);
            }

            //newResults.ColumnList.AddRange(table2.ColumnList);

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

        private static long[] Benchmark(Action act, long iterations)
        {
            long[] times = new long[iterations];
            GC.Collect();
            // run once outside of loop to avoid initialization costs
            act.Invoke();
            for (int i = 0; i < iterations; i++)
            {
                Stopwatch sw = Stopwatch.StartNew();
                act.Invoke();
                sw.Stop();
                times[i] = sw.ElapsedMilliseconds;
            }

            return times;
        }
    }
}
