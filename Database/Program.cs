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
using Database.SQLStatements.DML;

namespace Database
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string text = "SELECT table1.column, table1.column2 FROM table1";

                AntlrInputStream inputStream = new AntlrInputStream(text.ToString());
                SQLGrammarLexer sqlLexer = new SQLGrammarLexer(inputStream);
                CommonTokenStream commonTokenStream = new CommonTokenStream(sqlLexer);
                SQLGrammarParser sqlParser = new SQLGrammarParser(commonTokenStream);

                SQLGrammarParser.CompileUnitContext context = sqlParser.compileUnit();
                SQLVisitor visitor = new SQLVisitor();
                
                Select hi = (Select)visitor.Visit(context);

                Console.WriteLine(hi.FromTable);

                TableList tableList = new TableList(); 
                tableList.Tables.Add(new Table("Person"));
                tableList.Tables[0].AddColumn(new Column("Name"));
                tableList.Tables[0].AddColumn(new Column("Age"));
                tableList.Tables[0].AddRow(new Row(new List<string>() {"Chris", "21" }));
                tableList.Tables[0].AddRow(new Row(new List<string>() { "Lewys", "22" }));
                tableList.Tables[0].AddRow(new Row(new List<string>() { "Talha", "22" }));


                tableList.Tables.Add(new Table("Car"));
                tableList.Tables[1].AddColumn(new Column("Brand Name"));
                tableList.Tables[1].AddRow(new Row(new List<string>() { "Mercedes"}));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
            }
        }
    }  
}
