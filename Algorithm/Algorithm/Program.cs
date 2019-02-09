using System;
using System.Linq;
using System.Reflection;
using BenchmarkDotNet.Running;

namespace Algorithm
{
    class Program
    {
        public static int DataGroupCount { get; set; } = 1;

        static void Main(string[] args)
        {
            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
        }
    }
}