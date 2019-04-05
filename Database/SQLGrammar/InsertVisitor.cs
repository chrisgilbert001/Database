using Database.Parsing;
using Database.SQLStatements;
using Database.SQLStatements.DDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;
using Database.Structure;

namespace Database.SQLGrammar
{
    class InsertVisitor : SQLGrammarBaseVisitor<Insert>
    {
        public Insert insert = new Insert();

        public override Insert VisitInsert_statement([NotNull] SQLGrammarParser.Insert_statementContext context)
        {
            VisitChildren(context);
            return insert;
        }

        public override Insert VisitTable_name([NotNull] SQLGrammarParser.Table_nameContext context)
        {
            VisitChildren(context);
            insert.TableName = context.table.GetText();
            return insert;
        }

        public override Insert VisitValues([NotNull] SQLGrammarParser.ValuesContext context)
        {
            insert.Values.Clear();
            VisitChildren(context);
            List<string> list = new List<string>();
            list.AddRange(insert.Values);
            insert.Rows.Add(list);
            return insert;
        }

        public override Insert VisitVal([NotNull] SQLGrammarParser.ValContext context)
        {
            VisitChildren(context);
            insert.Values.Add(context.value.GetText());
            return insert;
        }
    }
}
