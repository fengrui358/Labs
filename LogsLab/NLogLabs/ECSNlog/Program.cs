using System;
using System.Threading.Tasks;
using NLog;

namespace ECSNLogLab
{
    class Program
    {
        private static readonly ILogger Log = LogManager.GetCurrentClassLogger();

        static async Task Main(string[] args)
        {
            var r = new Random();
            while (true)
            {
                var interval = r.Next(500, 5000);

                if (interval > 4000)
                {
                    if (interval > 4500)
                    {
                        var x = Throw1(0);
                        Console.WriteLine(x);
                    }
                    else
                    {
                        Throw2(interval);
                    }
                }
                else
                {
                    Log.Info("测试，index:{interval}", interval);
                }

                await Task.Delay(interval);
            }
        }

        static int Throw1(int num)
        {
            try
            {
                var c = 45 / num;
                return c;
            }
            catch (Exception e)
            {
                Log.Error(e);
            }

            return 0;
        }

        static int Throw2(int interval)
        {
            try
            {
                throw new ArgumentException($"异常 argument is {interval}");
            }
            catch (Exception e)
            {
                Log.Error(e);
            }

            return 0;
        }
    }
}
