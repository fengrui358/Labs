using Microsoft.AspNetCore.Mvc;

namespace AuthenticateApiLab.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet("test")]
        public string Test()
        {
            return "hello";
        }
    }
}
