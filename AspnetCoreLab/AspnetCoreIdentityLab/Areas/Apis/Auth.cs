using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspnetCoreIdentityLab.Areas.Apis
{
    /// <summary>
    /// 认证授权测试
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class Auth : ControllerBase
    {
        /// <summary>
        /// TestAuthorize
        /// </summary>
        //[Authorize]
        [Route(nameof(TestAuthentication))]
        [HttpGet]
        public string TestAuthentication()
        {
            var c = base.HttpContext.User;
            return "Success";
        }
    }
}
