using AuthenticateApiLab.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticateApiLab.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiKeyTestController : ControllerBase
    {
        [HttpGet("apiKeyAuthTest")]
        [Authorize(AuthenticationSchemes = ApiKeyAuthenticationDefaults.AuthenticationSchema)]
        public IActionResult ApiKeyAuthTest()
        {
            return Ok(User.Identity);
        }
    }
}
