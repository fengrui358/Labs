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
            var s = $"ģ���½�����ȡ�û��� {userName}������ {password}";
            Console.WriteLine(s);

            return Task.FromResult(s);
        }
    }
}
