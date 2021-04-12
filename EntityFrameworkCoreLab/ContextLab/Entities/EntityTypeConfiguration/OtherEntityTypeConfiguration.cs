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
                TestString = guid.ToString("N"),
                LastUpdatedDateTimeOffset = new DateTimeOffset(2021,4,12,17,28,35, TimeSpan.FromMilliseconds(256)),
                LastUpdated = new DateTime(2021, 4, 12, 17, 28, 35)
            });
        }
    }
}
