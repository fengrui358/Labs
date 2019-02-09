using System;
using System.Linq;
using BenchmarkDotNet.Running;

namespace Algorithm
{
    class Program
    {
        public static int DataGroupCount { get; set; } = 1;

        static void Main(string[] args)
        {
            var test = new long[] {43, 6576, 43, 23, 54, 657, 87, 54, 23, 2, 54, 76, -34, 43, -5435, -43, -546, 43, 0};
            var test2 = new long[] {56};
            test.SelectedSort(true);
            test2.SelectedSort(true);

            var datas = SortBenchmark.LongNumbers.First();
            var datasCopy = new long[datas.Length];
            datas.CopyTo(datasCopy, 0);

            datas.SelectedSort(true);
            Array.Sort(datasCopy, (l, l1) => -l.CompareTo(l1));
            if (!datasCopy.SequenceEqual(datas))
            {

            }

            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
        }
    }
}