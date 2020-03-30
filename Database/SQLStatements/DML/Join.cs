using Database.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.SQLStatements.DML
{
    /* 
     * Class representing a Join Clause.
     * Contains information about the join to do.    
     */
    class Join: Statement
    {
        public string TableName { get; set; }
        public List<Tuple<string, string>> ColumnList = new List<Tuple<string, string>>();
        public Table Table1;
        public Table Table2;
        public Column Table1Column;
        public Column Table2Column;
        public bool IsDirty;
        public int Selectivity;


        public void GetStuff(Db db)
        {
            Tuple<string, string> table1Join = ColumnList.Find(x => x.Item1 != TableName);
            Table1 = db.GetTable(table1Join.Item1);
            Table1Column = Table1.GetColumn(table1Join.Item2);

            Tuple<string, string> table2Join = ColumnList.Find(x => x.Item1.Equals(TableName));
            
            if (IsDirty == false)
            {
                Table2 = db.GetTable(table2Join.Item1);
            }

            Table2Column = Table2.GetColumn(table2Join.Item2);
        }

        public override void Execute(Db database)
        {
            this.GetStuff(database);
        }
    }
}
