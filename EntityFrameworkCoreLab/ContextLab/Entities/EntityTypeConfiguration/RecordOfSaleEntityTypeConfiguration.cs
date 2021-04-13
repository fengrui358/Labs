using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContextLab.Entities.EntityTypeConfiguration
{
    public class RecordOfSaleEntityTypeConfiguration : IEntityTypeConfiguration<RecordOfSale>
    {
        public void Configure(EntityTypeBuilder<RecordOfSale> builder)
        {
            builder.HasOne(s => s.Car).WithMany(s => s.SaleHistory).OnDelete(DeleteBehavior.Cascade).HasForeignKey(s => s.CarLicensePlate)
                .HasPrincipalKey(s => s.LicensePlate);
        }
    }
}
