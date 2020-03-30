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
    static class Test9
    {
        private static Random rng = new Random();

        public static void Test9M()
        {
            List<int> numbers = new List<int>();
            for (int i = 0; i < 100000; i++)
            {
                numbers.Add(i);
            }

            // Shuffle the numbers
            Shuffle<int>(numbers);
            long[] array0 = new long[10];
            long[] array1 = new long[10];
            long[] array2 = new long[10];
            long[] array3 = new long[10];
            long[] array4 = new long[10];

            for (int i = 0; i < 10; i++)
            {
                Action action0 = () => Stuff(3, numbers);
                long[] time0 = Benchmark(action0, 1);

                Action action1 = () => Stuff(10, numbers);
                long[] time1 = Benchmark(action1, 1);

                Action action2 = () => Stuff(100, numbers);
                long[] time2 = Benchmark(action2, 1);

                Action action3 = () => Stuff(1000, numbers);
                long[] time3 = Benchmark(action3, 1);

                Action action4 = () => Stuff(10000, numbers);
                long[] time4 = Benchmark(action4, 1);

                array0[i] = time0[0];
                array1[i] = time1[0];
                array2[i] = time2[0];
                array3[i] = time3[0];
                array4[i] = time4[0];

                /*
                List<long[]> list = new List<long[]> { time0, time1, time2, time3, time4 };


                for (int j = 0; j < list.Count; j++)
                {
                    long total = 0;

                    foreach (long t in list[j])
                    {
                        total = total + t;
                    }

                    list2[j][i] = (total / 10);
                }
                */

            }
            Console.WriteLine();
        }


        public static BTree<int, int> Stuff(int num, List<int> nums)
        {
            BTree<int, int> tree = new BTree<int, int>(num);

            for (int i = 0; i < 100000; i++)
            {
                int j = nums[i];
                tree.Insert(j, j);
            }
            return tree;
        }

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
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