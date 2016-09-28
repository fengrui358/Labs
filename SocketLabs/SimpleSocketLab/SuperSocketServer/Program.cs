using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;

namespace SuperSocketServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("服务启动");

            var port = int.Parse(ConfigurationManager.AppSettings["Port"]);

            Console.WriteLine($"地址：{IPAddress.Any}:{port}");

            var server = new TcpServer();
            server.Setup(port);

            server.NewRequestReceived += ServerOnNewRequestReceived;

            server.Start();

            Console.ReadKey();
        }

        private static void ServerOnNewRequestReceived(TcpSession session, BinaryRequestInfo requestInfo)
        {
            Console.WriteLine($"收到来时客户端{session.SessionID}的消息：{Encoding.UTF8.GetString(requestInfo.Body)}");
        }
    }
}
