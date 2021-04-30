using System;
using AspnetCoreWebApiLab.Interfaces;
using Microsoft.Extensions.Logging;

namespace AspnetCoreWebApiLab.Services
{
    public class MyService : IMyService
    {
        private readonly ILogger<MyService> _logger;

        public MyService(ILogger<MyService> logger)
        {
            _logger = logger;
        }

        public string WriteMessage(string message)
        {
            var s = $"MyDependency.WriteMessage Message: {message}";

            _logger.LogDebug(s);

            return s;
        }
    }
}
