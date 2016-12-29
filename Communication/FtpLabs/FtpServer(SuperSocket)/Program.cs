using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperSocket.Ftp.FtpService;
using SuperSocket.SocketBase;
using SuperSocket.SocketEngine;

namespace FtpLabs
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start the server!");

            var bootstrap = BootstrapFactory.CreateBootstrapFromConfigFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Config\Basic.config"));

            if (!bootstrap.Initialize())
            {
                Console.WriteLine("Failed to initialize!");
                Console.ReadKey();
                return;
            }

            var result = bootstrap.Start();

            Console.WriteLine("Start result: {0}!", result);

            if (result == StartResult.Failed)
            {
                Console.WriteLine("Failed to start!");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Press key 'q' to stop it!");

            while (Console.ReadKey().KeyChar != 'q')
            {
                Console.WriteLine();
                continue;
            }

            Console.WriteLine();

            //Stop the appServer
            bootstrap.Stop();
            Console.ReadKey();
        }
    }
}
