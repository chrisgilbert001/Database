using Database.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.SQLStatements;
using Database.SQLStatements.DML;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;

namespace Database.SQLGrammar
{
    class SelectVisitor : SQLGrammarBaseVisitor<Select>
    {
        public Select select = new Select();

        public override Select VisitSelect_statement([NotNull] SQLGrammarParser.Select_statementContext context)
        {
            VisitChildren(context);
            return select;
        }

        public override Select VisitColumn_name([NotNull] SQLGrammarParser.Column_nameContext context)
        {
            VisitChildren(context);

            select.FromColumns.Add(context.column.GetText());
            return select;
        }

        public override Select VisitColumn_element([NotNull] SQLGrammarParser.Column_elementContext context)
        {
            return base.VisitColumn_element(context);
        }

        public override Select VisitTable_name([NotNull] SQLGrammarParser.Table_nameContext context)
        {
            VisitChildren(context);

            // If its parent is a select statement then this is the FROM table.
            // select = new Select();
            if (context.parent.GetType() == typeof(SQLGrammarParser.Select_statementContext))
            {
                select.FromTable = context.table.GetText();
            }
          
            return select;
        }  

        public override Select VisitColumn_list([NotNull] SQLGrammarParser.Column_listContext context)
        {
            return base.VisitColumn_list(context);
        }
    }
}
