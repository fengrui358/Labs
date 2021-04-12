using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContextLab.Entities.EntityTypeConfiguration
{
    /// <summary>
    /// EFCore PDF: p169
    /// </summary>
    public class AuthorEntityTypeConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.Property(s => s.Name).IsRequired();
        }
    }
}
