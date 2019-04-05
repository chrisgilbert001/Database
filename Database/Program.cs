using Database.Structure;
using System;
using System.Collections.Generic;
using Database.Parsing;
using Antlr4.Runtime;
using Database.SQLGrammar;
using System.Diagnostics;
using Database.SQLStatements;
using Database.QueryEngine;
using Database.Loading;

namespace Database
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Db database = Loader.Load(@"C:\Users\chris gilbert\Desktop\db2.json");

                //Db database = new Db("GilbertDB");
                // Db database = Loader.Load(args[0]);
                //Console.WriteLine("Database Successfully Loaded.");
                //Console.WriteLine();
                //Console.Write("Enter a Query");
                //Console.WriteLine();

                //string query = Console.ReadLine();

                #region testy stuff
                /*
                // Test B tree
                BTree <int, Row> tree = new BTree<int, Row>(3);      
                tree.Insert(6, new Row());
                tree.Insert(4, new Row());
                tree.Insert(2, new Row());
                tree.Insert(3, new Row());
                tree.Insert(7, new Row());
                tree.Insert(5, new Row());
                tree.Insert(10, new Row());
                tree.Insert(8, new Row());
                tree.Insert(11, new Row());
                tree.Insert(13, new Row());
                tree.Insert(9, new Row());
                tree.Insert(14, new Row());
                tree.Insert(15, new Row()); 
                tree.Insert(16, new Row());
                tree.Insert(17, new Row());
                tree.Insert(1, new Row());


                tree.DeleteFromTree(13, tree.RootNode);
                tree.DeleteFromTree(15, tree.RootNode);
                tree.DeleteFromTree(11, tree.RootNode);
                tree.DeleteFromTree(14, tree.RootNode);
                tree.DeleteFromTree(16, tree.RootNode);
                tree.DeleteFromTree(17, tree.RootNode);
                */
                /*
                // Create some tables and fill them with test data.
                // Db database = new Db("database");

                // Person Table#
                Table person = database.AddAndCreateTable("Person"); 
                person.AddAndCreateColumn("Name");
                person.AddAndCreateColumn("JobID");
                person.AddAndCreateColumn("HouseID");
                Row row = person.AddAndCreateRow(new List<string>() { "Chris", "1", "11" });
                person.AddAndCreateRow(new List<string>() { "Jones", "2", "10" });
                for (int i = 3; i < 1000; i++)
                { 
                
                    person.AddAndCreateRow(new List<string>() { "Lewys", i.ToString(), "10" });
                }

                
                tree.Insert("Chris", row);
                tree.Insert("Simon", new Row());
                tree.Insert("Lewys", new Row());
                tree.Insert("Bob", new Row());
                

                // Job Table
                Table job = database.AddAndCreateTable("Job");
                job.AddAndCreateColumn("JobName");
                job.AddAndCreateColumn("JobID");
                job.AddAndCreateRow(new List<string>() { "Retail Worker", "1" });
                job.AddAndCreateRow(new List<string>() { "Engineer", "2" });
                for (int i = 3; i < 1000; i++)
                {
                    job.AddAndCreateRow(new List<string>() { "Student", i.ToString() });
                }

                // House Table
                Table house = database.AddAndCreateTable("House");
                house.AddAndCreateColumn("HouseID");
                house.AddAndCreateColumn("Town");
                house.AddAndCreateRow(new List<string>() { "10", "Cambridge" });
                house.AddAndCreateRow(new List<string>() { "11", "Crawley" });

                // Saver.Save(database, @"C:\Users\chris gilbert\Desktop\db2.json" );
                */
                #endregion


                // Query to execute on the database.
                
                string query = "SELECT Person.Name, Job.JobName, House.Town, Job.JobName " +
                               "FROM Person " +
                               "INNER JOIN House ON Person.HouseID = House.HouseID " +
                               "INNER JOIN Job ON Person.JobID = Job.JobID " +
                               "INNER JOIN House ON Person.HouseID = House.HouseID " +
                               "WHERE Job.JobID = '2'";
                

                /*
                // Query to execute on the database.
                string query = "INSERT INTO Person " +
                               "VALUES ('John', '1', '1'), ('Chris', '2', '2')";

                */

                //string query = "CREATE TABLE Person (PersonID, Name, Age, Size, CONSTRAINT index UNIQUE (PersonID))";

                #region AntlrStuff
                // Parse and visit the test query.
                AntlrInputStream inputStream = new AntlrInputStream(query.ToString());
                SQLGrammarLexer sqlLexer = new SQLGrammarLexer(inputStream);
                CommonTokenStream commonTokenStream = new CommonTokenStream(sqlLexer);
                SQLGrammarParser sqlParser = new SQLGrammarParser(commonTokenStream);
                SQLGrammarParser.CompileUnitContext context = sqlParser.compileUnit();
                SQLVisitor visitor = new SQLVisitor();
                #endregion

                Statement statement = visitor.Visit(context);
                statement.Execute(database);

                #region AntlrStuff2
                string query1 = "INSERT INTO Person ('1', 'Chris', '16', 'big'), ('2', 'Bob', '16', 'big'), ('3', 'Charlie', '16', 'big')";
                // Parse and visit the test query.
                AntlrInputStream inputStream1 = new AntlrInputStream(query1.ToString());
                SQLGrammarLexer sqlLexer1 = new SQLGrammarLexer(inputStream1);
                CommonTokenStream commonTokenStream1 = new CommonTokenStream(sqlLexer1);
                SQLGrammarParser sqlParser1 = new SQLGrammarParser(commonTokenStream1);
                SQLGrammarParser.CompileUnitContext context1 = sqlParser1.compileUnit();
                SQLVisitor visitor1 = new SQLVisitor();
                #endregion



                Statement statement1 = visitor.Visit(context1);
                statement1.Execute(database);

                // Run the query 1000 times and benchmark the speed.
                Action action = () => visitor.Visit(context).Execute(database);

                Benchmark(action, 10);  
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
            // run once outside of loop to avoid initialization costs
            act.Invoke(); 
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
