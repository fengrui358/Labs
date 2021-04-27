using AspnetCoreWebApiLab.Entities;
using Microsoft.EntityFrameworkCore;

namespace AspnetCoreWebApiLab
{
    public class TodoContext : DbContext
    {
        public DbSet<TodoItem> TodoItems { get; set; }

        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TodoContext).Assembly);
        }
    }
}
