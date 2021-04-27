using System.Collections.Generic;
using System.Threading.Tasks;
using AspnetCoreWebApiLab.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspnetCoreWebApiLab.Controllers
{
    /// <summary>
    /// 待处置列表控制器
    /// </summary>
    [Route("api/[controller]")]
    public class TodoController : Controller
    {
        private readonly TodoContext _context;

        public TodoController(TodoContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 获取所有待办事项
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<TodoItem>> GetAll()
        {
            return await _context.TodoItems.ToListAsync();
        }

        /// <summary>
        /// 根据Id获取待办事项
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetTodo")]
        public async Task<TodoItem> GetById(long id)
        {
            var item = await _context.TodoItems.FirstOrDefaultAsync(s => s.Id == id);
            return item;
        }

        /// <summary>
        /// GetActionResultById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("result/{id}", Name = "GetActionResultById")]
        public async Task<IActionResult> GetActionResultById(long id)
        {
            var item = await _context.TodoItems.FirstOrDefaultAsync(s => s.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return new ObjectResult(item);
        }
    }
}
