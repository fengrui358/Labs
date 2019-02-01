using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ArrayAndLinkedListCompare
{
    /// <summary>
    /// 比较C#中的数组(List)和链表(LinkedList)的性能差异
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("初始化测试数据，输入最大数据量(大于1000)：");
            var maxCount = Convert.ToInt32(Console.ReadLine());
            var source = new List<string>(maxCount);
            var targets = new List<string>(3);

            for (int i = 0; i < maxCount; i++)
            {
                var s = Guid.NewGuid().ToString("N");
                if (i == maxCount - 10)
                {
                    targets.Add(s);
                }

                source.Add(s);
            }

            Console.WriteLine("是否预分配数组大小？(Y/N):");
            var b = Console.ReadLine();

            var sw = Stopwatch.StartNew();
            List<string> list = null;

            if (b.Equals("y", StringComparison.OrdinalIgnoreCase))
            {
                list = new List<string>(maxCount);
            }
            else
            {
                list = new List<string>();
            }

            for (int i = 0; i < maxCount; i++)
            {
                list.Add(source[i]);
            }

            sw.Stop();

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"List 加载完毕，耗时：{sw.ElapsedMilliseconds}ms");

            sw.Restart();
            var linkedList = new LinkedList<string>();

            var current = linkedList.AddFirst(source[0]);
            for (int i = 1; i < maxCount; i++)
            {
                current = linkedList.AddAfter(current, source[i]);
            }

            sw.Stop();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"LinkedList 加载完毕，耗时：{sw.ElapsedMilliseconds}ms");

            sw.Restart();

            var listFind = new List<string>(3);
            foreach (var target in targets)
            {
                var f = list.FirstOrDefault(s => s == target);
                if (f != null)
                {
                    listFind.Add(f);
                }
            }

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"List 查找完毕，耗时：{sw.ElapsedMilliseconds}ms");

            sw.Restart();

            var linkedListFind = new List<string>(3);
            foreach (var target in targets)
            {
                var f = linkedList.FirstOrDefault(s => s == target);
                if (f != null)
                {
                    linkedListFind.Add(f);
                }
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"LinkedList 查找完毕，耗时：{sw.ElapsedMilliseconds}ms");

            sw.Restart();
            foreach (var s in listFind)
            {
                list.Remove(s);
            }

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"List 移除完毕，耗时：{sw.ElapsedMilliseconds}ms");

            sw.Restart();
            foreach (var s in linkedListFind)
            {
                linkedList.Remove(s);
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"LinkedList 移除完毕，耗时：{sw.ElapsedMilliseconds}ms");

            Console.ResetColor();
        }
    }
}
