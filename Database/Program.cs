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

                // Person Table
                Table person = gilbertDb.AddAndCreateTable("Person"); 
                person.AddAndCreateColumn("Name");
                person.AddAndCreateColumn("JobID");
                person.AddAndCreateColumn("HouseID");
                person.AddAndCreateRow(new List<string>() { "Chris", "1", "11" });
                person.AddAndCreateRow(new List<string>() { "Jones", "2", "10" });
                person.AddAndCreateRow(new List<string>() { "Lewys", "3", "10" });

                // Job Table
                Table job = gilbertDb.AddAndCreateTable("Job");
                job.AddAndCreateColumn("JobName");
                job.AddAndCreateColumn("JobID");
                job.AddAndCreateRow(new List<string>() { "Retail Worker", "1" });
                job.AddAndCreateRow(new List<string>() { "Engineer", "2" });
                job.AddAndCreateRow(new List<string>() { "Student", "3" });

                // House Table
                Table house = gilbertDb.AddAndCreateTable("House");
                house.AddAndCreateColumn("HouseID");
                house.AddAndCreateColumn("Town");
                house.AddAndCreateRow(new List<string>() { "10", "Cambridge" });
                house.AddAndCreateRow(new List<string>() { "11", "Crawley" });


                // 1Query to execute on the database.
                string query = "SELECT Person.Name, Job.JobName, House.Town, House.HouseID " +
                               "FROM Person " +
                               "INNER JOIN House ON Person.HouseID = House.HouseID " +
                               "INNER JOIN Job ON Person.JobID = Job.JobID";
                
                #region AntlrStuff
                // Parse and visit the test query.
                AntlrInputStream inputStream = new AntlrInputStream(query.ToString());
                SQLGrammarLexer sqlLexer = new SQLGrammarLexer(inputStream);
                CommonTokenStream commonTokenStream = new CommonTokenStream(sqlLexer);
                SQLGrammarParser sqlParser = new SQLGrammarParser(commonTokenStream);
                SQLGrammarParser.CompileUnitContext context = sqlParser.compileUnit();
                SQLVisitor visitor = new SQLVisitor();
                #endregion

                // Run the query 1000 times and benchmark the speed.
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
