using Microsoft.AspNetCore.Mvc;

namespace BadWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class XssController : ControllerBase
    {
        [HttpGet(nameof(WriteCookie))]
        public void WriteCookie(string? cookie = null)
        {
            if (cookie != null)
            {
                Console.WriteLine($"获取 cookie：{cookie}");
            }
            else
            {
                Console.WriteLine("获取 cookie 为空");
            }
        }
    }
}
