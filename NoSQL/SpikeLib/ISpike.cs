using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpikeLib
{
    public interface ISpike
    {
        public Task InitStock(int stock);

        public Task<SpikeOperateType> Decrease(int userId);
    }
}
