using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    }
}
