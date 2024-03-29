﻿using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

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
        // The following configures EF to create a Sqlite database file as `C:\blogging.db`.
        // For Mac or Linux, change this to `/tmp/blogging.db` or any other absolute path.

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite(@"Data Source=D:\blogging.db");
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

        public ICollection<Tag> Tags { get; set; }
    }

    public class Tag
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}
