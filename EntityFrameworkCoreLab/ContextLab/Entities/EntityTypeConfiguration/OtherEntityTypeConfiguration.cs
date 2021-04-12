using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContextLab.Entities.EntityTypeConfiguration
{
    public class OtherEntityTypeConfiguration : IEntityTypeConfiguration<Other>
    {
        public void Configure(EntityTypeBuilder<Other> builder)
        {
            var guid = Guid.NewGuid();
            builder.HasData(new Other
            {
                Id = guid,
                TestString = guid.ToString("N")
            });
        }
    }
}
