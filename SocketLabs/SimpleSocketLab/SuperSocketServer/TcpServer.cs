using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;

namespace SuperSocketServer
{
    internal class TcpServer : AppServer<TcpSession, BinaryRequestInfo>
    {
        public TcpServer()
            : base(new DefaultReceiveFilterFactory<TcpReceiveAllFilter, BinaryRequestInfo>())
        {
        }
    }
}
