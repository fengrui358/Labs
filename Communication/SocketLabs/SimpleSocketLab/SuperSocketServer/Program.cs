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
        private static TcpServer _server;

        static void Main(string[] args)
        {
            Console.WriteLine("Start Service");

            var port = int.Parse(ConfigurationManager.AppSettings["Port"]);

            Console.WriteLine($"Address:{IPAddress.Any}:{port}");

            _server = new TcpServer();
            _server.Setup(port);

            _server.NewRequestReceived += ServerOnNewRequestReceived;

            _server.Start();

            Console.ReadKey();
        }

        private static void ServerOnNewRequestReceived(TcpSession session, BinaryRequestInfo requestInfo)
        {
            Console.WriteLine($"当前客户端总数{_server.GetAllSessions().Count()}，收到来时客户端{session.SessionID}的消息：{Encoding.UTF8.GetString(requestInfo.Body)}");
        }
    }
}
