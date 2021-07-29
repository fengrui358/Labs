using System;
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

            Console.ReadLine();
        }
    }
}
