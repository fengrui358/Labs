using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSubscribeLab
{
    /// <summary>
    /// 测试事件订阅影响GC回收
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Test(1);
            Test(2);
            Test(3);

            Console.WriteLine("结论：如果仅仅释放订阅者，发布者还存在，那么订阅者的内存并不会被GC回收！！需要重点注意！！");
            Console.Read();
        }

        /// <summary>
        /// 进行实验，key为1时释放发布者，key为2时释放订阅者，key为3时释放两者
        /// </summary>
        /// <param name="key"></param>
        private static void Test(int key)
        {
            var lab = new Lab();
            PrintExist(lab);

            switch (key)
            {
                //key为1时释放发布者
                case 1:
                    Console.WriteLine("释放发布者");
                    lab.ReleasePublisher();
                    break;
                //key为2时释放订阅者
                case 2:
                    Console.WriteLine("释放订阅者");
                    lab.ReleaseSubscriber();
                    break;
                //key为3时释放两者
                case 3:
                    Console.WriteLine("释放两者");
                    lab.ReleasePublisher();
                    lab.ReleaseSubscriber();
                    break;

            }
            
            PrintExist(lab);
            Console.WriteLine();
        }

        /// <summary>
        /// 打印存在情况
        /// </summary>
        private static void PrintExist(Lab lab)
        {
            Console.WriteLine($"发布者是否存在：{lab.TestExistPublisher()}---订阅者是否存在：{lab.TestExistSubscriber()}");
        }
    }
}
