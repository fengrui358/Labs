using System;
using System.Runtime.InteropServices;

namespace NumericalSizeLab
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"{nameof(Single)}.{nameof(Marshal.SizeOf)}:{Marshal.SizeOf<Single>()}");
            Console.WriteLine($"{nameof(Double)}.{nameof(Marshal.SizeOf)}:{Marshal.SizeOf<Double>()}");
            Console.WriteLine($"{nameof(Int32)}.{nameof(Marshal.SizeOf)}:{Marshal.SizeOf<Int32>()}");
            Console.WriteLine($"{nameof(Int64)}.{nameof(Marshal.SizeOf)}:{Marshal.SizeOf<Int64>()}");
            Console.WriteLine();

            Console.WriteLine($"{nameof(Single)}.{nameof(Single.MinValue)}:{Single.MinValue}");
            Console.WriteLine($"{nameof(Double)}.{nameof(Double.MinValue)}:{Double.MinValue}");
            Console.WriteLine($"{nameof(Int32)}.{nameof(Int32.MinValue)}:{Int32.MinValue}");
            Console.WriteLine($"{nameof(Int64)}.{nameof(Int64.MinValue)}:{Int64.MinValue}");
            Console.WriteLine($"{nameof(Single)}.{nameof(Single.MaxValue)}:{Single.MaxValue}");
            Console.WriteLine($"{nameof(Double)}.{nameof(Double.MaxValue)}:{Double.MaxValue}");
            Console.WriteLine($"{nameof(Int32)}.{nameof(Int32.MaxValue)}:{Int32.MaxValue}");
            Console.WriteLine($"{nameof(Int64)}.{nameof(Int64.MaxValue)}:{Int64.MaxValue}");

            Console.ReadLine();
        }
    }
}