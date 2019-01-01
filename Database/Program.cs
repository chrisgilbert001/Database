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
using Database.SQLStatements;
using System.Diagnostics;

namespace Database
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Create some tables and fill them with test data.
                Db gilbertDb = new Db();
                Table person = gilbertDb.AddAndCreateTable("Person"); 
                person.AddAndCreateColumn("Name");
                person.AddAndCreateColumn("Age");
                for (int i = 0; i < 2; i++)
                {
                    person.AddAndCreateRow(new List<string>() { "Chris", "33" });
                }
                person.AddAndCreateRow(new List<string>() { "John", "44" });


                // Parse and execute a test query
                string query = "SELECT Name FROM Person INNER JOIN Person ON Name = Name";
                
                #region AntlrStuff
                AntlrInputStream inputStream = new AntlrInputStream(query.ToString());
                SQLGrammarLexer sqlLexer = new SQLGrammarLexer(inputStream);
                CommonTokenStream commonTokenStream = new CommonTokenStream(sqlLexer);
                SQLGrammarParser sqlParser = new SQLGrammarParser(commonTokenStream);
                SQLGrammarParser.CompileUnitContext context = sqlParser.compileUnit();
                SQLVisitor visitor = new SQLVisitor();
                #endregion

                //Statement statement = visitor.Visit(context);
                //statement.Execute(gilbertDb);

                // Start benchmarking.
                Action action = () => visitor.Visit(context).Execute(gilbertDb);
                Benchmark(action, 1000);
               
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
            }
        }

        // Benchmark method to test method speeds.
        private static void Benchmark(Action act, int iterations)
        {
            GC.Collect();
            act.Invoke(); // run once outside of loop to avoid initialization costs
            Stopwatch sw = Stopwatch.StartNew();
            for (int i = 0; i < iterations; i++)
            {
                act.Invoke();
            }
            sw.Stop();
            Console.WriteLine((sw.ElapsedMilliseconds / iterations).ToString());
        }
    }  
}
