using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using BenchmarkDotNet.Attributes;

namespace AlgorithmPerformanceDemo
{
    #region Count:10

    /* Count:10
|                     Method |         Mean |      Error |      StdDev |       Median |
|--------------------------- |-------------:|-----------:|------------:|-------------:|
|                 CreateList |    10.367 ns |  0.2414 ns |   0.5596 ns |    10.129 ns |
|                CreateArray |     4.770 ns |  0.0396 ns |   0.0351 ns |     4.771 ns |
|              CreateHashSet |    12.805 ns |  0.1127 ns |   0.0999 ns |    12.776 ns |
|           CreateDictionary |    13.327 ns |  0.0701 ns |   0.0622 ns |    13.332 ns |
|           CreateSortedList |    17.777 ns |  0.4782 ns |   1.0395 ns |    17.351 ns |
|            CreateSortedSet |     9.754 ns |  0.2341 ns |   0.6449 ns |     9.393 ns |
|     CreateSortedDictionary |    19.883 ns |  0.2102 ns |   0.1966 ns |    19.818 ns |
|        CreateConcurrentBag | 1,524.381 ns | 36.6838 ns | 108.1630 ns | 1,490.309 ns |
| CreateConcurrentDictionary |    72.122 ns |  0.3181 ns |   0.2976 ns |    72.184 ns |
|           CreateLinkedList |     4.903 ns |  0.0207 ns |   0.0193 ns |     4.895 ns |
     */

    #endregion

    public class AlgorithmCreate
    {
        public List<Guid> List { get; private set; }
        public Guid[] Array { get; private set; }
        public HashSet<Guid> HashSet { get; private set; }
        public Dictionary<Guid, Guid> Dictionary { get; private set; }
        public SortedList<Guid, Guid> SortedList { get; private set; }
        public SortedSet<Guid> SortedSet { get; private set; }
        public SortedDictionary<Guid, Guid> SortedDictionary { get; private set; }
        public ConcurrentBag<Guid> ConcurrentBag { get; private set; }
        public ConcurrentDictionary<Guid, Guid> ConcurrentDictionary { get; private set; }
        public LinkedList<Guid> LinkedList { get; private set; }

        [GlobalSetup]
        public void GlobalSetup()
        {
            var sw = Stopwatch.StartNew();

            DatasProvider.Create();

            sw.Stop();
            Console.WriteLine($"{MethodBase.GetCurrentMethod().Name}--{sw.ElapsedTicks}");
        }

        [Benchmark]
        public void CreateList()
        {
            var sw = Stopwatch.StartNew();

            List = new List<Guid>();
            for (var i = 0; i < DatasProvider.DataSource.Length; i++)
            {
                List.Add(DatasProvider.DataSource[i]);
            }

            sw.Stop();
            Console.WriteLine($"{MethodBase.GetCurrentMethod().Name}--{sw.ElapsedTicks}");
        }

        [Benchmark]
        public void CreateArray()
        {
            var sw = Stopwatch.StartNew();

            Array = new Guid[DatasProvider.Count];
            for (var i = 0; i < DatasProvider.DataSource.Length; i++)
            {
                Array[i] = DatasProvider.DataSource[i];
            }

            sw.Stop();
            Console.WriteLine($"{MethodBase.GetCurrentMethod().Name}--{sw.ElapsedTicks}");
        }

        [Benchmark]
        public void CreateHashSet()
        {
            var sw = Stopwatch.StartNew();

            HashSet = new HashSet<Guid>();
            for (var i = 0; i < DatasProvider.DataSource.Length; i++)
            {
                HashSet.Add(DatasProvider.DataSource[i]);
            }

            sw.Stop();
            Console.WriteLine($"{MethodBase.GetCurrentMethod().Name}--{sw.ElapsedTicks}");
        }

        [Benchmark]
        public void CreateDictionary()
        {
            var sw = Stopwatch.StartNew();

            Dictionary = new Dictionary<Guid, Guid>();
            for (var i = 0; i < DatasProvider.DataSource.Length; i++)
            {
                Dictionary.TryAdd(DatasProvider.DataSource[i], DatasProvider.DataSource[i]);
            }

            sw.Stop();
            Console.WriteLine($"{MethodBase.GetCurrentMethod().Name}--{sw.ElapsedTicks}");
        }

        [Benchmark]
        public void CreateSortedList()
        {
            var sw = Stopwatch.StartNew();

            SortedList = new SortedList<Guid, Guid>();
            for (var i = 0; i < DatasProvider.DataSource.Length; i++)
            {
                SortedList.TryAdd(DatasProvider.DataSource[i], DatasProvider.DataSource[i]);
            }

            sw.Stop();
            Console.WriteLine($"{MethodBase.GetCurrentMethod().Name}--{sw.ElapsedTicks}");
        }

        [Benchmark]
        public void CreateSortedSet()
        {
            var sw = Stopwatch.StartNew();

            SortedSet = new SortedSet<Guid>();
            for (var i = 0; i < DatasProvider.DataSource.Length; i++)
            {
                SortedSet.Add(DatasProvider.DataSource[i]);
            }

            sw.Stop();
            Console.WriteLine($"{MethodBase.GetCurrentMethod().Name}--{sw.ElapsedTicks}");
        }

        [Benchmark]
        public void CreateSortedDictionary()
        {
            var sw = Stopwatch.StartNew();

            SortedDictionary = new SortedDictionary<Guid, Guid>();
            for (var i = 0; i < DatasProvider.DataSource.Length; i++)
            {
                SortedDictionary.TryAdd(DatasProvider.DataSource[i], DatasProvider.DataSource[i]);
            }

            sw.Stop();
            Console.WriteLine($"{MethodBase.GetCurrentMethod().Name}--{sw.ElapsedTicks}");
        }

        [Benchmark]
        public void CreateConcurrentBag()
        {
            var sw = Stopwatch.StartNew();

            ConcurrentBag = new ConcurrentBag<Guid>();
            for (var i = 0; i < DatasProvider.DataSource.Length; i++)
            {
                ConcurrentBag.Add(DatasProvider.DataSource[i]);
            }

            sw.Stop();
            Console.WriteLine($"{MethodBase.GetCurrentMethod().Name}--{sw.ElapsedTicks}");
        }

        [Benchmark]
        public void CreateConcurrentDictionary()
        {
            var sw = Stopwatch.StartNew();

            ConcurrentDictionary = new ConcurrentDictionary<Guid, Guid>();
            for (var i = 0; i < DatasProvider.DataSource.Length; i++)
            {
                ConcurrentDictionary.TryAdd(DatasProvider.DataSource[i], DatasProvider.DataSource[i]);
            }

            sw.Stop();
            Console.WriteLine($"{MethodBase.GetCurrentMethod().Name}--{sw.ElapsedTicks}");
        }

        [Benchmark]
        public void CreateLinkedList()
        {
            var sw = Stopwatch.StartNew();

            LinkedList = new LinkedList<Guid>();
            for (var i = 0; i < DatasProvider.DataSource.Length; i++)
            {
                LinkedList.AddLast(DatasProvider.DataSource[i]);
            }

            sw.Stop();
            Console.WriteLine($"{MethodBase.GetCurrentMethod().Name}--{sw.ElapsedTicks}");
        }
    }
}