using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using BenchmarkDotNet.Attributes;

namespace AlgorithmPerformanceDemo
{
    #region Count:10

    /* Count:10
|                     Method |      Mean |     Error |    StdDev |
|--------------------------- |----------:|----------:|----------:|
|                 ToListList |  25.32 ns | 0.4287 ns | 0.4010 ns |
|                ToListArray |  46.98 ns | 0.6974 ns | 0.5823 ns |
|              ToListHashSet |  24.45 ns | 0.3862 ns | 0.3612 ns |
|           ToListDictionary |  26.13 ns | 0.3036 ns | 0.2840 ns |
|           ToListSortedList |  25.02 ns | 0.3808 ns | 0.3562 ns |
|            ToListSortedSet |  27.39 ns | 0.3351 ns | 0.3134 ns |
|     ToListSortedDictionary |  24.86 ns | 0.1344 ns | 0.1122 ns |
|        ToListConcurrentBag |  41.90 ns | 0.4232 ns | 0.3959 ns |
| ToListConcurrentDictionary | 104.82 ns | 0.9103 ns | 0.8070 ns |
|           ToListLinkedList |  24.09 ns | 0.3732 ns | 0.3491 ns |
     */

    #endregion
    public class AlgorithmToList
    {
        private AlgorithmCreate _algorithmCreate;

        [GlobalSetup]
        public void GlobalSetup()
        {
            var sw = Stopwatch.StartNew();

            DatasProvider.Create();
            _algorithmCreate = new AlgorithmCreate();

            _algorithmCreate.CreateList();
            _algorithmCreate.CreateArray();
            _algorithmCreate.CreateHashSet();
            _algorithmCreate.CreateDictionary();
            _algorithmCreate.CreateSortedList();
            _algorithmCreate.CreateSortedSet();
            _algorithmCreate.CreateSortedDictionary();
            _algorithmCreate.CreateConcurrentBag();
            _algorithmCreate.CreateConcurrentDictionary();
            _algorithmCreate.CreateLinkedList();

            sw.Stop();
            Console.WriteLine($"{MethodBase.GetCurrentMethod().Name}--{sw.ElapsedTicks}");
        }

        [Benchmark]
        public void ToListList()
        {
            var sw = Stopwatch.StartNew();

            var x = _algorithmCreate.List.ToList();
            GC.KeepAlive(x);

            sw.Stop();
            Console.WriteLine($"{MethodBase.GetCurrentMethod().Name}--{sw.ElapsedTicks}");
        }

        [Benchmark]
        public void ToListArray()
        {
            var sw = Stopwatch.StartNew();

            var x = _algorithmCreate.Array.ToList();
            GC.KeepAlive(x);

            sw.Stop();
            Console.WriteLine($"{MethodBase.GetCurrentMethod().Name}--{sw.ElapsedTicks}");
        }

        [Benchmark]
        public void ToListHashSet()
        {
            var sw = Stopwatch.StartNew();

            var x = _algorithmCreate.HashSet.ToList();
            GC.KeepAlive(x);

            sw.Stop();
            Console.WriteLine($"{MethodBase.GetCurrentMethod().Name}--{sw.ElapsedTicks}");
        }

        [Benchmark]
        public void ToListDictionary()
        {
            var sw = Stopwatch.StartNew();

            var x = _algorithmCreate.Dictionary.ToList();
            GC.KeepAlive(x);

            sw.Stop();
            Console.WriteLine($"{MethodBase.GetCurrentMethod().Name}--{sw.ElapsedTicks}");
        }

        [Benchmark]
        public void ToListSortedList()
        {
            var sw = Stopwatch.StartNew();

            var x = _algorithmCreate.SortedList.ToList();
            GC.KeepAlive(x);

            sw.Stop();
            Console.WriteLine($"{MethodBase.GetCurrentMethod().Name}--{sw.ElapsedTicks}");
        }

        [Benchmark]
        public void ToListSortedSet()
        {
            var sw = Stopwatch.StartNew();

            var x = _algorithmCreate.SortedSet.ToList();
            GC.KeepAlive(x);

            sw.Stop();
            Console.WriteLine($"{MethodBase.GetCurrentMethod().Name}--{sw.ElapsedTicks}");
        }

        [Benchmark]
        public void ToListSortedDictionary()
        {
            var sw = Stopwatch.StartNew();

            var x = _algorithmCreate.SortedDictionary.ToList();
            GC.KeepAlive(x);

            sw.Stop();
            Console.WriteLine($"{MethodBase.GetCurrentMethod().Name}--{sw.ElapsedTicks}");
        }

        [Benchmark]
        public void ToListConcurrentBag()
        {
            var sw = Stopwatch.StartNew();

            var x = _algorithmCreate.ConcurrentBag.ToList();
            GC.KeepAlive(x);

            sw.Stop();
            Console.WriteLine($"{MethodBase.GetCurrentMethod().Name}--{sw.ElapsedTicks}");
        }

        [Benchmark]
        public void ToListConcurrentDictionary()
        {
            var sw = Stopwatch.StartNew();

            var x = _algorithmCreate.ConcurrentDictionary.ToList();
            GC.KeepAlive(x);

            sw.Stop();
            Console.WriteLine($"{MethodBase.GetCurrentMethod().Name}--{sw.ElapsedTicks}");
        }

        [Benchmark]
        public void ToListLinkedList()
        {
            var sw = Stopwatch.StartNew();

            var x = _algorithmCreate.LinkedList.ToList();
            GC.KeepAlive(x);

            sw.Stop();
            Console.WriteLine($"{MethodBase.GetCurrentMethod().Name}--{sw.ElapsedTicks}");
        }
    }
}
