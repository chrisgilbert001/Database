using Antlr4.Runtime.Misc;
using Database.Parsing;
using Database.SQLStatements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.SQLGrammar
{
    class QueryVisitor : SQLGrammarBaseVisitor<Statement>
    {
        public override Statement VisitDmlstatements([NotNull] SQLGrammarParser.DmlstatementsContext context)
        {
            // So far we only support a select statement.
            var statement = context.select_statement();
            return new SelectVisitor().VisitSelect_statement(statement);
        }
    }
}
