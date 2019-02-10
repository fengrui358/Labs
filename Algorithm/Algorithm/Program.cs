using System;
using System.Linq;
using BenchmarkDotNet.Running;

namespace Algorithm
{
    class Program
    {
        public static int DataGroupCount { get; set; } = 3;

        static void Main(string[] args)
        {
            var test = new long[] { 56 };
            var test2 = new long[] { 56, 1, -34 };
            var test3 = new long[] { 43, 6576, 43, 23, 54, 657, 87, 54, 23, 2, 54, 76, -34, 43, -5435, -43, -546, 43, 0 };
            
            test.MergeSort(true);
            test2.MergeSort(true);
            test3.MergeSort(true);

            var datas = SortBenchmark.LongNumbers.First();
            var datasCopy = new long[datas.Length];
            datas.CopyTo(datasCopy, 0);

            datas.MergeSort(true);
            Array.Sort(datasCopy, (l, l1) => -l.CompareTo(l1));
            if (!datasCopy.SequenceEqual(datas))
            {

            }

            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
        }
    }
}