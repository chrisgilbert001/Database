using Database.Parsing;
using Database.SQLStatements.DDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;

namespace Database.SQLGrammar
{
    class CreateTableVisitor : SQLGrammarBaseVisitor<CreateTable>
    {
        public CreateTable createTable = new CreateTable();

        public override CreateTable VisitCreate_table_statement([NotNull] SQLGrammarParser.Create_table_statementContext context)
        {
            VisitChildren(context);
            createTable.TableName = context.tablename.GetText();

            return createTable;
        }

        public override CreateTable VisitColumn_name([NotNull] SQLGrammarParser.Column_nameContext context)
        {
            VisitChildren(context);
            createTable.ColumnNames.Add(context.column.GetText());

            return createTable;
        }

        public override CreateTable VisitUnique_constraint([NotNull] SQLGrammarParser.Unique_constraintContext context)
        {
            VisitChildren(context);
            createTable.ConstraintName = context.constraint_name.GetText();
            createTable.UniqueColumn = context.unique_column.GetText();

            return createTable;
        }
    }
}
