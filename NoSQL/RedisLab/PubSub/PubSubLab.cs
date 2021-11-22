using System;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace RedisLab.PubSub
{
    public class PubSubLab : RedisLab
    {
        /// <summary>
        /// https://stackexchange.github.io/StackExchange.Redis/PubSubOrder
        /// </summary>
        /// <returns></returns>
        public override async Task Run()
        {
            var subMultiplexer = await Init();
            var subscriber = subMultiplexer.GetSubscriber();

            // 不保证顺序，性能更好的方式（推荐使用）
            await subscriber.SubscribeAsync("test", (channel, message) =>
            {
                Console.WriteLine($"不保证接收顺序的方式 received {channel} message: {message}");
            }, CommandFlags.FireAndForget);

            // 保证顺序，性能稍差的方式
            var channel = await subscriber.SubscribeAsync("test");
            channel.OnMessage(message =>
            {
                Console.WriteLine($"保证单线程发送时接收顺序的方式 received {channel} message: {message}");
            });

            Parallel.For(0, 1, async i =>
            {
                var pubMultiplexer = await Init();
                var sub = pubMultiplexer.GetSubscriber();
                for (int j = 0; j < 10; j++)
                {
                    var message = $"parallel {i} publish message {j}";
                    await sub.PublishAsync("test", message, CommandFlags.FireAndForget);
                    Console.WriteLine($"published {message}");
                }
            });

            Console.WriteLine($"{nameof(PubSubLab)} run finished");
        }
    }
}
