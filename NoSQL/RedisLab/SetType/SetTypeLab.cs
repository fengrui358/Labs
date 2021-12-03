using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using StackExchange.Redis;

namespace RedisLab.SetType
{
    public class SetTypeLab : RedisLab
    {
        public override async Task Run()
        {
            using var redis = await Init();

            var db = redis.GetDatabase(0);
            await db.SetAddAsync("setA", new RedisValue[] { 1, 2, 3 }, CommandFlags.FireAndForget);
            await db.SetAddAsync("setB", new RedisValue[] { 1, 2, 3, 4 }, CommandFlags.FireAndForget);
            await db.SetCombineAndStoreAsync(SetOperation.Difference, "setC", "setB", "setA");

            var setC = await db.SetMembersAsync("setC");
            Assert.AreEqual(1, setC.Length);
            Assert.AreEqual(4, (int)setC[0]);

            await db.SetCombineAndStoreAsync(SetOperation.Union, "setD", new RedisKey[] { "setA", "setC" });
            var setD = await db.SetMembersAsync("setD");
            Assert.AreEqual(4, setD.Length);

            await db.SetCombineAndStoreAsync(SetOperation.Intersect, "setE", new RedisKey[] { "setD", "setA" });
            var setE = await db.SetMembersAsync("setE");
            Assert.AreEqual(3, setE.Length);

            await db.StringSetAsync("setExpired", "", TimeSpan.FromSeconds(3), flags: CommandFlags.FireAndForget);
            await Task.Delay(TimeSpan.FromSeconds(2));
            // 延长过期时间
            await db.StringSetAsync("setExpired", "", expiry: TimeSpan.FromSeconds(5));
            var t = await db.KeyTimeToLiveAsync("setExpired");
            Console.WriteLine($"发送 setExpired，剩余过期时间：{t}");
            await Task.Delay(TimeSpan.FromSeconds(2));
            var setExpired = await db.StringGetAsync("setExpired");
            Assert.IsFalse(setExpired.IsNull, "setExpired is not null");
            Assert.AreEqual("", setExpired.ToString());
            await Task.Delay(TimeSpan.FromSeconds(3));
            setExpired = await db.StringGetAsync("setExpired");
            Assert.IsTrue(setExpired.IsNull, "setExpired is null");
        }
    }
}
