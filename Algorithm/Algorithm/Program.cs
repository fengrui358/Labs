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
            var test = new long[] { 56 };
            var test4 = new long[] {-45, 3, 47};
            var test2 = new long[] { 56, 1, -34 };
            var test3 = new long[] { 43, 6576, 43, 23, 54, 657, 87, 54, 23, 2, 54, 76, -34, 43, -5435, -43, -546, 43, 0 };
            
            test.BucketSort();
            test2.BucketSort();
            //test3.BucketSort();

            var i = test2.BinarySearch(1, new Sort.ReverseComparer<long>());
            var i2 = Array.BinarySearch(test2, 1, new Sort.ReverseComparer<long>());

            var datas = SearchBenchmark.LongNumbersSortDesc.First();
            var datasCopy = new long[datas.Length];
            datas.CopyTo(datasCopy, 0);

            var ii1 = datas.BinarySearch(546, new Sort.ReverseComparer<long>());
            var ii2 = Array.BinarySearch(datasCopy, 546, new Sort.ReverseComparer<long>());
            Array.Sort(datasCopy, (l, l1) => -l.CompareTo(l1));
            if (!datasCopy.SequenceEqual(datas))
            {

            }

            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
        }
    }
}