using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;

namespace Algorithm
{
    [CoreJob]
    public class SortBenchmark
    {
        private const int Count = 10000;
        public static IEnumerable<long[]> LongNumbers { get; }

        static SortBenchmark()
        {
            var list = new List<long[]>();
            for (int i = 0; i < Program.DataGroupCount; i++)
            {
                var random = new Random();
                var array = new long[Count];
                for (int j = 0; j < Count; j++)
                {
                    array[j] = (long)random.Next(int.MinValue, int.MaxValue) << 32 + random.Next(int.MinValue, Int32.MaxValue);
                }
                list.Add(array);
            }

            LongNumbers = new List<long[]>(list);
        }

        [Benchmark]
        [ArgumentsSource(nameof(LongNumbers))]
        public void BubbleSort(long[] numbers)
        {
            numbers.BubbleSort();
            var x = numbers.ToArray();
        }

        [Benchmark]
        [ArgumentsSource(nameof(LongNumbers))]
        public void BubbleSortDesc(long[] numbers)
        {
            numbers.BubbleSort(true);
            var x = numbers.ToArray();
        }

        [Benchmark]
        [ArgumentsSource(nameof(LongNumbers))]
        public void StandardOrderBy(long[] numbers)
        {
            numbers = numbers.OrderBy(s => s).ToArray();
        }

        [Benchmark]
        [ArgumentsSource(nameof(LongNumbers))]
        public void StandardOrderByDescending(long[] numbers)
        {
            numbers = numbers.OrderByDescending(s => s).ToArray();
        }

        [Benchmark(Baseline = true)]
        [ArgumentsSource(nameof(LongNumbers))]
        public void StandardSort(long[] numbers)
        {
            Array.Sort(numbers);
        }

        [Benchmark]
        [ArgumentsSource(nameof(LongNumbers))]
        public void StandardSortByDescending(long[] numbers)
        {
            Array.Sort(numbers, (l, l1) => -l.CompareTo(l1));
        }
    }
}