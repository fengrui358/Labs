using System;
using System.Threading.Tasks;
using Polly;

namespace PollyLab
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var policy = Policy.Handle<Exception>().WaitAndRetry(
                new[]
                {
                    TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(8)
                }, (exception, timeSpan, count, context) =>
                {
                    Console.WriteLine($"error {exception}, retry {count}");
                });

            policy.Execute(() =>
            {
                // 同步 Tenants
                Console.WriteLine("执行业务逻辑 " + DateTime.Now);

                throw new Exception("测试抛出异常 polly 的行为");
            });

            var policyAsync = Policy.Handle<Exception>().WaitAndRetryAsync(
                new[]
                {
                    TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(8)
                }, (exception, timeSpan, count, context) =>
                {
                    Console.WriteLine($"error {exception}, retry {count}");
                });

            await policyAsync.ExecuteAsync(async () =>
            {
                await Task.Delay(100);

                Console.WriteLine("执行业务逻辑 " + DateTime.Now);

                throw new Exception("测试抛出异常 polly 的行为");
            });

            Console.WriteLine("退出");
            Console.ReadLine();
        }
    }
}
