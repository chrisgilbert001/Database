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
    class SQLVisitor : SQLGrammarBaseVisitor<string>
    {
        public override string Visit([NotNull] IParseTree tree)
        {
            return base.Visit(tree);
        }

        public override string VisitCompileUnit([NotNull] SQLGrammarParser.CompileUnitContext context)
        {
            return base.VisitCompileUnit(context);
        }

        public override string VisitDmlstatements([NotNull] SQLGrammarParser.DmlstatementsContext context)
        {
            return base.VisitDmlstatements(context);
        }

        public override string VisitSelect_statement([NotNull] SQLGrammarParser.Select_statementContext context)
        {
            Select select = new Select();
            return base.VisitSelect_statement(context);
        }

        public override string VisitAsterisk([NotNull] SQLGrammarParser.AsteriskContext context)
        {
            return base.VisitAsterisk(context);
        }

        public override string VisitChildren([NotNull] IRuleNode node)
        {
            Console.WriteLine(node.GetText());
            return base.VisitChildren(node);
        }

        public override string VisitColumn_element([NotNull] SQLGrammarParser.Column_elementContext context)
        {
            return base.VisitColumn_element(context);
        }
       
        public override string VisitColumn_list([NotNull] SQLGrammarParser.Column_listContext context)
        {
            return base.VisitColumn_list(context);
        }
    
        public override string VisitColumn_name([NotNull] SQLGrammarParser.Column_nameContext context)
        {
            return base.VisitColumn_name(context);
        }

        public override string VisitErrorNode([NotNull] IErrorNode node)
        {
            return base.VisitErrorNode(node);
        }

        public override string VisitId([NotNull] SQLGrammarParser.IdContext context)
        {
            return base.VisitId(context);
        }

        public override string VisitTable_name([NotNull] SQLGrammarParser.Table_nameContext context)
        {
            return base.VisitTable_name(context);
        }
    }
}
