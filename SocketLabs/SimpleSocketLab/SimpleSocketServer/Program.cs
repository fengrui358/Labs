using System;
using System.Configuration;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace SimpleSocketServer
{
    class Program
    {
        private static Socket _serverSocket;

        static void Main(string[] args)
        {
            Console.WriteLine("启动服务");

            var port = int.Parse(ConfigurationManager.AppSettings["Port"]);

            Console.WriteLine($"地址：{IPAddress.Any}:{port}");

            _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _serverSocket.Bind(new IPEndPoint(IPAddress.Any, port));
            _serverSocket.Listen(10);

            var t = new Thread(ListenClientConnect);
            t.Start();

            Console.ReadKey();
        }

        private static void ListenClientConnect()
        {
            while(true)
            {
                var socketClient = _serverSocket.Accept();
                socketClient.Send(Encoding.UTF8.GetBytes("Hello World"));

                var t = new Thread(ReceiveMessage);
                t.Start(socketClient);
            }
        }

        private static void ReceiveMessage(object socketClient)
        {
            var client = (Socket) socketClient;
            var receiveReult = new byte[1024];
            while (true)
            {
                try
                {
                    int receiveNumber = client.Receive(receiveReult);
                    Console.WriteLine(
                        $"接受到客户端{client.RemoteEndPoint}的消息：{Encoding.UTF8.GetString(receiveReult, 0, receiveNumber)}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    client.Shutdown(SocketShutdown.Both);
                    client.Close();
                    break;
                }
            }
        }
    }
}
