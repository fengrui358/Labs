using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BaiduAi.Demo.APIs;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BaiduAi.Demo
{
    public class Baidu : BackgroundService
    {
        private readonly ILogger<Baidu> _logger;
        private readonly Orc _orc;

        public Baidu(ILogger<Baidu> logger, Orc orc)
        {
            _logger = logger;
            _orc = orc;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            

            //while (!stoppingToken.IsCancellationRequested)
            //{
            //    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            //    await Task.Delay(1000, stoppingToken);
            //}
        }
    }
}
