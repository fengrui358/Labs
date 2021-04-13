using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContextLab.Entities.EntityTypeConfiguration
{
    public class PostEntityTypeConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            //EFCore p197
            builder.HasOne(s => s.Blog).WithMany(s => s.Posts).HasForeignKey(s => s.BlogId).HasConstraintName("ConstraintForeignKeyForBlogId");
            builder.HasData(new List<Post>
            {
                new() {BlogId = 1, PostId = 1, Title = "Post1", Content = "PostC1"},
                new() {BlogId = 1, PostId = 2, Title = "Post2", Content = "PostC2"},
                new() {BlogId = 2, PostId = 3, Title = "Post3", Content = "PostC3"}
            });

            //EFCore p207
            builder.HasMany(s => s.Tags).WithMany(s => s.Posts).UsingEntity<PostTag>(
                s => s.HasOne(t => t.Tag).WithMany(t => t.PostTags).HasForeignKey(t => t.TagId),
                s => s.HasOne(t => t.Post).WithMany(t => t.PostTags).HasForeignKey(t => t.PostId), s =>
                {
                    s.Property(t => t.PublicationDate).ValueGeneratedOnAddOrUpdate();
                    s.HasKey(t => new {t.PostId, t.TagId});
                });
        }
    }
}
