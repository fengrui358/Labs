using System;
using System.Linq;
using BenchmarkDotNet.Attributes;

namespace AlgorithmPerformanceDemo
{
    public class AlgorithmFind
    {
        private AlgorithmCreate _algorithmCreate;

        [GlobalSetup]
        public void GlobalSetup()
        {
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
        public void FindList()
        {
            _algorithmCreate.List.Find(s => s == DatasProvider.SearchTarget);
        }

        [Benchmark]
        public void FindArray()
        {
            for (var i = 0; i < _algorithmCreate.Array.Length; i++)
            {
                if (_algorithmCreate.Array[i] == DatasProvider.SearchTarget)
                {
                    break;
                }
            }
        }

        [Benchmark]
        public void FindHashSet()
        {
            var i = 0;
            if (_algorithmCreate.HashSet.Contains(DatasProvider.NewTarget))
            {
                GC.KeepAlive(i);
            }
        }

        [Benchmark]
        public void FindDictionary()
        {
            var i = 0;
            if (_algorithmCreate.Dictionary.TryGetValue(DatasProvider.SearchTarget, out _))
            {
                GC.KeepAlive(i);
            }
        }

        [Benchmark]
        public void FindSortedList()
        {
            var i = _algorithmCreate.SortedList.IndexOfKey(DatasProvider.SearchTarget);
            GC.KeepAlive(i);
        }

        [Benchmark]
        public void FindSortedSet()
        {
            var i = _algorithmCreate.SortedSet.TryGetValue(DatasProvider.SearchTarget, out _);
            GC.KeepAlive(i);
        }

        [Benchmark]
        public void FindSortedDictionary()
        {
            var i = _algorithmCreate.SortedDictionary.TryGetValue(DatasProvider.SearchTarget, out _);
            GC.KeepAlive(i);
        }

        [Benchmark]
        public void FindConcurrentBag()
        {
            var i = _algorithmCreate.ConcurrentBag.FirstOrDefault(s => s == DatasProvider.SearchTarget);
            GC.KeepAlive(i);
        }

        [Benchmark]
        public void FindConcurrentDictionary()
        {
            var i = _algorithmCreate.ConcurrentDictionary.TryGetValue(DatasProvider.SearchTarget, out _);
            GC.KeepAlive(i);
        }

        [Benchmark]
        public void FindLinkedList()
        {
            var i = _algorithmCreate.LinkedList.Find(DatasProvider.SearchTarget);
            GC.KeepAlive(i);
        }
    }
}