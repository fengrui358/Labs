using System;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace RedisLab.StringType
{
    public class StringTypeLab : RedisLab
    {
        public override async Task Run()
        {
            using var redis = await Init();

            var db = redis.GetDatabase(3);
            Console.WriteLine($"Get database {db.Database}");

            var key = Guid.NewGuid().ToString("N").Substring(0, 4);
            var value = Guid.NewGuid().ToString();
            var setResult = await db.StringSetAsync(key, value, flags: CommandFlags.FireAndForget); // FireAndForget 不关心结果
            Console.WriteLine($"Set string key: {key} value: {value} result: {setResult}");

            var getValue = await db.StringGetAsync(key);
            Console.WriteLine($"Get string key: {key} value: {getValue}");

            await Task.Delay(1000);
            var getValue2 = await db.StringGetAsync(key);
            Console.WriteLine($"After 1 s, get string key: {key} value: {getValue2}");
        }
    }
}
