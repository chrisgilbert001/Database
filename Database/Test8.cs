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
    class Test8
    {
        public void Test8M()
        {
            BTree<int, Row> tree0 = Stuff(3);
            BTree<int, Row> tree1 = Stuff(10);
            BTree<int, Row> tree2 = Stuff(100);
            BTree<int, Row> tree3 = Stuff(1000);
            BTree<int, Row> tree4 = Stuff(10000);

            List<BTree<int, Row>> list = new List<BTree<int, Row>> { tree0, tree1, tree2, tree3, tree4 };

            Console.WriteLine(tree0);
            Console.WriteLine(tree1);
            Console.WriteLine(tree2);
            Console.WriteLine(tree3);
            Console.WriteLine(tree4);
        }



        public static BTree<int, Row> Stuff(int num)
        {
            BTree<int, Row> tree = new BTree<int, Row>(num);

            for (int i = 0; i < 100000; i++)
            {
                Row row = new Row();
                tree.Insert(i, row);
            }
            return tree;
        }
    }
}
