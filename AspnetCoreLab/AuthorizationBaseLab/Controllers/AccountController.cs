using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AuthorizationBaseLab.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        [HttpGet(nameof(Login))]
        public Task<string> Login()
        {
            return Task.FromResult("请先登录");
        }

        /// <summary>
        /// Cookie login
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="returnUrl">重定向地址</param>
        /// <returns></returns>
        [HttpGet(nameof(CookieLogin))]
        public async Task<IActionResult> CookieLogin([FromServices]IAntiforgery antiforgery, string userName, string? returnUrl = null)
        {
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim("Name", userName));
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

            var tokens = antiforgery.GetAndStoreTokens(HttpContext);
            // 向客户端发送名称为 XSRF-TOKEN 的 Cookie ， 客户端必须将这个 Cookie 的值
            // 以 X-XSRF-TOKEN 为名称的 Header 再发送回服务端， 才能完成 XSRF 认证。
            Response.Cookies.Append(
                "XSRF-TOKEN",
                tokens.RequestToken,
                new CookieOptions
                {
                    HttpOnly = false,
                    Path = "/",
                    IsEssential = true,
                    SameSite = SameSiteMode.Lax
                }
            );
            return Content("login success");
        }

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost(nameof(Login))]
        public IActionResult Login([FromForm] string userName, [FromForm] string password, string? returnUrl = null)
        {
            var s = $"正式登陆界面获取用户名 {userName}，密码 {password}，returnUrl {returnUrl}";
            Console.WriteLine(s);

            if (!string.IsNullOrEmpty(returnUrl))
            {
                // 直接重定向，会导致开放重定向攻击
                return Redirect(returnUrl);
            }

            return Content(s);
        }

        /// <summary>
        /// JwtLogin
        /// </summary>
        /// <param name="securityKey"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpGet(nameof(JwtLogin))]
        public async Task<IActionResult> JwtLogin([FromServices] SymmetricSecurityKey securityKey, string userName)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim("Name", userName));
            var creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(issuer: "localhost", audience: "localhost", claims: claims, notBefore: DateTime.Now,
                expires: DateTime.Now.AddSeconds(60), signingCredentials: creds);
            var t = new JwtSecurityTokenHandler().WriteToken(token);

            return Content(t);
        }

        ///// <summary>
        ///// Challenge
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet(nameof(Challenge))]
        //public override Task<string> Challenge()
        //{
        //    base.Challenge()
        //    return Task.FromResult("你没登录");
        //}
    }
}
