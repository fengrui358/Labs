using BenchmarkDotNet.Running;
using System;

namespace SpikeBenchmark
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<SpikeRunner>();
            //Console.WriteLine(summary);
            Console.ReadLine();
        }
    }
}
