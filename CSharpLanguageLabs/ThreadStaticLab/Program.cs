using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadStaticLab
{
    class Program
    {
        private static int _testField;

        [ThreadStatic]
        private static int _testFieldWithThreadStatic;

        static void Main(string[] args)
        {
            _testField = 5;
            _testFieldWithThreadStatic = 10;

            Console.WriteLine($"主线程Id:{Thread.CurrentThread.ManagedThreadId}---设置普通静态字段值为{_testField}，设置特性静态字段值为{_testFieldWithThreadStatic}");

            Task.Run(() =>
            {
                Console.WriteLine($"线程Id:{Thread.CurrentThread.ManagedThreadId}---读取普通静态字段为{_testField}，读取特性静态字段值为{_testFieldWithThreadStatic}");

                _testField = 8;
                _testFieldWithThreadStatic = 15;

                Console.WriteLine($"线程Id:{Thread.CurrentThread.ManagedThreadId}---设置普通静态字段值为{_testField}，设置特性静态字段值为{_testFieldWithThreadStatic}");
            }).Wait();

            Console.WriteLine($"主线程Id:{Thread.CurrentThread.ManagedThreadId}---设置普通静态字段值为{_testField}，设置特性静态字段值为{_testFieldWithThreadStatic}");
            Console.ReadKey();
        }
    }
}
