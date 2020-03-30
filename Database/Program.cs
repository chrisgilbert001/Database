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

#if DEBUG
            args = new string[2];
            args[0] = @"C:\Users\chris gilbert\Desktop\demodb2.json";
#endif

            try
            {
                Db db;


                // Load a database
                if (args[1] == null)
                {
                    db = Loader.Load(args[0]);
                }
                // Create a new database
                else
                {
                    string databaseName = args[1];
                    string filepath = args[0];

                    db = new Db(databaseName);
                }

                // Run the program
                Run(db, args[0]);
            }
            catch (Exception ex)
            {
            }
        }

        // Run a query
        private static void Run(Db db, string filepath)
        {
            try
            {
                Console.WriteLine("Enter a query");
                string query = Console.ReadLine();

                if (query.Equals("Save"))
                {
                    Saver.Save(db, filepath);
                }
                {
                    // Antlr initialisation
                    AntlrInputStream inputStream = new AntlrInputStream(query.ToString());
                    SQLGrammarLexer sqlLexer = new SQLGrammarLexer(inputStream);
                    CommonTokenStream commonTokenStream = new CommonTokenStream(sqlLexer);
                    SQLGrammarParser sqlParser = new SQLGrammarParser(commonTokenStream);
                    SQLGrammarParser.CompileUnitContext context1 = sqlParser.compileUnit();
                    SQLVisitor visitor = new SQLVisitor();

                    try
                    {
                        Statement statement = visitor.Visit(context1);
                        statement.Execute(db);
                    }
                    catch (Exception ex)
                    {
                        // Display the appropriate error.
                        Console.WriteLine("Error: " + ex.Message);

                        // Re-run the program
                        Run(db, filepath);
                    }

                    Run(db, filepath);
                }
            }
            catch (Exception ex)
            {
                // Display the appropriate error.
                Console.WriteLine("Error: " + ex.Message);

                // Re-run the program
                Run(db, filepath);
            }
        }
    }
}
        
