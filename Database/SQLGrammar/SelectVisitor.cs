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

            // If its parent is a select statement then add the columns we are getting from the FROM table.
            if (context.parent.parent.parent.GetType() == typeof(SQLGrammarParser.Select_statementContext))
            {
                select.FromColumns.Add(context.column.GetText());
            }
            // If its parent is a Join then get the columns we are joining.
            else if (context.parent.GetType() == typeof(SQLGrammarParser.JoinContext))
            {
                select.Joins[0].JoinColumns.Add(context.column.GetText());
            }
            
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
            if (context.parent.GetType() == typeof(SQLGrammarParser.Select_statementContext))
            {
                select.FromTable = context.table.GetText();
            }           
            // If its parent is a Join then get the join tables and columns
            else if (context.parent.GetType() == typeof(SQLGrammarParser.JoinContext))
            {
                Join join = new Join();
                join.JoinTable = context.table.GetText();
                select.Joins.Add(join);
            }

            return select;
        }  

        public override Select VisitColumn_list([NotNull] SQLGrammarParser.Column_listContext context)
        {
            return base.VisitColumn_list(context);
        }

        public override Select VisitJoin([NotNull] SQLGrammarParser.JoinContext context)
        {    
            return base.VisitJoin(context);
        }
    }
}
