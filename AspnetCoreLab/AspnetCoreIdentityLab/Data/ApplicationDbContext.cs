using System;
using AspnetCoreIdentityLab.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AspnetCoreIdentityLab.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>(entity =>
            {
                entity.Property(s => s.FamilyAddress).HasMaxLength(256);
            });

            base.OnModelCreating(builder);
        }
    }
}
