using Database.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.SQLStatements.DML
{
    class Join
    {
        public string JoinTable { get; set; }
        private List<string> _joinColumns = new List<string>();

        public List<string> JoinColumns
        {
            get { return _joinColumns; }
        }

        public List<Row> ExecuteJoin(Db database, Table table)
        { 
            List<Row> results = new List<Row>();
            Table joiningTable = database.GetTable(JoinTable);

            int fromTableColumn = table.GetColumn(JoinColumns[0]).ColumnID;
            int joinTableColumn = joiningTable.GetColumn(JoinColumns[0]).ColumnID;

            foreach (Row row in table.Rows)
            {
                string entry = row.Entries[fromTableColumn];
                foreach (Row joinRow in joiningTable.Rows)
                {
                    if (entry == joinRow.Entries[joinTableColumn])
                    {
                        Row resultRow = new Row();
                        resultRow.Entries.AddRange(row.Entries);
                        resultRow.Entries.AddRange(joinRow.Entries);
                        results.Add(resultRow);
                    }
                }
            }
            return results;
        }
    }
}
