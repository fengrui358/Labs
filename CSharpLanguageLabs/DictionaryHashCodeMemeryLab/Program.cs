using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryHashCodeMemeryLab
{
    //期初实验的目的是查看不同类型Key对内存的影响，不过现在看来字符串的HashCode并不唯一
    class Program
    {
        private static Dictionary<string, string> _stringKeyDic = new Dictionary<string, string>();
        private static Dictionary<int, string> _intKeyDic = new Dictionary<int, string>();

        static void Main(string[] args)
        {
            PrintMemorySize();

            Console.WriteLine("创建第一轮对象");

            for (int i = 0; i < 1000000; i++)
            {
                var g = Guid.NewGuid();
                _stringKeyDic.Add(g.ToString(), g.ToString());
            }

            PrintMemorySize();

            Console.WriteLine("创建第二轮对象");

            for (int i = 0; i < 1000000; i++)
            {
                var g = Guid.NewGuid();
                try
                {
                    _intKeyDic.Add(g.ToString().GetHashCode(), g.ToString());
                }
                catch (ArgumentException e)
                {
                    var hashCode = g.ToString().GetHashCode();
                    var oldGuid = _intKeyDic[hashCode];

                    Console.WriteLine($"创建至第{i}个元素时出现Guid的HashCode出现重复，GUID：{oldGuid}和GUID{g}的HashCode均为{hashCode}");
                    break;
                }
            }

            PrintMemorySize();

            Console.Read();
        }

        private static void PrintMemorySize()
        {
            GC.Collect();

            var p = Process.GetCurrentProcess();
            Console.WriteLine("当前内存：" + p.PrivateMemorySize64 / 1024 + "KB");
        }
    }
}
