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
    class QueryVisitor : SQLGrammarBaseVisitor<Query>
    {
        public override Query VisitDmlstatements([NotNull] SQLGrammarParser.DmlstatementsContext context)
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
