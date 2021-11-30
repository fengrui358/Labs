using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpikeLib
{
    public enum SpikeOperateType
    {
        /// <summary>
        /// 成功
        /// </summary>
        Success,

        /// <summary>
        /// 重复下单
        /// </summary>
        Repeat,

        /// <summary>
        /// 库存不足
        /// </summary>
        StockLimit
    }
}
