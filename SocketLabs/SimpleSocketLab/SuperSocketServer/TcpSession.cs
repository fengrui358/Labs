﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;

namespace SuperSocketServer
{
    internal class TcpSession : AppSession<TcpSession, BinaryRequestInfo>
    {
        protected override void OnSessionStarted()
        {
            try
            {
                Console.WriteLine("Session启动");
                Send("Hello " + SessionID);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        protected override void HandleUnknownRequest(BinaryRequestInfo requestInfo)
        {

        }        

        protected override void HandleException(Exception e)
        {
            Console.WriteLine(e);
        }

        protected override void OnSessionClosed(CloseReason reason)
        {
            try
            {
                Console.WriteLine($"客户端Session{SessionID}断开连接，{reason}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }        
    }
}
