using Database.Parsing;
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
    class SQLVisitor : SQLGrammarBaseVisitor<object>
    {
        public override object VisitCompileUnit([NotNull] SQLGrammarParser.CompileUnitContext context)
        {
            // Also implement UML here
            return VisitDmlstatements(context.dmlstatements());
        }

        public override object VisitDmlstatements([NotNull] SQLGrammarParser.DmlstatementsContext context)
        {
            // If it is a select statement then do it.
            var statement = context.select_statement();
            if (statement != null)
            {
                return new SelectVisitor().VisitSelect_statement(statement);
            }
            else
            {
                return null;
            }
        }
    }
}
