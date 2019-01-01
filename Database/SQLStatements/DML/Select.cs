using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Structure;

namespace Database.SQLStatements.DML
{
    class Select : Query
    {
        public string FromTable { get; set; }
        private List<string> _fromColumns = new List<string>();
        private List<string> _joinTables = new List<string>();
        private List<Join> _joins = new List<Join>();

        public List<string> FromColumns
        {
            get { return _fromColumns; }
        }

        public List<string> JoinTables
        {
            get { return _joinTables; }
        }

        public List<Join> Joins
        {
            get { return _joins; }
        }

        public Select()
        {

        }

        /// <summary>
        /// Execute the select statement that has been built on a database
        /// </summary>
        /// <returns></returns>
        public override void Execute(Db database)
        {
            Table table = database.GetTable(FromTable);
            List<int> columnIndexes = new List<int>();
            List<Row> rows = new List<Row>();
            int joinCount = Joins.Count;

            if (joinCount != 0)
            {
                rows = Joins[0].ExecuteJoin(database, table);
            }

            // Get the columns
            foreach (string column in FromColumns)
            {
                columnIndexes.Add(table.GetColumn(column).ColumnID);
            }
            if (joinCount != 0)
            {
                foreach (Row row in rows)
                {

                    Row resultRow = new Row();
                    foreach (int index in columnIndexes)
                    {
                        Console.Write(row.Entries[index] + " | ");
                        resultRow.Entries.Add(row.Entries[index]);
                    }
                    Console.WriteLine();
                }
            }
            else
            {
                foreach (Row row in table.Rows)
                {

                    Row resultRow = new Row();
                    foreach (int index in columnIndexes)
                    {
                        Console.Write(row.Entries[index] + " | ");
                        resultRow.Entries.Add(row.Entries[index]);
                    }
                    Console.WriteLine();
                    rows.Add(resultRow);
                }
            }
        } 
    }
}
