using Database.Parsing;
using Database.SQLStatements;
using Database.SQLStatements.DDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;

namespace Database.SQLGrammar
{
    class InsertVisitor : SQLGrammarBaseVisitor<Insert>
    {
        public Insert insert = new Insert();
        
        public override Insert VisitDdlstatements([NotNull] SQLGrammarParser.DdlstatementsContext context)
        {
            VisitChildren(context);
            return insert;
        }

        public override Insert VisitTable_name([NotNull] SQLGrammarParser.Table_nameContext context)
        {
            VisitChildren(context);
            insert.TableName = context.table.ToString();
            return insert;
        }

        public override Insert VisitInsert_statement([NotNull] SQLGrammarParser.Insert_statementContext context)
        {
            VisitChildren(context);
            return insert;
        }

        public override Insert VisitValues([NotNull] SQLGrammarParser.ValuesContext context)
        {
            VisitChildren(context);
            return insert;
        }

        public override Insert VisitVal([NotNull] SQLGrammarParser.ValContext context)
        {
            VisitChildren(context);
            insert.values.Add(context.value.GetText());
            return insert;
        }
    }
}
