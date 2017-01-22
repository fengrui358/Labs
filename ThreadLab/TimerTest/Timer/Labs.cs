using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timer
{
    class Labs
    {
        public void SessionLab()
        {
            //初始化
            var mgr = new SessionManager();

            Console.WriteLine("SessionLab运行中");

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

        private const int TimerArrayLimit = 10000000;
        private readonly System.Threading.Timer[] _timer = new System.Threading.Timer[TimerArrayLimit];

        /// <summary>
        /// 构造1W个Timer，看内存增长以及遍历改变触发时机的时间消耗
        /// </summary>
        public void TimerPerformanceLab()
        {
            var sw = Stopwatch.StartNew();
            var startMemery = Process.GetCurrentProcess().WorkingSet64;
            Console.WriteLine($"起始内存：{startMemery / 1024}kb");

            for (int i = 0; i < TimerArrayLimit; i++)
            {
                _timer[i] = new System.Threading.Timer(s => { });
            }

            sw.Stop();

            var endMemery = Process.GetCurrentProcess().WorkingSet64;
            Console.WriteLine(
                $"构造{TimerArrayLimit}个Timer完成，当前内存：{endMemery/1024}kb，增加：{(endMemery - startMemery)/1024}kb，1个Timer内存大约{(endMemery - startMemery)/TimerArrayLimit}byte");
            Console.WriteLine($"构造{TimerArrayLimit}个Timer耗时：{sw.ElapsedMilliseconds}ms，构造1个Timer大约耗时{sw.ElapsedTicks / TimerArrayLimit}tick");

            sw.Restart();
            for (int i = 0; i < TimerArrayLimit; i++)
            {
                _timer[i].Change(1000000, 0);
            }

            sw.Stop();
            Console.WriteLine($"改变{TimerArrayLimit}个Timer触发时间耗时：{sw.ElapsedMilliseconds}ms，改变1个Timer大约耗时{sw.ElapsedTicks / TimerArrayLimit}tick");
        }
    }
}
