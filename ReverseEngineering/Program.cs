using System;
using System.Threading.Tasks;

namespace ReverseEngineering
{
    /// <summary>
    /// https://mp.weixin.qq.com/s/I6lPHghGAUIuODf-bUAMJA
    /// </summary>
    class Program
    {
        static async Task Main(string[] args)
        {
            //使用DnSpy直接编辑方法，编辑变量
            var i = 10;
            var j = 20;
            Console.WriteLine($"{i}+{j}={i + j}");

            //使用DnSpy的IL指令编辑
            var id = await Query();
            Console.WriteLine(id);

            Console.ReadLine();
        }

        static async Task<string> Query()
        {
            return await Task.Run(async () =>
            {
                await Task.Delay(1000);
                return "teststring";
            });
        }
    }
}
