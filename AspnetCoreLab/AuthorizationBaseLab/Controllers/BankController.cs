using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;

namespace AuthorizationBaseLab.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class BankController : ControllerBase
    {
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
    }
}
