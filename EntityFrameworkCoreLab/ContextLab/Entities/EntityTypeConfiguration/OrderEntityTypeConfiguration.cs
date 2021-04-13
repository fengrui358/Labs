using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContextLab.Entities.EntityTypeConfiguration
{
    public class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(s => s.ShippingAddress, navigationBuilder =>
            {
                navigationBuilder.Property(s => s.Street).HasColumnName(nameof(ShippingAddress.Street)).IsRequired();
                navigationBuilder.Property(s => s.City).IsRequired();
            });
            builder.Navigation(s => s.ShippingAddress).IsRequired();
        }
    }
}
