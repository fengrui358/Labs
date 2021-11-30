using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SpikeLib
{
    public class SpikeInMemery : ISpike
    {
        private int _stock;
        private HashSet<int> _users = new HashSet<int>();
        private bool _stockLimit;

        public Task<SpikeOperateType> Decrease(int userId)
        {
            if (_stockLimit)
            {
                return Task.FromResult(SpikeOperateType.StockLimit);
            }

            // 用户未购买才继续
            if (!_users.Add(userId))
            {
                return Task.FromResult(SpikeOperateType.Repeat);
            }

            // 检查库存不为 0
            var stock = Interlocked.Decrement(ref _stock);
            if (stock < 0)
            {
                _stockLimit = true;
                Interlocked.Increment(ref _stock);
                _users.Remove(userId);
                return Task.FromResult(SpikeOperateType.StockLimit);
            }

            return Task.FromResult(SpikeOperateType.Success);
        }

        public Task InitStock(int stock)
        {
            Interlocked.Exchange(ref _stock, stock);
            return Task.CompletedTask;
        }
    }
}
