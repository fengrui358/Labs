using Microsoft.AspNetCore.Mvc;

namespace BadWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost(nameof(MockLogin))]
        public IActionResult MockLogin([FromForm] string userName, [FromForm] string password)
        {
            var s = $"伪造登陆界面获取用户名 {userName}，密码 {password}";
            Console.WriteLine(s);

            return Content(s);
        }
    }
}
