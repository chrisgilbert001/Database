using Database.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using Database.SQLStatements.DML;
using Database.SQLStatements;

namespace Database.SQLGrammar
{
    class SQLVisitor : SQLGrammarBaseVisitor<Statement>
    {
        public override Statement VisitCompileUnit([NotNull] SQLGrammarParser.CompileUnitContext context)
        {
            if (context.dmlstatements() != null)
            {
                return new QueryVisitor().VisitDmlstatements(context.dmlstatements());
            }
            else if (context.ddlstatements() != null)
            {
                return VisitDdlstatements(context.ddlstatements());
            }
            else
            {
                throw new Exception();
            }
        }

        public override Statement VisitDdlstatements([NotNull] SQLGrammarParser.DdlstatementsContext context)
        {
            if (context.insert_statement() != null)
            {
                return new InsertVisitor().VisitInsert_statement(context.insert_statement());
            }
            else if (context.create_table_statement() != null)
            {
                return new CreateTableVisitor().VisitCreate_table_statement(context.create_table_statement());
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
