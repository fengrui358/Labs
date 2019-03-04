using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace AlgorithmPerformanceDemo
{
    public class AlgorithmAdd
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
        public void AddList()
        {
            _algorithmCreate.List.Add(DatasProvider.NewTarget);
        }

        [Benchmark]
        public void AddArray()
        {
            var newArray = new string[DatasProvider.Count + 1];
            _algorithmCreate.Array.CopyTo(newArray, 0);
            newArray[DatasProvider.Count] = DatasProvider.NewTarget;
        }

        [Benchmark]
        public void AddHashSet()
        {
            _algorithmCreate.HashSet.Add(DatasProvider.NewTarget);
        }

        [Benchmark]
        public void AddDictionary()
        {
            _algorithmCreate.Dictionary.TryAdd(DatasProvider.NewTarget, DatasProvider.NewTarget);
        }

        [Benchmark]
        public void AddSortedList()
        {
            _algorithmCreate.SortedList.TryAdd(DatasProvider.NewTarget, DatasProvider.NewTarget);
        }

        [Benchmark]
        public void AddSortedSet()
        {
            _algorithmCreate.SortedSet.Add(DatasProvider.NewTarget);
        }

        [Benchmark]
        public void AddSortedDictionary()
        {
            _algorithmCreate.SortedDictionary.TryAdd(DatasProvider.NewTarget, DatasProvider.NewTarget);
        }

        [Benchmark]
        public void AddConcurrentBag()
        {
            _algorithmCreate.ConcurrentBag.Add(DatasProvider.NewTarget);
        }

        [Benchmark]
        public void AddConcurrentDictionary()
        {
            _algorithmCreate.ConcurrentDictionary.TryAdd(DatasProvider.NewTarget, DatasProvider.NewTarget);
        }

        [Benchmark]
        public void AddLinkedList()
        {
            _algorithmCreate.LinkedList.AddLast(DatasProvider.NewTarget);
        }
    }
}