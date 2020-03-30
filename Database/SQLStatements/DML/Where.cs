using Database.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.SQLStatements.DML
{
    class Where : Statement
    {
        public Tuple<string, string> Column;
        public Column Column_Column;
        public string EqualsString;
        public Table Table;
        public List<Tuple<string, string>> Columns = new List<Tuple<string, string>>();
        public List<string> EqualsStrings = new List<string>();

        #region Setters

        public void SetColumn(Db db)
        {
            Table table = db.GetTable(Column.Item1);
            SetTable(table);
            Column_Column = table.GetColumn(Column.Item2);
        }

        private void SetTable(Table table)
        {
            Table = table;
        }

        #endregion

        /// <summary>
        /// Find the rows that match the where clause.
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public List<Row> GetMatchingRows(Db db)
        {
            List<Row> rows = new List<Row>();
            // Search the unique index if we can
            if (this.Column_Column.IsUnique)
            {
                Row matchingRow = Table.UniqueIndex.Search(Convert.ToInt32(EqualsString), Table.UniqueIndex.RootNode).Pointer;
                rows.Add(matchingRow);
                return rows;
            }
            // If not -  manually search every row
            else
            {
                foreach (Row row in Table.Rows)
                {
                    if (row.Entries[Column_Column.ColumnIndex].Equals(EqualsString))
                    {
                        rows.Add(row);
                    }
                }
                return rows;
            }
        }

        /// <summary>
        /// Find the rows that match the where clause.
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public List<Row> GetMatchingRows(Db db, ResultTable results)
        {
            List<Row> rows = new List<Row>();
            int ind = results.ColumnList.IndexOf(Column_Column);
            foreach (Row row in results.Rows)
            {
                if (row.Entries[ind].Equals(EqualsString))
                {
                    rows.Add(row);
                }
            }
            return rows;
        }

        public override void Execute(Db database)
        {
            this.SetColumn(database);
        }
    }
}
