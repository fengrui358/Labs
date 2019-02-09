using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;

namespace Algorithm
{
    [CoreJob]
    public class SearchBenchmark
    {
        private const long Target = 5876;
        private const int Count = 10000;
        public static IEnumerable<long[]> LongNumbers { get; }

        static SearchBenchmark()
        {
            var list = new List<long[]>();
            for (int i = 0; i < Program.DataGroupCount; i++)
            {
                var random = new Random();
                var array = new long[Count];
                for (long j = 0; j < Count; j++)
                {
                    array[j] = j;
                }

                list.Add(array.OrderBy(s => random.Next(int.MaxValue)).ToArray());
            }

            LongNumbers = new List<long[]>(list);
        }

        [Benchmark(Baseline = true)]
        [ArgumentsSource(nameof(LongNumbers))]
        public void StandardFind(long[] numbers)
        {
            var f = Array.Find(numbers,s => s == Target);
        }

        [Benchmark]
        [ArgumentsSource(nameof(LongNumbers))]
        public void StandardFirst(long[] numbers)
        {
            var f = numbers.First(s => s == Target);
        }

        [Benchmark]
        [ArgumentsSource(nameof(LongNumbers))]
        public void StandardFirstOrDefault(long[] numbers)
        {
            var f = numbers.FirstOrDefault(s => s == Target);
        }

        [Benchmark]
        [ArgumentsSource(nameof(LongNumbers))]
        public void StandardLast(long[] numbers)
        {
            var f = numbers.Last(s => s == Target);
        }

        [Benchmark]
        [ArgumentsSource(nameof(LongNumbers))]
        public void StandardLastOrDefault(long[] numbers)
        {
            var f = numbers.LastOrDefault(s => s == Target);
        }

        [Benchmark]
        [ArgumentsSource(nameof(LongNumbers))]
        public void StandardBinarySearch(long[] numbers)
        {
            var fIndex = Array.BinarySearch(numbers, Target);
            if (fIndex >= 0)
            {
                var f = numbers[fIndex];
            }
        }
    }
}
