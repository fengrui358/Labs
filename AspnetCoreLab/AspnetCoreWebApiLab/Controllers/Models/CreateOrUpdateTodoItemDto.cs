using System.ComponentModel.DataAnnotations;

namespace AspnetCoreWebApiLab.Controllers.Models
{
    /// <summary>
    /// 创建TodoItem
    /// </summary>
    public class CreateOrUpdateTodoItemDto
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        public string Name { get; set; }
    }
}
