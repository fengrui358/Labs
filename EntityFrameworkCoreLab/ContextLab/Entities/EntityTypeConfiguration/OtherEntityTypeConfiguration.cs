using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContextLab.Entities.EntityTypeConfiguration
{
    public class OtherEntityTypeConfiguration : IEntityTypeConfiguration<Other>
    {
        public void Configure(EntityTypeBuilder<Other> builder)
        {
            var guid = Guid.Parse("1379c923-3595-468c-8b41-ecc3d8d6f455");
            builder.HasData(new Other
            {
                Id = guid,
                TestString = guid.ToString("N"),
                LastUpdatedDateTimeOffset = new DateTimeOffset(2021,4,12,17,28,35, TimeSpan.FromHours(8)),
                LastUpdated = new DateTime(2021, 4, 12, 17, 28, 35)
            });
        }
    }
}
