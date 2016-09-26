using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleSocketClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("启动客户端");

            var ip = ConfigurationManager.AppSettings["RemoteIp"];
            var port = int.Parse(ConfigurationManager.AppSettings["RemotePort"]);

            var localPort = int.Parse(ConfigurationManager.AppSettings["Port"]);

            Console.WriteLine($"服务器地址：{ip}:{port}");

            var ipAddress = IPAddress.Parse(ip);
            var client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            client.Bind(new IPEndPoint(IPAddress.Any, localPort));

            EndPoint remoteEndPoint = new IPEndPoint(ipAddress, port);
            try
            {
                client.Connect(remoteEndPoint);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine("连接服务失败！");
            }

            var receiveResult = new byte[1024];
            var receiveNumber = client.ReceiveFrom(receiveResult, ref remoteEndPoint);            

            Console.WriteLine($"接收到服务端消息：{Encoding.UTF8.GetString(receiveResult, 0, receiveNumber)}");

            while (true)
            {
                Thread.Sleep(1000);

                var sendMsg = $"{Guid.NewGuid()}，时间：{DateTime.Now}";
                client.Send(Encoding.UTF8.GetBytes(sendMsg));

                Console.WriteLine($"向服务端发送消息：{sendMsg}");
            }
        }
    }
}
