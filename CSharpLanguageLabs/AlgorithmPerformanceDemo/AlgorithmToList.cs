using System;
using System.Linq;
using BenchmarkDotNet.Attributes;

namespace AlgorithmPerformanceDemo
{
    public class AlgorithmToList
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
