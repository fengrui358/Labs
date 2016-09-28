using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
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

        private static void ReceiveHandler(IPackageInfo packageInfo)
        {
            
        }
    }
}
