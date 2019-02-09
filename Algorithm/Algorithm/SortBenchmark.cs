using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;

namespace Algorithm
{
    [CoreJob]
    public class SortBenchmark
    {
        private const int Count = 1000;
        private const int DataGroupCount = 3;

        public static IEnumerable<long[]> LongNumbers { get; }

        static SortBenchmark()
        {
            var list = new List<long[]>();
            for (int i = 0; i < DataGroupCount; i++)
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

        //[Benchmark(Description = "O(n2)")]
        //[ArgumentsSource(nameof(GetLongNumbers))]
        //public void BubbleSort(long[] numbers)
        //{
        //    var t = new long[numbers.Length];
        //    Array.Copy(numbers, t, numbers.Length);
        //    t = t.OrderBy(s => s).ToArray();

        //    numbers.BubbleSort();
        //    if (!t.SequenceEqual(numbers))
        //    {
        //        throw new Exception("冒泡排序错误");
        //    }
        //}

        //[Benchmark(Description = "O(n2)")]
        //[ArgumentsSource(nameof(GetLongNumbers))]
        //public void BubbleSortDesc(long[] numbers)
        //{
        //    var t = new long[numbers.Length];
        //    Array.Copy(numbers, t, numbers.Length);
        //    t = t.OrderByDescending(s => s).ToArray();

        //    numbers.BubbleSort(true);
        //    if (!t.SequenceEqual(numbers))
        //    {
        //        throw new Exception("冒泡排序错误");
        //    }
        //}

        [Benchmark(Baseline = true)]
        [ArgumentsSource(nameof(LongNumbers))]
        public void StandardSort(long[] numbers)
        {
            numbers = numbers.OrderBy(s => s).ToArray();
        }

        [Benchmark]
        [ArgumentsSource(nameof(LongNumbers))]
        public void StandardSortDesc(long[] numbers)
        {
            numbers = numbers.OrderByDescending(s => s).ToArray();
        }
    }
}