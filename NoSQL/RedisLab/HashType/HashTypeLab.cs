using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using StackExchange.Redis;

namespace RedisLab.HashType
{
    public class HashTypeLab : RedisLab
    {
        public override async Task Run()
        {
            var redis = await Init();
            var db = redis.GetDatabase(5);

            await db.HashSetAsync("hashA", new HashEntry[] { new ("Id", 1), new ("Name", "free") });
            var exist = await db.HashExistsAsync("hashA", "Id");
            Assert.AreEqual(true, exist);
        }
    }
}
