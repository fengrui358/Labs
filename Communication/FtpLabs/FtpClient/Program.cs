using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.FtpClient;
using WindowsFormsApplication1;

namespace FtpClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //var ftpClient = new System.Net.FtpClient.FtpClient();
            //ftpClient.Host = "192.168.1.80";
            //ftpClient.Port = 21;

            //ftpClient.

            var ftpClient = new FTPClient();
            ftpClient.RemoteHost = "192.168.1.80";
            ftpClient.RemotePort = 21;

            ftpClient.Connect();
        }
    }
}
