using System;
using System.Diagnostics;
using Exceptionless;

namespace ExceptionlessLab
{
    class Program
    {
        static void Main(string[] args)
        {
            var startSw = Stopwatch.StartNew();

            //apiKey get from https://be.exceptionless.io/
            ExceptionlessClient.Default.Startup("");

            startSw.Stop();
            Console.WriteLine($"启动注册完毕，耗时：{startSw.ElapsedMilliseconds}");

            for (int i = 0; i < 10; i++)
            {
                try
                {
                    throw new Exception($"{i}_Omg, 出错了！{DateTime.Now}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);

                    var sw = Stopwatch.StartNew();
                    ex.ToExceptionless().Submit();

                    sw.Stop();
                    Console.WriteLine($"提交异常完毕，耗时：{sw.ElapsedMilliseconds}ms");
                }
            }

            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }
}
