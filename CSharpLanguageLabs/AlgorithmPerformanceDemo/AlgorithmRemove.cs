using System;
using BenchmarkDotNet.Attributes;

namespace AlgorithmPerformanceDemo
{
    public class AlgorithmRemove
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
        public void RemoveList()
        {
            _algorithmCreate.List.Remove(DatasProvider.SearchTarget);
        }

        [Benchmark]
        public void RemoveArray()
        {
            var index = -1;
            for (var i = 0; i < _algorithmCreate.Array.Length; i++)
            {
                if (_algorithmCreate.Array[i] == DatasProvider.SearchTarget)
                {
                    index = i;
                    break;
                }
            }

            if (index > 0)
            {
                var newArray = new string[DatasProvider.Count - 1];

                Array.Copy(_algorithmCreate.Array, 0, newArray, 0, index);
                Array.Copy(_algorithmCreate.Array, index + 1, newArray, index, newArray.Length - index);
            }
        }

        [Benchmark]
        public void RemoveHashSet()
        {
            _algorithmCreate.HashSet.Remove(DatasProvider.SearchTarget);
        }

        [Benchmark]
        public void RemoveDictionary()
        {
            _algorithmCreate.Dictionary.Remove(DatasProvider.SearchTarget);
        }

        [Benchmark]
        public void RemoveSortedList()
        {
            _algorithmCreate.SortedList.Remove(DatasProvider.SearchTarget);
        }

        [Benchmark]
        public void RemoveSortedSet()
        {
            _algorithmCreate.SortedSet.Remove(DatasProvider.SearchTarget);
        }

        [Benchmark]
        public void RemoveSortedDictionary()
        {
            _algorithmCreate.SortedDictionary.Remove(DatasProvider.SearchTarget);
        }

        [Benchmark]
        public void RemoveConcurrentDictionary()
        {
            _algorithmCreate.ConcurrentDictionary.TryRemove(DatasProvider.SearchTarget, out _);
        }

        [Benchmark]
        public void RemoveLinkedList()
        {
            _algorithmCreate.LinkedList.Remove(DatasProvider.SearchTarget);
        }
    }
}