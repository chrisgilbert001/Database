using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using Database.SQLStatements.DML;
using Database.SQLStatements;
using Database.Parsing;

namespace Database.SQLGrammar
{
    class WhereVisitor : SQLGrammarBaseVisitor<Where>
    {
        public Where where = new Where();

        public override Where VisitWhere([NotNull] SQLGrammarParser.WhereContext context)
        {
            VisitChildren(context);

            where.EqualsString = context.equals_string.GetText();

            return where;
        }

        public override Where VisitColumn_name([NotNull] SQLGrammarParser.Column_nameContext context)
        {
            VisitChildren(context);

            where.Column = new System.Tuple<string, string>(context.table_name().table.GetText(), context.column.GetText());

            return where;
        }       
    }
}
