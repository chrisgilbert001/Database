using Antlr.Runtime;
using Database.Structure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Parsing;

namespace Database
{
    class Program
    {
        static void Main(string[] args)
        {

            Stream inputStream = Console.OpenStandardInput();
            ANTLRInputStream input = new ANTLRInputStream(inputStream);
            CombinedGrammarLexer lexer = new CombinedGrammarLexer(input);
            //parser.addSubExpr();
        }
    }
}
