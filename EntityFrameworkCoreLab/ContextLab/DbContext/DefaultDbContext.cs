using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using ContextLab.Entities;
using ContextLab.Entities.Enums;
using ContextLab.Entities.Structs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;

namespace ContextLab.DbContext
{
    public class DefaultDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        private readonly IConfiguration _configuration;

        public DbSet<Blog> Blogs { get; set; }

        public DbSet<Other> Others { get; set; }

        public DbSet<Car> Cars { get; set; }

        public DbSet<RecordOfSale> CarHistories { get; set; }

        public DbSet<RssBlog> RssBlogs { get; set; }

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
            optionsBuilder.LogTo(Console.WriteLine);

            //EFCore p312 Lazy Loading
            optionsBuilder.UseLazyLoadingProxies();
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
                new() {BlogId = 1, Url = "test1", AuthorId = 1},
                new() {BlogId = 2, Url = "test2", AuthorId = 1, Price = 12.545443, PriceDecimal = 125.454M}
            });

            modelBuilder.Entity<Blog>(s =>
            {
                //EFCore p271 排除表的 Migration
                //s.ToTable("blog", blog => blog.ExcludeFromMigrations());

                s.Property(blog => blog.AuthorId).IsRequired();
                s.Property(blog => blog.Url).HasMaxLength(255).IsRequired();

                s.HasMany(blog => blog.Posts).WithOne(post => post.Blog);
                s.Navigation(blog => blog.Posts).UsePropertyAccessMode(PropertyAccessMode.Property);
                //EFCore p212
                s.HasIndex(blog => blog.Url).IsUnique().HasFilter(null);

                //EFCore p214
                s.HasDiscriminator(blog => blog.BlogType).HasValue<Blog>(BlogType.Blog).HasValue<RssBlog>(BlogType.RssBlog);

                //EFCore p220
                s.Property("_backField");

                //EFCore p231
                s.Property(blog => blog.Finances).HasConversion(f => JsonSerializer.Serialize(f, null),
                    f => JsonSerializer.Deserialize<List<AnnualFinance>>(f, null),
                    new ValueComparer<IList<AnnualFinance>>((c1, c2) => c1.SequenceEqual(c2),
                        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())), c => (IList<AnnualFinance>) c.ToList()));

                //EFCore p233
                //s.Property(blog => blog.Version).IsRowVersion().HasConversion<byte[]>();

            });

            //EFCore p209
            modelBuilder.Entity<PostTag2>(s =>
            {
                s.HasKey(t => new {t.PostId, t.TagId});
                s.HasOne(t => t.Post).WithMany(t => t.PostTags2).HasForeignKey(t => t.PostId);
                s.HasOne(t => t.Tag).WithMany(t => t.PostTags2).HasForeignKey(t => t.TagId);
            });

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DefaultDbContext).Assembly);
        }
    }
}
