using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace NLogLabs
{
    class Program
    {
        static void Main(string[] args)
        {
            var log = LogManager.GetCurrentClassLogger();

            log.Debug("邮件日志测试");

            Console.Read();
        }
    }
}
