using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContextLab.Entities.EntityTypeConfiguration
{
    public class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.Property(s => s.Status).HasColumnName(nameof(Order.Status));
            builder.OwnsOne(s => s.ShippingAddress, navigationBuilder =>
            {
                navigationBuilder.Property(s => s.Street).HasColumnName(nameof(ShippingAddress.Street)).IsRequired();
                navigationBuilder.Property(s => s.City).IsRequired();
            });
            builder.Navigation(s => s.ShippingAddress).IsRequired();
            builder.HasOne(s => s.DetailedOrder).WithOne().HasForeignKey<DetailedOrder>(s => s.Id);

            //EFCore P252
            builder.Property<byte[]>(nameof(DetailedOrder.Version)).IsRowVersion().HasColumnName(nameof(DetailedOrder.Version));
        }
    }

    public class DetailedOrderEntityTypeConfiguration : IEntityTypeConfiguration<DetailedOrder>
    {
        public void Configure(EntityTypeBuilder<DetailedOrder> builder)
        {
            builder.ToTable("Orders");
            builder.Property(s => s.Status).HasColumnName(nameof(Order.Status));

            //EFCore P252
            builder.Property(s => s.Version).IsRowVersion().HasColumnName(nameof(DetailedOrder.Version));
        }
    }
}
