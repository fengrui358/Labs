using System.Threading.Tasks;
using AspnetCoreIdentityLab.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AspnetCoreIdentityLab.Areas.Apis
{
    /// <summary>
    /// Identity Test
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class Identity : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        /// <summary>
        /// Construct
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        /// <param name="applicationDbContext"></param>
        public Identity(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        /// 新增用户测试
        /// </summary>
        /// <param name="userRequest">用户信息</param>
        [Route(nameof(AddUserTest))]
        [HttpPost]
        public async Task<IActionResult> AddUserTest(UserRequest userRequest)
        {
            var user = new ApplicationUser
            {
                UserName = userRequest.UserName,
            };
            var identityResult = await _userManager.CreateAsync(user, userRequest.Password);
            return new JsonResult(identityResult);
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userRequest"></param>
        /// <returns></returns>
        [Route(nameof(Sign))]
        [HttpPost]
        public async Task<IActionResult> Sign(UserRequest userRequest)
        {
            var result =
                await _signInManager.PasswordSignInAsync(userRequest.UserName, userRequest.Password, true, false);
            return new JsonResult(result);
        }
    }

    /// <summary>
    /// 创建用户请求
    /// </summary>
    public class UserRequest
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
    }
}
