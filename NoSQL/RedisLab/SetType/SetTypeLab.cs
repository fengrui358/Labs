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

            var db = redis.GetDatabase(5);
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

            db.StringSetAsync("setExpired", "free", TimeSpan.FromSeconds(5), flags: CommandFlags.FireAndForget);
        }
    }
}
