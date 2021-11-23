using System;
using System.Threading.Tasks;
using RedisLab.PubSub;
using RedisLab.SetType;
using RedisLab.StringType;

namespace RedisLab
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello Redis!");

            var stringTypeLab = new StringTypeLab();
            await stringTypeLab.Run();

            var pubSubLab = new PubSubLab();
            await pubSubLab.Run();

            var setTypeLab = new SetTypeLab();
            await setTypeLab.Run();

            var hashTypeLab = new HashType.HashTypeLab();
            await hashTypeLab.Run();

            Console.ReadLine();
        }
    }
}
