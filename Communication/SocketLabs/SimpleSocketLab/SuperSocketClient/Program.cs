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
using SuperSocket.ClientEngine;
using SuperSocket.ProtoBase;

namespace SuperSocketClient
{
    class Program
    {
        private static int _sendInterval = 1000;
        private static EndPoint _remoteEndPoint;

        private static readonly ConcurrentDictionary<string, Tuple<int, EasyClient>> _allClients =
            new ConcurrentDictionary<string, Tuple<int, EasyClient>>();

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
                var all = _allClients.Values;

                foreach (var tuple in all)
                {
                    try
                    {
                        if (tuple.Item2.IsConnected)
                        {
                            var sendMsg = $"客户端：{tuple.Item1}，{Guid.NewGuid()}，时间：{DateTime.Now}";
                            Console.WriteLine(sendMsg);

                            var sendMsgBytes = Encoding.UTF8.GetBytes(sendMsg);
                            tuple.Item2.Send(sendMsgBytes);
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

        private static async void CreateSocket(int clientId, EndPoint remoteEndPoint)
        {
            await Task.Run(async () =>
            {
                try
                {
                    var client = new EasyClient();
                    client.Initialize(new ReceiveFilter(), ReceiveHandler);

                    var isConnected = await client.ConnectAsync(remoteEndPoint);
                    if (isConnected)
                    {
                        _allClients.TryAdd(client.LocalEndPoint.ToString(), new Tuple<int, EasyClient>(clientId, client));
                    }
                    else
                    {
                        Console.WriteLine($"客户端{clientId}链接服务失败。");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            });
        }

        private static void ReceiveHandler(BufferedPackageInfo packageInfo)
        {
            foreach (var arrayBytes in packageInfo.Data)
            {
                var bytes = arrayBytes.ToArray();
                Console.WriteLine(Encoding.UTF8.GetString(bytes));
            }
        }
    }
}
