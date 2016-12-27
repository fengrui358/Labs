using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperSocket.ProtoBase;

namespace SuperSocketClientWithCommand
{
    class ReceiveFilter : IReceiveFilter<CalcResultPackageInfo>
    {
        public void Reset()
        {
            State = FilterState.Normal;
        }

        CalcResultPackageInfo IReceiveFilter<CalcResultPackageInfo>.Filter(BufferList data, out int rest)
        {
            //这个参数需要注意，当设为0时则没有后续内容，可以退出了，这里只作为测试
            rest = 0;
            var contents = new ArraySegment<byte>(data.Last.ToArray(), 0, data.Last.Count);

            return new CalcResultPackageInfo("From Server", new List<ArraySegment<byte>>() { contents });
        }

        public FilterState State { get; protected set; }

        IReceiveFilter<CalcResultPackageInfo> IReceiveFilter<CalcResultPackageInfo>.NextReceiveFilter { get; }
    }
}
