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
using Database.Structure;

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
            // TODO: Maybe improve here.
            // If its great grand parent is a select statement then add the columns we are getting from the FROM table.
            if (context.parent.parent.parent.GetType() == typeof(SQLGrammarParser.Select_statementContext))
            {
                select.QueryColumnList.Add(new Tuple <string, string>(context.table_name().table.GetText(), context.column.GetText()));
            }
            
            return select;
        }

        public override Select VisitTable_name([NotNull] SQLGrammarParser.Table_nameContext context)
        {
            VisitChildren(context);
            // TODO: Maybe improve here.
            // If its parent is a select statement then this is the FROM table.
            if (context.parent.GetType() == typeof(SQLGrammarParser.Select_statementContext))
            {
                select.FromTableName = context.table.GetText();
            }

            return select;
        }

        public override Select VisitJoin([NotNull] SQLGrammarParser.JoinContext context)
        {
            VisitChildren(context);

            // When we reach a join clause in the tree store the table to join and the two columns in which to join on.
            Join join = new Join();
            join.TableName = context.table_name().table.GetText();
            join.ColumnList.Add(new Tuple<string, string>(context.column_name(0).table_name().table.GetText(), context.column_name(0).column.GetText()));
            join.ColumnList.Add(new Tuple<string, string>(context.column_name(1).table_name().table.GetText(), context.column_name(1).column.GetText()));
            select.Joins.Add(join);

            return select;
        }

        public override Select VisitWhere([NotNull] SQLGrammarParser.WhereContext context)
        {
            VisitChildren(context);
            select.Where = new WhereVisitor().VisitWhere(context);
            return select;
        }
    }
}
