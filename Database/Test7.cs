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
    class Test7
    {
        public void Test7m()
        {
            long[] array0;
            long[] array1;
            long[] array2;
            long[] array3;
            long[] array4;


            Action action = () => Stuff(3);
            array0 = Benchmark(action, 10);

            Action action1 = () => Stuff(10);
            array1 = Benchmark(action1, 10);

            Action action2 = () => Stuff(100);
            array2 = Benchmark(action2, 10);

            Action action3 = () => Stuff(1000);
            array3 = Benchmark(action3, 10);

            Action action4 = () => Stuff(10000);
            array4 = Benchmark(action4, 10);
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

        private static long[] Benchmark(Action act, long iterations)
        {
            long[] times = new long[iterations];
            GC.Collect();
            // run once outside of loop to avoid initialization costs
            //act.Invoke();
            for (int i = 0; i < iterations; i++)
            {
                Stopwatch sw = Stopwatch.StartNew();
                act.Invoke();
                sw.Stop();
                times[i] = sw.ElapsedMilliseconds;
            }

            return times;
        }
    }
}
