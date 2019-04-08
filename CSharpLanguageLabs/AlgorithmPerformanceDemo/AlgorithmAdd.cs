using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using BenchmarkDotNet.Attributes;

namespace AlgorithmPerformanceDemo
{
    public class AlgorithmAdd
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
        public void AddList()
        {
            var sw = Stopwatch.StartNew();

            _algorithmCreate.List.Add(DatasProvider.NewTarget);

            sw.Stop();
            Console.WriteLine($"{MethodBase.GetCurrentMethod().Name}--{sw.ElapsedTicks}");
        }

        [Benchmark]
        public void AddArray()
        {
            var sw = Stopwatch.StartNew();

            var newArray = new Guid[DatasProvider.Count + 1];
            _algorithmCreate.Array.CopyTo(newArray, 0);
            newArray[DatasProvider.Count] = DatasProvider.NewTarget;

            sw.Stop();
            Console.WriteLine($"{MethodBase.GetCurrentMethod().Name}--{sw.ElapsedTicks}");
        }

        [Benchmark]
        public void AddHashSet()
        {
            var sw = Stopwatch.StartNew();

            _algorithmCreate.HashSet.Add(DatasProvider.NewTarget);

            sw.Stop();
            Console.WriteLine($"{MethodBase.GetCurrentMethod().Name}--{sw.ElapsedTicks}");
        }

        [Benchmark]
        public void AddDictionary()
        {
            var sw = Stopwatch.StartNew();

            _algorithmCreate.Dictionary.TryAdd(DatasProvider.NewTarget, DatasProvider.NewTarget);

            sw.Stop();
            Console.WriteLine($"{MethodBase.GetCurrentMethod().Name}--{sw.ElapsedTicks}");
        }

        [Benchmark]
        public void AddSortedList()
        {
            var sw = Stopwatch.StartNew();

            _algorithmCreate.SortedList.TryAdd(DatasProvider.NewTarget, DatasProvider.NewTarget);

            sw.Stop();
            Console.WriteLine($"{MethodBase.GetCurrentMethod().Name}--{sw.ElapsedTicks}");
        }

        [Benchmark]
        public void AddSortedSet()
        {
            var sw = Stopwatch.StartNew();

            _algorithmCreate.SortedSet.Add(DatasProvider.NewTarget);

            sw.Stop();
            Console.WriteLine($"{MethodBase.GetCurrentMethod().Name}--{sw.ElapsedTicks}");
        }

        [Benchmark]
        public void AddSortedDictionary()
        {
            var sw = Stopwatch.StartNew();

            _algorithmCreate.SortedDictionary.TryAdd(DatasProvider.NewTarget, DatasProvider.NewTarget);

            sw.Stop();
            Console.WriteLine($"{MethodBase.GetCurrentMethod().Name}--{sw.ElapsedTicks}");
        }

        [Benchmark]
        public void AddConcurrentBag()
        {
            var sw = Stopwatch.StartNew();

            _algorithmCreate.ConcurrentBag.Add(DatasProvider.NewTarget);

            sw.Stop();
            Console.WriteLine($"{MethodBase.GetCurrentMethod().Name}--{sw.ElapsedTicks}");
        }

        [Benchmark]
        public void AddConcurrentDictionary()
        {
            var sw = Stopwatch.StartNew();

            _algorithmCreate.ConcurrentDictionary.TryAdd(DatasProvider.NewTarget, DatasProvider.NewTarget);

            sw.Stop();
            Console.WriteLine($"{MethodBase.GetCurrentMethod().Name}--{sw.ElapsedTicks}");
        }

        [Benchmark]
        public void AddLinkedList()
        {
            var sw = Stopwatch.StartNew();

            _algorithmCreate.LinkedList.AddLast(DatasProvider.NewTarget);

            sw.Stop();
            Console.WriteLine($"{MethodBase.GetCurrentMethod().Name}--{sw.ElapsedTicks}");
        }
    }
}