using System.Linq;
using BenchmarkDotNet.Running;

namespace BitOperateLab
{
    class Program
    {
        static void Main(string[] args)
        {
            var b = new BitOperateBenchmark();
            b.CollectionJudge((int[]) b.CollectionJudgeSource().First()[0],
                (int[]) b.CollectionJudgeSource().First()[1]);
            b.CollectionJudgeWithBitOperate((int[])b.CollectionJudgeSource().First()[0],
                (int[])b.CollectionJudgeSource().First()[1]);

            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
        }
    }
}