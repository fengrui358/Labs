using System;
using AspnetCoreWebApiLab.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AspnetCoreWebApiLab.EntityFramework
{
    /// <summary>
    /// 初始化数据
    /// </summary>
    public class SeedData
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="serviceProvider"></param>
        public static async void Initialize(IServiceProvider serviceProvider)
        {
            await using var context = new TodoContext(serviceProvider.GetRequiredService<DbContextOptions<TodoContext>>());

            if (!await context.TodoItems.AnyAsync())
            {
                await context.TodoItems.AddAsync(new TodoItem {Name = Guid.NewGuid().ToString()});
                await context.SaveChangesAsync();
            }
        }
    }
}
