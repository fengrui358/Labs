using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContextLab.Entities.EntityTypeConfiguration
{
    public class RiderEntityTypeConfiguration : IEntityTypeConfiguration<Rider>
    {
        public void Configure(EntityTypeBuilder<Rider> builder)
        {
            builder.Property(s => s.Mount).HasConversion(s => s.ToString(), s => (EquineBeast) Enum.Parse(typeof(EquineBeast), s));
            builder.HasData(new Rider {Id = 1, Mount = EquineBeast.Mule});
        }
    }
}
