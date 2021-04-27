using System.Collections.Generic;
using System.Threading.Tasks;
using AspnetCoreWebApiLab.Controllers.Models;
using AspnetCoreWebApiLab.Entities;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public TodoController(TodoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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

        /// <summary>
        /// 添加待办项
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrUpdateTodoItemDto item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            var todoItem = _mapper.Map<CreateOrUpdateTodoItemDto, TodoItem>(item);

            await _context.TodoItems.AddAsync(todoItem);
            await _context.SaveChangesAsync();

            return CreatedAtRoute(nameof(GetActionResultById), new {id = todoItem.Id}, todoItem);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] CreateOrUpdateTodoItemDto item)
        {
            if (item == null || id == 0)
            {
                return BadRequest();
            }

            var todoItem = await _context.TodoItems.FirstOrDefaultAsync(s => s.Id == id);
            if (todoItem == null)
            {
                return NotFound();
            }

            _mapper.Map(item, todoItem);

            _context.TodoItems.Update(todoItem);
            await _context.SaveChangesAsync();

            return new NoContentResult();
        }
    }
}
