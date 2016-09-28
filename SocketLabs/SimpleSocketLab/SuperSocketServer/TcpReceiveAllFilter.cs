using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperSocket.Common;
using SuperSocket.SocketBase.Protocol;

namespace SuperSocketServer
{
    internal class TcpReceiveAllFilter : NoFilter<BinaryRequestInfo>
    {
        /// <summary>
        /// 不需要过滤 直接转发
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        /// <param name="toBeCopied"></param>
        /// <returns></returns>
        protected override BinaryRequestInfo ProcessMatchedRequest(byte[] buffer, int offset, int length,
            bool toBeCopied)
        {
            return new BinaryRequestInfo(string.Empty, buffer.CloneRange(offset, length));
        }
    }
}
