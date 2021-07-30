using System;
using LinqLab.GroupBy;
using LinqLab.GroupJoin;
using LinqLab.OfType;
using LinqLab.Where;

namespace LinqLab
{
    /// <summary>
    /// https://www.tutorialsteacher.com/linq/linq-tutorials
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"{nameof(Where1)}:{Environment.NewLine}");
            new Where1().Test();
            Console.WriteLine();

            Console.WriteLine($"{nameof(Where2)}:{Environment.NewLine}");
            new Where2().Test();
            Console.WriteLine();

            Console.WriteLine($"{nameof(OfType1)}:{Environment.NewLine}");
            new OfType1().Test();
            Console.WriteLine();

            Console.WriteLine($"{nameof(OrderBy.OrderBy)}:{Environment.NewLine}");
            new OrderBy.OrderBy().Test();
            Console.WriteLine();

            Console.WriteLine($"{nameof(Join.Join)}:{Environment.NewLine}");
            new Join.Join().Test();
            Console.WriteLine();

            Console.WriteLine($"{nameof(GroupBy1)}:{Environment.NewLine}");
            new GroupBy1().Test();
            Console.WriteLine();

            Console.WriteLine($"{nameof(GroupJoin1)}:{Environment.NewLine}");
            new GroupJoin1().Test();
            Console.WriteLine();

            Console.ReadLine();
        }
    }
}
