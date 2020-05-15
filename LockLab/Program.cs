using System;

namespace LockLab
{
    class Program
    {
        static void Main(string[] args)
        {
            var task = new ConcurrentTask();

            task.SemaphoreSlimTest();
            task.ReaderWriterLockSlimTest();
            task.LockTest();

            Console.ReadLine();
        }
    }
}
