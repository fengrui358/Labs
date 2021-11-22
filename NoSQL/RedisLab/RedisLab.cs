using System.Threading.Tasks;
using StackExchange.Redis;

namespace RedisLab
{
    public abstract class RedisLab
    {
        protected RedisLab()
        {
            
        }

        public async Task<ConnectionMultiplexer> Init()
        {
            return await ConnectionMultiplexer.ConnectAsync("localhost:6379", (options) => options.Password = "abc123");
        }

        public abstract Task Run();
    }
}
