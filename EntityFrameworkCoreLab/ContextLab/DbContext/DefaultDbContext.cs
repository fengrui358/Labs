﻿using System.Collections.Generic;
using ContextLab.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ContextLab.DbContext
{
    public class DefaultDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        private readonly IConfiguration _configuration;

        public DbSet<Blog> Blogs { get; set; }

        public DefaultDbContext(DbContextOptions<DefaultDbContext> options, IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //放在 StartUp 中出现问题，不能 Migration
            optionsBuilder.UseMySql(_configuration.GetConnectionString("Default"), MySqlServerVersion.LatestSupportedServerVersion);
            optionsBuilder.EnableDetailedErrors();
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>(s => s.ToTable("BlogAuthorTable"));

            modelBuilder.Entity<Author>().HasData(new Author
            {
                Id = 1,
                Name = "test"
            });

            modelBuilder.Entity<Blog>().HasData(new List<Blog>
            {
                new Blog {BlogId = 1, Url = "test1", AuthorId = 1},
                new Blog {BlogId = 2, Url = "test2", AuthorId = 1}
            });
        }
    }
}
