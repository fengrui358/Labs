using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SuperSocket.ClientEngine;

namespace SuperSocketClientWithCommand
{
    //测试断线重连以及命令
    class Program
    {
        private static EndPoint _remoteEndPoint;

        static void Main(string[] args)
        {
            Console.WriteLine("Start Client");

            var ip = ConfigurationManager.AppSettings["RemoteIp"];
            var port = int.Parse(ConfigurationManager.AppSettings["RemotePort"]);

            var isIp = Regex.Match(ip,
                @"((?:(?:25[0-5]|2[0-4]\d|((1\d{2})|([1-9]?\d)))\.){3}(?:25[0-5]|2[0-4]\d|((1\d{2})|([1-9]?\d))))")
                .Success;
            if (!isIp)
            {
                var firstIpAddress = Dns.GetHostAddresses(ip).First();
                ip = firstIpAddress.ToString();
            }

            Console.WriteLine($"Service Addess:{ip}:{port}");

            var ipAddress = IPAddress.Parse(ip);

            _remoteEndPoint = new IPEndPoint(ipAddress, port);

            var client = new EasyClient();

            // Initialize the client with the receive filter and request handler
            client.Initialize(new ReceiveFilter(), (request) =>
            {
                // handle the received request
                Console.WriteLine(request.Data);
            });

            client.ConnectAsync(_remoteEndPoint).Wait();
            client.Send(Encoding.UTF8.GetBytes("ADD 45 78"));

            Console.Read();
        }
    }
}
