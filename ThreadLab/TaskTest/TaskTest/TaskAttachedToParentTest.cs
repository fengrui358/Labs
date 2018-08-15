using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaskTest
{
    /// <summary>
    /// 《C#并发编程经典实例》 P41
    /// </summary>
    public class TaskAttachedToParentTest
    {
        public static async Task ParentTask()
        {
            await Task.Factory.StartNew(() =>
                {
                    Console.WriteLine(
                        $"{DateTime.Now:HH:mm:ss ffff} Parent task begin, id is {Thread.CurrentThread.ManagedThreadId}");

                    StartSubTask();
                });

            Console.WriteLine(
                $"{DateTime.Now:HH:mm:ss ffff} Parent task end, id is {Thread.CurrentThread.ManagedThreadId}");
        }

        private static void StartSubTask()
        {
            Task.Factory.StartNew(() =>
            {
                Console.WriteLine(
                    $"{DateTime.Now:HH:mm:ss ffff} Child task begin, id is {Thread.CurrentThread.ManagedThreadId}");

                Thread.Sleep(1000);

                Console.WriteLine(
                    $"{DateTime.Now:HH:mm:ss ffff} Child task end, id is {Thread.CurrentThread.ManagedThreadId}");
            }, TaskCreationOptions.AttachedToParent);
            //}, TaskCreationOptions.None);  如果不设置AttachedToParent父线程不会等待就直接结束
        }
    }
}
