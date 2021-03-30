using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EntityFrameworkCoreLab
{
    /// <summary>
    /// dotnet tool install --global dotnet-ef
    /// dotnet add package Microsoft.EntityFrameworkCore.Design
    /// dotnet ef migrations add InitialCreate
    /// dotnet ef database update
    /// </summary>
    public class BloggingContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<PostTag> PostTags { get; set; }
        // The following configures EF to create a Sqlite database file as `C:\blogging.db`.
        // For Mac or Linux, change this to `/tmp/blogging.db` or any other absolute path.

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite(@"Data Source=D:\blogging.db");
            options.LogTo(Console.WriteLine);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PostTag>().HasKey(t => new {t.PostId, t.TagId});

            modelBuilder.Entity<PostTag>().HasOne(p => p.Post).WithMany(p => p.PostTags).HasForeignKey(p => p.PostId);
            modelBuilder.Entity<PostTag>().HasOne(p => p.Tag).WithMany(p => p.PostTags).HasForeignKey(p => p.TagId);
        }
    }
    public class Blog
    {
        public int BlogId { get; set; }
        public string Url { get; set; }
        public List<Post> Posts { get; } = new List<Post>();
    }
    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int BlogId { get; set; }
        public Blog Blog { get; set; }

        public List<PostTag> PostTags { get; } = new List<PostTag>();

        public ICollection<Tag> Tags { get; }
    }

    public class Tag
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public List<PostTag> PostTags { get; } = new List<PostTag>();

        public ICollection<Post> Posts { get; }
    }

    /// <summary>
    /// 手动定义关联
    /// </summary>
    public class PostTag
    {
        public DateTime PublicationDate { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
        public int TagId { get; set; }
        public Tag Tag { get; set; }

    }
}
