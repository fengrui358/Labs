using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thread
{
    class Program
    {
        static void Main(string[] args)
        {
            //初始化
            var mgr = new SessionManager();

            Console.WriteLine("运行中");
            while (true)
            {
                Console.WriteLine("输入gc，进行内存回收");

                var input = Console.ReadLine();
                if (input.Equals("gc", StringComparison.OrdinalIgnoreCase))
                {
                    GC.Collect();
                }
            }
        }
    }
}
