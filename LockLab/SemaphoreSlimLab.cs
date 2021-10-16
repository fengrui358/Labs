using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LockLab
{
    public class SemaphoreSlimLab
    {
        private readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1);

        public void Run()
        {
            var task1 = new Task(async () =>
            {
                await _semaphoreSlim.WaitAsync();
                Console.WriteLine("任务 1 启动");
                await Task.Delay(5000);
                Console.WriteLine("任务 1 结束");
                _semaphoreSlim.Release();
            });

            var task2 = new Task(async () =>
            {
                await Task.Delay(300);
                await _semaphoreSlim.WaitAsync();
                Console.WriteLine("任务 2 启动");
                await Task.Delay(5000);
                Console.WriteLine("任务 2 结束");
                _semaphoreSlim.Release();
            });

            var task3 = new Task(async () =>
            {
                try
                {
                    if (await _semaphoreSlim.WaitAsync(TimeSpan.FromMilliseconds(1000)))
                    {
                        Console.WriteLine("任务 3 启动");
                        await Task.Delay(3000);
                        Console.WriteLine("任务 3 结束");
                    }
                    else
                    {
                        Console.WriteLine("任务 3 启动失败");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                finally
                {
                    _semaphoreSlim.Release();
                }
            });


            task1.Start();
            task2.Start();
            task3.Start();
        }
    }
}
