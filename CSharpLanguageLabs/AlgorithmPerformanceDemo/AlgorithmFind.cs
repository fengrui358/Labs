﻿using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using BenchmarkDotNet.Attributes;

namespace AlgorithmPerformanceDemo
{
    #region Count:10

    /* Count:10
|                   Method |       Mean |     Error |    StdDev |
|------------------------- |-----------:|----------:|----------:|
|                 FindList |  3.8779 ns | 0.1002 ns | 0.0937 ns |
|                FindArray |  0.4389 ns | 0.0245 ns | 0.0229 ns |
|              FindHashSet |  4.1967 ns | 0.0674 ns | 0.0631 ns |
|           FindDictionary |  6.6329 ns | 0.0381 ns | 0.0357 ns |
|           FindSortedList | 23.6474 ns | 0.2151 ns | 0.2012 ns |
|            FindSortedSet | 10.3935 ns | 0.1886 ns | 0.1764 ns |
|     FindSortedDictionary | 19.3021 ns | 0.2837 ns | 0.2654 ns |
|        FindConcurrentBag | 38.6849 ns | 0.4408 ns | 0.4123 ns |
| FindConcurrentDictionary | 19.4685 ns | 0.1667 ns | 0.1560 ns |
|           FindLinkedList |  5.6919 ns | 0.0491 ns | 0.0459 ns |
     
     */

    #endregion

    public class AlgorithmFind
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
        public void FindList()
        {
            var sw = Stopwatch.StartNew();

            _algorithmCreate.List.Find(s => s == DatasProvider.SearchTarget);

            sw.Stop();
            Console.WriteLine($"{MethodBase.GetCurrentMethod().Name}--{sw.ElapsedTicks}");
        }

        [Benchmark]
        public void FindArray()
        {
            var sw = Stopwatch.StartNew();

            for (var i = 0; i < _algorithmCreate.Array.Length; i++)
            {
                if (_algorithmCreate.Array[i] == DatasProvider.SearchTarget)
                {
                    break;
                }
            }

            sw.Stop();
            Console.WriteLine($"{MethodBase.GetCurrentMethod().Name}--{sw.ElapsedTicks}");
        }

        [Benchmark]
        public void FindHashSet()
        {
            var sw = Stopwatch.StartNew();

            var i = 0;
            if (_algorithmCreate.HashSet.Contains(DatasProvider.NewTarget))
            {
                GC.KeepAlive(i);
            }

            sw.Stop();
            Console.WriteLine($"{MethodBase.GetCurrentMethod().Name}--{sw.ElapsedTicks}");
        }

        [Benchmark]
        public void FindDictionary()
        {
            var sw = Stopwatch.StartNew();

            var i = 0;
            if (_algorithmCreate.Dictionary.TryGetValue(DatasProvider.SearchTarget, out _))
            {
                GC.KeepAlive(i);
            }

            sw.Stop();
            Console.WriteLine($"{MethodBase.GetCurrentMethod().Name}--{sw.ElapsedTicks}");
        }

        [Benchmark]
        public void FindSortedList()
        {
            var sw = Stopwatch.StartNew();

            var i = _algorithmCreate.SortedList.IndexOfKey(DatasProvider.SearchTarget);
            GC.KeepAlive(i);

            sw.Stop();
            Console.WriteLine($"{MethodBase.GetCurrentMethod().Name}--{sw.ElapsedTicks}");
        }

        [Benchmark]
        public void FindSortedSet()
        {
            var sw = Stopwatch.StartNew();

            var i = _algorithmCreate.SortedSet.TryGetValue(DatasProvider.SearchTarget, out _);
            GC.KeepAlive(i);

            sw.Stop();
            Console.WriteLine($"{MethodBase.GetCurrentMethod().Name}--{sw.ElapsedTicks}");
        }

        [Benchmark]
        public void FindSortedDictionary()
        {
            var sw = Stopwatch.StartNew();

            var i = _algorithmCreate.SortedDictionary.TryGetValue(DatasProvider.SearchTarget, out _);
            GC.KeepAlive(i);

            sw.Stop();
            Console.WriteLine($"{MethodBase.GetCurrentMethod().Name}--{sw.ElapsedTicks}");
        }

        [Benchmark]
        public void FindConcurrentBag()
        {
            var sw = Stopwatch.StartNew();

            var i = _algorithmCreate.ConcurrentBag.FirstOrDefault(s => s == DatasProvider.SearchTarget);
            GC.KeepAlive(i);

            sw.Stop();
            Console.WriteLine($"{MethodBase.GetCurrentMethod().Name}--{sw.ElapsedTicks}");
        }

        [Benchmark]
        public void FindConcurrentDictionary()
        {
            var sw = Stopwatch.StartNew();

            var i = _algorithmCreate.ConcurrentDictionary.TryGetValue(DatasProvider.SearchTarget, out _);
            GC.KeepAlive(i);

            sw.Stop();
            Console.WriteLine($"{MethodBase.GetCurrentMethod().Name}--{sw.ElapsedTicks}");
        }

        [Benchmark]
        public void FindLinkedList()
        {
            var sw = Stopwatch.StartNew();

            var i = _algorithmCreate.LinkedList.Find(DatasProvider.SearchTarget);
            GC.KeepAlive(i);

            sw.Stop();
            Console.WriteLine($"{MethodBase.GetCurrentMethod().Name}--{sw.ElapsedTicks}");
        }
    }
}