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
                return VisitDmlstatements(context.dmlstatements());
            }
            else if (context.ddlstatements() != null)
            {
                return VisitDdlstatements(context.ddlstatements());
            }
            else
            {
                return null;
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
            else if (context.delete_statement() != null)
            {
                return new DeleteVisitor().VisitDelete_statement(context.delete_statement());
            }
            else
            {
                return null;
            }
        }

        public override Statement VisitDmlstatements([NotNull] SQLGrammarParser.DmlstatementsContext context)
        {
            if (context.select_statement() != null)
            {
                return new SelectVisitor().VisitSelect_statement(context.select_statement());
            }
            else
            {
                return null;
            }
        }

    }
}
