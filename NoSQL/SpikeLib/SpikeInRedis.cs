using System;
using System.Threading.Tasks;

namespace SpikeLib
{
    public class SpikeInRedis : ISpike
    {
        public Task<SpikeOperateType> Decrease(int userId)
        {
            throw new NotImplementedException();
        }

        public Task InitStock(int stock)
        {
            throw new NotImplementedException();
        }
    }
}
