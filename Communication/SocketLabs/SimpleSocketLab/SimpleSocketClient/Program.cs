using System;
using System.Collections.Concurrent;
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
        private static int _sendInterval = 1000;
        private static EndPoint _remoteEndPoint;

        private static readonly ConcurrentDictionary<string, Tuple<int, Socket>> _allSockets =
            new ConcurrentDictionary<string, Tuple<int, Socket>>();

        static void Main(string[] args)
        {
            Console.WriteLine("启动客户端");

            var ip = ConfigurationManager.AppSettings["RemoteIp"];
            var port = int.Parse(ConfigurationManager.AppSettings["RemotePort"]);

            Console.WriteLine($"服务器地址：{ip}:{port}");

            var ipAddress = IPAddress.Parse(ip);

            _remoteEndPoint = new IPEndPoint(ipAddress, port);

            Console.WriteLine("输入期望建立的链接数：");
            var num = int.Parse(Console.ReadLine());

            for (int i = 0; i < num; i++)
            {                
                CreateSocket(i + 1, _remoteEndPoint);
            }

            while (true)
            {
                var all = _allSockets.Values;

                foreach (var tuple in all)
                {
                    try
                    {
                        if (tuple.Item2.Connected)
                        {
                            var sendMsg = $"客户端：{tuple.Item1}，{Guid.NewGuid()}，时间：{DateTime.Now}";
                            Console.WriteLine(sendMsg);

                            var sendMsgBytes = Encoding.UTF8.GetBytes(sendMsg);
                            tuple.Item2.BeginSend(sendMsgBytes, 0, sendMsgBytes.Length, SocketFlags.None, SendCallback,
                                tuple.Item2);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }

                Thread.Sleep(_sendInterval);
            }
        }

        private static void CreateSocket(int clientId, EndPoint remoteEndPoint)
        {
            var client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                client.BeginConnect(remoteEndPoint, ConnectCallBack, new Tuple<Socket,int>(client, clientId));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine("连接服务失败！");
            }
        }

        private static void ReviceCallBack(IAsyncResult ar)
        {
            try
            {
                var tuple = (Tuple<Socket, byte[]>) ar.AsyncState;
                var client = tuple.Item1;

                var receiveNumber = client.EndReceiveFrom(ar, ref _remoteEndPoint);
                Console.WriteLine($"接收到服务端消息：{Encoding.UTF8.GetString(tuple.Item2, 0, receiveNumber)}");

                client.BeginReceiveFrom(tuple.Item2, 0, tuple.Item2.Length, SocketFlags.None, ref _remoteEndPoint,
                    ReviceCallBack, new Tuple<Socket, byte[]>(client, tuple.Item2));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private static void ConnectCallBack(IAsyncResult ar)
        {
            try
            {
                var tuple = (Tuple<Socket, int>) ar.AsyncState;
                var client = tuple.Item1;

                client.EndConnect(ar);

                var receiveResult = new byte[1024];
                client.BeginReceiveFrom(receiveResult, 0, receiveResult.Length, SocketFlags.None, ref _remoteEndPoint,
                    ReviceCallBack, new Tuple<Socket, byte[]>(client, receiveResult));

                _allSockets.TryAdd(client.LocalEndPoint.ToString(), new Tuple<int, Socket>(tuple.Item2, client));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine("连接服务失败！");
            }
        }

        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                var client = (Socket)ar.AsyncState;

                client.EndSend(ar);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
