using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timer
{
    class Program
    {
        static void Main(string[] args)
        {
            //初始化
            var mgr = new SessionManager();

            Console.WriteLine("运行中");
            Console.ReadKey();
        }
    }
}
