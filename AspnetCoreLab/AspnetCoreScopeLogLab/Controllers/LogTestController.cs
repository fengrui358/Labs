using Microsoft.AspNetCore.Mvc;

namespace AspnetCoreScopeLogLab.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LogTestController : ControllerBase
    {
        private static long _requestId;
        private readonly ILogger _logger;

        public LogTestController(ILogger<LogTestController> logger)
        {
            _logger = logger;
        }

        [HttpGet("log")]
        public async Task<long> Log()
        {
            var id = Interlocked.Increment(ref _requestId);
            using (_logger.BeginScope(id))
            {
                _logger.LogInformation("Enter action");
                await DoSomething();
                _logger.LogInformation("Level action");
            }

            return id;
        }

        private async Task DoSomething()
        {
            _logger.LogInformation("Start do something");
#pragma warning disable CS4014 // 由于此调用不会等待，因此在调用完成前将继续执行当前方法
            Task.Run(() =>
            {
                _logger.LogInformation("Async do something");
            });
#pragma warning restore CS4014 // 由于此调用不会等待，因此在调用完成前将继续执行当前方法
            await Task.Delay(200);
            _logger.LogInformation("Finish do something");
        }
    }
}
