using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AuthorizationBaseLab.Pages
{
    public class MockLoginModel : PageModel
    {
        public void OnGet()
        {
        }

        public Task<string> MockLogin([FromForm] string userName, [FromForm] string password)
        {
            var s = $"模拟登陆界面获取用户名 {userName}，密码 {password}";
            Console.WriteLine(s);

            return Task.FromResult(s);
        }
    }
}
