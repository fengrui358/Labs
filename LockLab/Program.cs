using System;
using System.Threading.Tasks;

namespace LockLab
{
    class Program
    {
        static void Main(string[] args)
        {
            //var task = new ConcurrentTask();

            //task.SemaphoreSlimTest();
            //task.ReaderWriterLockSlimTest();
            //task.LockTest();

            var semaphoreSlimLab = new SemaphoreSlimLab();
            semaphoreSlimLab.Run();

            Console.ReadLine();
        }
    }
}
