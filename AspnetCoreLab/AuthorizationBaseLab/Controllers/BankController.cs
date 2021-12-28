using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthorizationBaseLab.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class BankController : ControllerBase
    {
        private readonly ILogger _logger;

        public BankController(ILogger<BankController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 查询银行信息
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet(nameof(QueryBankInfo))]
        public Task<string> QueryBankInfo()
        {
            return Task.FromResult("工商银行");
        }

        [Authorize]
        [HttpGet(nameof(WhoAreYouWithCookie))]
        public Task<string?> WhoAreYouWithCookie()
        {
            var userName = User.FindFirst("Name");
            return Task.FromResult(userName?.Value);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet(nameof(WhoAreYouWithJwt))]
        public Task<string?> WhoAreYouWithJwt()
        {
            var userName = User.FindFirst("Name");
            return Task.FromResult(userName?.Value);
        }

        [Authorize(AuthenticationSchemes = $"{JwtBearerDefaults.AuthenticationScheme},{CookieAuthenticationDefaults.AuthenticationScheme}")]
        [HttpGet(nameof(WhoAreYou))]
        public Task<string?> WhoAreYou()
        {
            var userName = User.FindFirst("Name");
            return Task.FromResult(userName?.Value);
        }

        /// <summary>
        /// 用于演示跨站请求伪造
        /// </summary>
        /// <param name="toUserName"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost(nameof(Transfer))]
        [ValidateAntiForgeryToken] // 如果不加入 XSRF Token 的校验，那么跨站请求伪造就能成功
        public Task<string> Transfer([FromForm]string toUserName, [FromForm]double amount)
        {
            var userName = User.FindFirst("Name");
            var msg = $"{userName?.Value} 转账 {amount} 给 {toUserName}";
            _logger.LogInformation(msg);

            return Task.FromResult(msg);
        }
    }
}
