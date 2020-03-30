using Database.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.TablePrinter
{
    class Printer
    {
        /// <summary>
        /// Prints the results of a query to a console
        /// </summary>
        /// <param name="results"></param>
        public static void PrintTable(ResultTable results)
        {
            if (results.Rows.Count == 0)
            {
                // Print the header line
                PrintLine(results.ColumnIndexes);

                // Print columns
                PrintCols(results);       
                
                // Print the header line
                PrintLine(results.ColumnIndexes);

                Console.WriteLine("0 rows returned.");
            }
            else
            {
                // Print the header line
                PrintLine(results.ColumnIndexes);

                // Print columns
                PrintCols(results);

                // Print the header line
                PrintLine(results.ColumnIndexes);

                // Print the rows
                foreach (Row row in results.Rows)
                {
                    PrintRow(results, row);
                }

                PrintLine(results.ColumnIndexes);
            }
        }

        /// <summary>
        /// Prints a dashed line to separate columns from rows.
        /// </summary>
        /// <param name="columnIndexes"></param>
        private static void PrintLine(int[] columnIndexes)
        {
            Console.WriteLine(' ' + new string('-', (columnIndexes.Length * 25) - 1));
        }

        /// <summary>
        /// Prints the columns
        /// </summary>
        /// <param name="results"></param>
        private static void PrintCols(ResultTable results)
        {
            int width = 24;
            string row = "|";

            foreach(int i in results.ColumnIndexes)
            {
                row += AlignCentre(results.ColumnList[i].ColumnName, width) + "|";
            }

            Console.WriteLine(row);
        }

        /// <summary>
        /// Prints the rows
        /// </summary>
        /// <param name="results"></param>
        /// <param name="row1"></param>
        private static void PrintRow(ResultTable results, Row row1)
        {
            int width = 24;
            string row = "|";

            foreach (int i in results.ColumnIndexes)
            {
                row += AlignCentre(row1.Entries[i], width) + "|";
            }

            Console.WriteLine(row);
        }

        /// <summary>
        /// Centers the string in a column
        /// </summary>
        /// <param name="text"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        private static string AlignCentre(string text, int width)
        {
            if (string.IsNullOrEmpty(text))
            {
                return new string(' ', width);
            }
            else
            {
                return text.PadRight(width - (width - text.Length)/ 2).PadLeft(24);
            }
        }
    }
}
