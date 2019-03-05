using System;
using System.Linq;
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
        }

        [Benchmark]
        public void ToListList()
        {
            var x = _algorithmCreate.List.ToList();
            GC.KeepAlive(x);
        }

        [Benchmark]
        public void ToListArray()
        {
            var x = _algorithmCreate.Array.ToList();
            GC.KeepAlive(x);
        }

        [Benchmark]
        public void ToListHashSet()
        {
            var x = _algorithmCreate.HashSet.ToList();
            GC.KeepAlive(x);
        }

        [Benchmark]
        public void ToListDictionary()
        {
            var x = _algorithmCreate.Dictionary.ToList();
            GC.KeepAlive(x);
        }

        [Benchmark]
        public void ToListSortedList()
        {
            var x = _algorithmCreate.SortedList.ToList();
            GC.KeepAlive(x);
        }

        [Benchmark]
        public void ToListSortedSet()
        {
            var x = _algorithmCreate.SortedSet.ToList();
            GC.KeepAlive(x);
        }

        [Benchmark]
        public void ToListSortedDictionary()
        {
            var x = _algorithmCreate.SortedDictionary.ToList();
            GC.KeepAlive(x);
        }

        [Benchmark]
        public void ToListConcurrentBag()
        {
            var x = _algorithmCreate.ConcurrentBag.ToList();
            GC.KeepAlive(x);
        }

        [Benchmark]
        public void ToListConcurrentDictionary()
        {
            var x = _algorithmCreate.ConcurrentDictionary.ToList();
            GC.KeepAlive(x);
        }

        [Benchmark]
        public void ToListLinkedList()
        {
            var x = _algorithmCreate.LinkedList.ToList();
            GC.KeepAlive(x);
        }
    }
}
