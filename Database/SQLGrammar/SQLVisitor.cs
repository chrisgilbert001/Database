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
            //TODO: Also implement DDL here
            return new QueryVisitor().VisitDmlstatements(context.dmlstatements());
        }     
    }
}
