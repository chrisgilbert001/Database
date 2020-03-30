using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Structure;
using Database.QueryEngine;
using Database.SQLStatements.DML;

namespace Database.SQLStatements.DDL
{
    class Delete : Statement
    {
        public string TableName;
        public Where where;

        public override void Execute(Db database)
        {
            Engine.ExecuteDelete(database, this);
        }
    }
}
