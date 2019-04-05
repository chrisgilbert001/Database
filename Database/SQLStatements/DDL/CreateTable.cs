using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Structure;
using Database.QueryEngine;

namespace Database.SQLStatements.DDL
{
    class CreateTable : Statement
    {
        public string TableName;
        public List<string> ColumnNames = new List<string>();
        public string ConstraintName;
        public string UniqueColumn;

        public override void Execute(Db database)
        {
            Engine.ExecuteCreateTable(database, this);
        }
    }
}
