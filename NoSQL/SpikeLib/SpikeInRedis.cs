using StackExchange.Redis;
using System.Threading.Tasks;

namespace SpikeLib
{
    public class SpikeInRedis : ISpike
    {
        private ConnectionMultiplexer _connectionMultiplexer;
        private IDatabase _database;
        private bool _stockLimit;

        public Task<SpikeOperateType> Decrease(int userId)
        {
            if (_stockLimit)
            {
                return Task.FromResult(SpikeOperateType.StockLimit);
            }

            var userAdded = _database.SetAdd("user", userId);
            // 用户未购买才继续
            if (!userAdded)
            {
                return Task.FromResult(SpikeOperateType.Repeat);
            }

            // 检查库存不为 0
            var stock = _database.StringDecrement("stock", 1);
            if (stock < 0)
            {
                _stockLimit = true;
                _database.StringIncrement("stock", 1);
                _database.SetRemove("user", userId);
                return Task.FromResult(SpikeOperateType.StockLimit);
            }

            return Task.FromResult(SpikeOperateType.Success);
        }

        public Task InitStock(int stock)
        {
            _connectionMultiplexer = ConnectionMultiplexer.Connect("localhost:6379", (options) => options.Password = "abc123");
            _database = _connectionMultiplexer.GetDatabase();
            _database.StringSet("stock", stock);

            return Task.CompletedTask;
        }
    }
}
