using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContextLab.Entities.EntityTypeConfiguration
{
    public class RssBlogEntityTypeConfiguration : IEntityTypeConfiguration<RssBlog>
    {
        public void Configure(EntityTypeBuilder<RssBlog> builder)
        {
            builder.HasData(new RssBlog {BlogId = 3, AuthorId = 1, Url = "RssBlog1", RssUrl = "RssUrl1"});
        }
    }
}
