using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using SuperSocket.SocketBase;

namespace SuperSocketServerWithCommand
{
    /// <summary>
    /// 使用命令的方式来接收请求
    /// </summary>
    class Program
    {
        private static AppServer _server;

        static void Main(string[] args)
        {
            Console.WriteLine("Press any key to start the server!");

            Console.ReadKey();
            Console.WriteLine();

            var port = int.Parse(ConfigurationManager.AppSettings["Port"]);

            Console.WriteLine($"Address:{IPAddress.Any}:{port}");

            var appServer = new AppServer();

            appServer.NewSessionConnected += session => session.Send("Hello " + session.SessionID);

            //Setup the appServer
            if (!appServer.Setup(port)) //Setup with listening port
            {
                Console.WriteLine("Failed to setup!");
                Console.ReadKey();
                return;
            }

            Console.WriteLine();

            //Try to start the appServer
            if (!appServer.Start())
            {
                Console.WriteLine("Failed to start!");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("The server started successfully, press key 'q' to stop it!");

            while (Console.ReadKey().KeyChar != 'q')
            {
                Console.WriteLine();
                continue;
            }

            //Stop the appServer
            appServer.Stop();

            Console.WriteLine("The server was stopped!");
            Console.ReadKey();
        }
    }
}
