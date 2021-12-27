using Microsoft.AspNetCore.Mvc;

namespace AuthorizationBaseLab.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        [HttpGet(nameof(Login))]
        public void Login()
        {

        }
    }
}
