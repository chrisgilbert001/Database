﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Structure;
using Database.QueryEngine;

namespace Database.SQLStatements.DDL
{
    class Insert : Statement
    {
        public string TableName;
        public List<List<string>> Rows = new List<List<string>>();
        public List<string> Values = new List<string>();

        public override void Execute(Db database)
        {
            Engine.ExecuteInsert(database, this);
        }
    }
}
