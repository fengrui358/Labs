using System;
using System.Collections.Generic;
using System.Threading;

namespace GcLab
{
    class Program
    {
        private static readonly List<Tuple<string, WeakReference>> WeakReferences = new List<Tuple<string, WeakReference>>();

        static void Main(string[] args)
        {
            Create();
            Thread.Sleep(1000);
            GC.Collect();

            Print();

            Thread.Sleep(3000);

            //通知各个线程停止执行
            Stop();
            Thread.Sleep(2000);
            GC.Collect();
            
            Print();

            Console.Read();
        }

        private static void Create()
        {
            Create(typeof(ThreadingTest));
            Create(typeof(ThreadingPoolTest));
            Create(typeof(TaskTest));
            Create(typeof(TimerTest));
            Create(typeof(ThreadingTimerTest));
        }

        private static void Create(Type type)
        {
            if (type != null && typeof(IThreadingLab).IsAssignableFrom(type))
            {
                var obj = Activator.CreateInstance(type);
                if (obj is IThreadingLab threadingLab)
                {
                    WeakReferences.Add(new Tuple<string, WeakReference>(type.Name,
                        new WeakReference(threadingLab)));
                    threadingLab.Start();

                    return;
                }
            }

            Console.WriteLine($"{type?.FullName} create error.");
        }

        private static void Stop()
        {   
            foreach (var weakReference in WeakReferences)
            {
                if (weakReference.Item2.IsAlive && weakReference.Item2.Target is IThreadingLab threadingLab)
                {
                    threadingLab.Stop();
                }
                else
                {
                    Console.WriteLine($"{weakReference.Item1} stop error.");
                }
            }
        }

        private static void Print()
        {
            foreach (var weakReference in WeakReferences)
            {
                Console.WriteLine($"{weakReference.Item1}:{weakReference.Item2.IsAlive}");
            }
        }
    }
}
