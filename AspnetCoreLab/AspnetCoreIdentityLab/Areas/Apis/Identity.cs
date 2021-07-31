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

        /// <summary>
        /// Construct
        /// </summary>
        /// <param name="userManager"></param>
        public Identity(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// 新增用户测试
        /// </summary>
        /// <param name="userRequest">用户信息</param>
        [Route(nameof(AddUserTest))]
        [HttpPost]
        public async Task<IActionResult> AddUserTest(CreateUserRequest userRequest)
        {
            var user = new ApplicationUser
            {
                UserName = userRequest.UserName,
            };
            var identityResult = await _userManager.CreateAsync(user, userRequest.Password);
            return new JsonResult(identityResult);
        }
    }

    /// <summary>
    /// 创建用户请求
    /// </summary>
    public class CreateUserRequest
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
