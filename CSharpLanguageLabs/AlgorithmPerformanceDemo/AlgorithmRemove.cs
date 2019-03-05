using System;
using BenchmarkDotNet.Attributes;

namespace AlgorithmPerformanceDemo
{
    #region Count:10

    /* Count:10
|                     Method |      Mean |     Error |    StdDev |
|--------------------------- |----------:|----------:|----------:|
|                 RemoveList | 10.762 ns | 0.1865 ns | 0.1745 ns |
|                RemoveArray |  1.242 ns | 0.0231 ns | 0.0193 ns |
|              RemoveHashSet |  4.298 ns | 0.0939 ns | 0.0879 ns |
|           RemoveDictionary |  3.820 ns | 0.0327 ns | 0.0306 ns |
|           RemoveSortedList | 21.124 ns | 0.3028 ns | 0.2832 ns |
|            RemoveSortedSet |  4.860 ns | 0.0887 ns | 0.0830 ns |
|     RemoveSortedDictionary |  9.361 ns | 0.1447 ns | 0.1353 ns |
| RemoveConcurrentDictionary | 33.147 ns | 0.4669 ns | 0.4139 ns |
|           RemoveLinkedList |  5.064 ns | 0.0630 ns | 0.0589 ns |
     */

    #endregion

    public class AlgorithmRemove
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
                var newArray = new Guid[DatasProvider.Count - 1];

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