using Database.Structure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Parsing;
using Antlr4.Runtime;
using Database.SQLGrammar;
using Antlr.Runtime.Tree;

namespace Database
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string text = "SELECT table1.column FROM table1";

                AntlrInputStream inputStream = new AntlrInputStream(text.ToString());
                SQLGrammarLexer sqlLexer = new SQLGrammarLexer(inputStream);
                CommonTokenStream commonTokenStream = new CommonTokenStream(sqlLexer);
                SQLGrammarParser sqlParser = new SQLGrammarParser(commonTokenStream);

                SQLGrammarParser.CompileUnitContext context = sqlParser.compileUnit();
                SQLVisitor visitor = new SQLVisitor();
                visitor.Visit(context);
             
                Column name = new Column ("name");
                Column age = new Column("age");

                //person.Columns.Add(name); 
                TableList tables = new TableList(); 
                tables.Add(new Table("Person")); 
                tables[0].Columns.Add(name);
                tables[0].Columns.Add(age);

                Column column1 = new Column();

                column1.ColumnName = "column1";
                column1.Table = tables[0];

                column1.KillFreeman();

                Console.WriteLine(tables.FindTable("House"));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
            }
        }
    }  
}
