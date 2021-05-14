using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AspnetCoreWebApiLab.Controllers
{
    /// <summary>
    /// 日志控制器
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class LoggerController
    {
        private readonly ILogger _logger;

        public LoggerController(ILogger<LoggerController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="log">日志信息</param>
        /// <param name="logLevel">日志等级</param>
        /// <returns></returns>
        [HttpPost]
        public string Log(string log, LogLevel logLevel = LogLevel.Information)
        {
            if (_logger.IsEnabled(logLevel))
            {
                _logger.Log(logLevel, log);
                return "已记录";
            }
            else
            {
                return "日志等级不符合，未记录";
            }
        }
    }
}
