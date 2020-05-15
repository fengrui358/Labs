using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace LockLab
{
    public class ConcurrentTask
    {
        //防止编译器优化的一个缓存
        private readonly List<string> _temp;
        private readonly object _lockObj = new object();
        private readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1, 1);
        private readonly ReaderWriterLockSlim _readerWriterLockSlim = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

        private int _initCount = 20000000;
        private int _readCount = 1000000;
        private int _writeCount = 1000000;
        private int _readThreadCount = 10;

        private readonly Random _random = new Random();

        private readonly List<string> _list = new List<string>();

        public ConcurrentTask()
        {
            _temp = new List<string>(2 * _initCount);

            for (int i = 0; i < _initCount; i++)
            {
                var item = Guid.NewGuid().ToString();

                _list.Add(item);
            }
        }

        public void LockTest()
        {
            var list = _list.ToList();

            var tasks = new List<Task>();
            for (int i = 0; i < _readThreadCount; i++)
            {
                var taskRead = new Task(() =>
                {
                    var readCount = _readCount;
                    while (readCount-- > 0)
                    {
                        lock (_lockObj)
                        {
                            _temp.Add(list[_random.Next(0, list.Count)]);
                        }
                    }
                });

                tasks.Add(taskRead);
            }

            var taskWrite = new Task(() =>
            {
                var writeCount = _writeCount;
                while (writeCount-- > 0)
                {
                    lock (_lockObj)
                    {
                        list.Add(Guid.NewGuid().ToString());
                    }
                }
            });

            tasks.Add(taskWrite);

            GetRunTime(tasks);
        }

        public void SemaphoreSlimTest()
        {
            var list = _list.ToList();

            var tasks = new List<Task>();
            for (int i = 0; i < _readThreadCount; i++)
            {
                var taskRead = new Task(async () =>
                {
                    var readCount = _readCount;
                    while (readCount-- > 0)
                    {
                        await _semaphoreSlim.WaitAsync();
                        _temp.Add(list[_random.Next(0, list.Count)]);
                        _semaphoreSlim.Release();
                    }
                });

                tasks.Add(taskRead);
            }

            var taskWrite = new Task(async () =>
            {
                var writeCount = _writeCount;
                while (writeCount-- > 0)
                {
                    await _semaphoreSlim.WaitAsync();
                    list.Add(Guid.NewGuid().ToString());
                    _semaphoreSlim.Release();
                }
            });

            tasks.Add(taskWrite);

            GetRunTime(tasks);
        }

        public void ReaderWriterLockSlimTest()
        {
            var list = _list.ToList();

            var tasks = new List<Task>();
            for (int i = 0; i < _readThreadCount; i++)
            {
                var taskRead = new Task(() =>
                {
                    var readCount = _readCount;
                    while (readCount-- > 0)
                    {
                        _readerWriterLockSlim.EnterReadLock();
                        _temp.Add(list[_random.Next(0, list.Count)]);
                        _readerWriterLockSlim.ExitReadLock();
                    }
                });

                tasks.Add(taskRead);
            }

            var taskWrite = new Task(() =>
            {
                var writeCount = _writeCount;
                while (writeCount-- > 0)
                {
                    _readerWriterLockSlim.EnterWriteLock();
                    list.Add(Guid.NewGuid().ToString());
                    _readerWriterLockSlim.ExitWriteLock();
                }
            });

            tasks.Add(taskWrite);

            GetRunTime(tasks);
        }

        private void GetRunTime(List<Task> tasks)
        {
            StackTrace trace = new StackTrace();
            StackFrame frame = trace.GetFrame(1);//1代表上级，2代表上上级，以此类推
            MethodBase method = frame.GetMethod();

            var sw = Stopwatch.StartNew();

            foreach (var task in tasks)
            {
                task.Start();
            }

            Task.WaitAll(tasks.ToArray());

            sw.Stop();
            Console.WriteLine($"{method.Name}耗时{sw.ElapsedMilliseconds}");
        }
    }
}
