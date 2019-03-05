using BenchmarkDotNet.Running;

namespace AlgorithmPerformanceDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            DatasProvider.Count = 10;
            //DatasProvider.Count = 100;
            //DatasProvider.Count = 10000;
            //DatasProvider.Count = 1000000;
            //DatasProvider.Count = 10000000;
            //var c = new AlgorithmAdd();
            //c.GlobalSetup();
            //c.AddList();
            //c.AddArray();
            //c.AddHashSet();
            //c.AddDictionary();
            //c.AddSortedList();
            //c.AddSortedSet();
            //c.AddSortedDictionary();
            //c.AddConcurrentBag();
            //c.AddConcurrentDictionary();
            //c.AddLinkedList();

            //var c2 = new AlgorithmCreate();
            //c2.CreateList();
            //c2.CreateArray();
            //c2.CreateHashSet();
            //c2.CreateDictionary();
            //c2.CreateSortedList();
            //c2.CreateSortedSet();
            //c2.CreateSortedDictionary();
            //c2.CreateConcurrentBag();
            //c2.CreateConcurrentDictionary();
            //c2.CreateLinkedList();

            //var c3 = new AlgorithmFind();
            //c3.GlobalSetup();
            //c3.FindList();
            //c3.FindArray();
            //c3.FindHashSet();
            //c3.FindDictionary();
            //c3.FindSortedList();
            //c3.FindSortedSet();
            //c3.FindSortedDictionary();
            //c3.FindConcurrentBag();
            //c3.FindConcurrentDictionary();
            //c3.FindLinkedList();

            //var c4 = new AlgorithmRemove();
            //c4.GlobalSetup();
            //c4.RemoveList();
            //c4.RemoveArray();
            //c4.RemoveHashSet();
            //c4.RemoveDictionary();
            //c4.RemoveSortedList();
            //c4.RemoveSortedSet();
            //c4.RemoveSortedDictionary();
            //c4.RemoveConcurrentDictionary();
            //c4.RemoveLinkedList();

            //var c5 = new AlgorithmToList();
            //c5.GlobalSetup();
            //c5.ToListList();
            //c5.ToListArray();
            //c5.ToListHashSet();
            //c5.ToListDictionary();
            //c5.ToListSortedList();
            //c5.ToListSortedSet();
            //c5.ToListSortedDictionary();
            //c5.ToListConcurrentBag();
            //c5.ToListConcurrentDictionary();
            //c5.ToListLinkedList();

            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
        }
    }
}