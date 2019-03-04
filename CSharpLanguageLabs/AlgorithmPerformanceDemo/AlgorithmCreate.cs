using System.Collections.Concurrent;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace AlgorithmPerformanceDemo
{
    public class AlgorithmCreate
    {
        public List<string> List { get; private set; }
        public string[] Array { get; private set; }
        public HashSet<string> HashSet { get; private set; }
        public Dictionary<string, string> Dictionary { get; private set; }
        public SortedList<string, string> SortedList { get; private set; }
        public SortedSet<string> SortedSet { get; private set; }
        public SortedDictionary<string, string> SortedDictionary { get; private set; }
        public ConcurrentBag<string> ConcurrentBag { get; private set; }
        public ConcurrentDictionary<string, string> ConcurrentDictionary { get; private set; }

        public LinkedList<string> LinkedList { get; private set; }

        [Benchmark]
        public void CreateList()
        {
            List = new List<string>();
            for (var i = 0; i < DatasProvider.DataSource.Length; i++)
            {
                List.Add(DatasProvider.DataSource[i]);
            }
        }

        [Benchmark]
        public void CreateArray()
        {
            Array = new string[DatasProvider.Count];
            for (var i = 0; i < DatasProvider.DataSource.Length; i++)
            {
                Array[i] = DatasProvider.DataSource[i];
            }
        }

        [Benchmark]
        public void CreateHashSet()
        {
            HashSet = new HashSet<string>();
            for (var i = 0; i < DatasProvider.DataSource.Length; i++)
            {
                HashSet.Add(DatasProvider.DataSource[i]);
            }
        }

        [Benchmark]
        public void CreateDictionary()
        {
            Dictionary = new Dictionary<string, string>();
            for (var i = 0; i < DatasProvider.DataSource.Length; i++)
            {
                Dictionary.TryAdd(DatasProvider.DataSource[i], DatasProvider.DataSource[i]);
            }
        }

        [Benchmark]
        public void CreateSortedList()
        {
            SortedList = new SortedList<string, string>();
            for (var i = 0; i < DatasProvider.DataSource.Length; i++)
            {
                SortedList.TryAdd(DatasProvider.DataSource[i], DatasProvider.DataSource[i]);
            }
        }

        [Benchmark]
        public void CreateSortedSet()
        {
            SortedSet = new SortedSet<string>();
            for (var i = 0; i < DatasProvider.DataSource.Length; i++)
            {
                SortedSet.Add(DatasProvider.DataSource[i]);
            }
        }

        [Benchmark]
        public void CreateSortedDictionary()
        {
            SortedDictionary = new SortedDictionary<string, string>();
            for (var i = 0; i < DatasProvider.DataSource.Length; i++)
            {
                SortedDictionary.TryAdd(DatasProvider.DataSource[i], DatasProvider.DataSource[i]);
            }
        }

        [Benchmark]
        public void CreateConcurrentBag()
        {
            ConcurrentBag = new ConcurrentBag<string>();
            for (var i = 0; i < DatasProvider.DataSource.Length; i++)
            {
                ConcurrentBag.Add(DatasProvider.DataSource[i]);
            }
        }

        [Benchmark]
        public void CreateConcurrentDictionary()
        {
            ConcurrentDictionary = new ConcurrentDictionary<string, string>();
            for (var i = 0; i < DatasProvider.DataSource.Length; i++)
            {
                ConcurrentDictionary.TryAdd(DatasProvider.DataSource[i], DatasProvider.DataSource[i]);
            }
        }

        [Benchmark]
        public void CreateLinkedList()
        {
            LinkedList = new LinkedList<string>();
            for (var i = 0; i < DatasProvider.DataSource.Length; i++)
            {
                LinkedList.AddLast(DatasProvider.DataSource[i]);
            }
        }
    }
}