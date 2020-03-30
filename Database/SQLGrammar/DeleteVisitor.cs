using Database.Parsing;
using Database.SQLStatements.DDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using Database.SQLStatements.DML;

namespace Database.SQLGrammar
{
    class DeleteVisitor : SQLGrammarBaseVisitor<Delete>
    {
        Delete delete = new Delete();

        public override Delete VisitDelete_statement([NotNull] SQLGrammarParser.Delete_statementContext context)
        {
            VisitChildren(context);
            
            return delete;
        }

        public override Delete VisitTable_name([NotNull] SQLGrammarParser.Table_nameContext context)
        {
            VisitChildren(context);

            delete.TableName = context.table.GetText();
            return delete;
        }

        public override Delete VisitWhere([NotNull] SQLGrammarParser.WhereContext context)
        { 
            delete.where = new WhereVisitor().VisitWhere(context);
            return delete;
        }
    }
}
