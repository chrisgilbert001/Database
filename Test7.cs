using System;

public class Class1
{
	public Class1()
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
	}
}
