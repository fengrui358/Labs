using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSocketServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("服务启动");
            var server = new TelnetServer();
            server.Setup(4567);
            
            server.Start();

            Console.ReadKey();
        }
    }
}
