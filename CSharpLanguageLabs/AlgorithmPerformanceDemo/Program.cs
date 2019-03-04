using BenchmarkDotNet.Running;

namespace AlgorithmPerformanceDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            DatasProvider.Count = 100;
            //DatasProvider.Count = 10000;
            //DatasProvider.Count = 1000000;
            //DatasProvider.Count = 10000000;

            DatasProvider.Create();

            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
        }
    }
}
