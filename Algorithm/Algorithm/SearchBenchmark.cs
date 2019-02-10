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

        public static IEnumerable<long[]> LongNumbersSort { get; }

        public static IEnumerable<long[]> LongNumbersSortDesc { get; }

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

            LongNumbersSort = new List<long[]> {LongNumbers.First().OrderBy(s => s).ToArray()};
            LongNumbersSortDesc = new List<long[]> {LongNumbers.First().OrderByDescending(s => s).ToArray()};
        }

        [Benchmark(Baseline = true)]
        [ArgumentsSource(nameof(LongNumbersSort))]
        public void StandardFind(long[] numbers)
        {
            var f = Array.Find(numbers, s => s == Target);
        }

        [Benchmark]
        [ArgumentsSource(nameof(LongNumbersSort))]
        public void StandardFirst(long[] numbers)
        {
            var f = numbers.First(s => s == Target);
        }

        [Benchmark]
        [ArgumentsSource(nameof(LongNumbersSort))]
        public void StandardFirstOrDefault(long[] numbers)
        {
            var f = numbers.FirstOrDefault(s => s == Target);
        }

        [Benchmark]
        [ArgumentsSource(nameof(LongNumbersSort))]
        public void StandardLast(long[] numbers)
        {
            var f = numbers.Last(s => s == Target);
        }

        [Benchmark]
        [ArgumentsSource(nameof(LongNumbersSort))]
        public void StandardLastOrDefault(long[] numbers)
        {
            var f = numbers.LastOrDefault(s => s == Target);
        }

        [Benchmark]
        [ArgumentsSource(nameof(LongNumbersSort))]
        public void StandardBinarySearch(long[] numbers)
        {
            var fIndex = Array.BinarySearch(numbers, Target);
            if (fIndex >= 0)
            {
                var f = numbers[fIndex];
            }
        }

        [Benchmark]
        [ArgumentsSource(nameof(LongNumbersSort))]
        public void BinarySearch(long[] numbers)
        {
            var fIndex = numbers.BinarySearch(Target);
            if (fIndex >= 0)
            {
                var f = numbers[fIndex];
            }
        }
    }
}