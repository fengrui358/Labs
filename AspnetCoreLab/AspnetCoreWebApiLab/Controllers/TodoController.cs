using Microsoft.AspNetCore.Mvc;

namespace AspnetCoreWebApiLab.Controllers
{
    /// <summary>
    /// 待处置列表控制器
    /// </summary>
    [Route("api/[controler]")]
    public class TodoController : Controller
    {
        private readonly TodoContext _context;

        public TodoController(TodoContext context)
        {
            _context = context;
        }
    }
}
