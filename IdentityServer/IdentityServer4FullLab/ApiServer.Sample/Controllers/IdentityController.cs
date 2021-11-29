using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiServer.Sample.Controllers
{
    /// <summary>
    /// IdentityController
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class IdentityController : ControllerBase
    {
        /// <summary>
        /// 获取 User Claims
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(User.Claims.Select(s => new { s.Type, s.Value }));
        }
    }
}
