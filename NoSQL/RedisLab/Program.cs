using System;
using System.Threading.Tasks;
using RedisLab.StringType;

namespace RedisLab
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello Redis!");

            var stringTypeLab = new StringTypeLab();
            await stringTypeLab.Init();
            await stringTypeLab.Run();

            Console.ReadLine();
        }
    }
}
