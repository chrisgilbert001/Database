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
        public static void PrintLine(int[] columnIndexes)
        {
            Console.WriteLine(new string('-', columnIndexes.Length));
        }

        public static void PrintRow(int[] columnIndexes, List<Column> columns)
        {
            int width = 20;
            string row = "|";

            foreach(int i in columnIndexes)
            {
                row += AlignCentre(columns[i].ColumnName, width) + "|";
            }

            Console.WriteLine(row);
        }

        public static void PrintRow(int[] columnIndexes, Row rowrow)
        {
            int width = 20;
            string row = "|";

            foreach (int i in columnIndexes)
            {
                row += AlignCentre(rowrow.Entries[i], width) + "|";
            }

            Console.WriteLine(row);
        }

        public static string AlignCentre(string text, int width)
        {
            text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

            if (string.IsNullOrEmpty(text))
            {
                return new string(' ', width);
            }
            else
            {
                return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
            }
        }
    }
}
