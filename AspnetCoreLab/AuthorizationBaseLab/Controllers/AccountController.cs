using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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
        /// <returns></returns>
        [HttpGet(nameof(CookieLogin))]
        public async Task<IActionResult> CookieLogin(string userName)
        {
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim("Name", userName));
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
            return Content("login success");
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
