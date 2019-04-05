using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Structure;
using System.Collections;
using Database.QueryEngine;

namespace Database.SQLStatements.DML
{
    /*
     * Class for a simple select statement.
     */
    class Select : Query
    {
        public string FromTableName { get; set; }
        public Table FromTable;
        public List<Table> QueryTables = new List<Table>();
        public List<Tuple<string, string>> QueryColumnList = new List<Tuple<string, string>>();
        public Column[] QueryColumns;
        public List<Join> Joins = new List<Join> ();
        public bool processed = false;
        public Where Where;

        public Select()
        {

        }

        public override void Execute(Db database)
        {
            Engine.ExecuteSelect(database, this);
        }
    };
}
