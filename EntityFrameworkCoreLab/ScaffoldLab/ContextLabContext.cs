using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ScaffoldLab
{
    public partial class ContextLabContext : DbContext
    {
        public ContextLabContext()
        {
        }

        public ContextLabContext(DbContextOptions<ContextLabContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Blog> Blogs { get; set; }
        public virtual DbSet<Blogauthortable> Blogauthortables { get; set; }
        public virtual DbSet<Blogimage> Blogimages { get; set; }
        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<Carhistory> Carhistories { get; set; }
        public virtual DbSet<Efmigrationshistory> Efmigrationshistories { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OtherEntity> OtherEntities { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Posttag> Posttags { get; set; }
        public virtual DbSet<Posttag2> Posttag2s { get; set; }
        public virtual DbSet<Rider> Riders { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<Tag2> Tag2s { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;database=ContextLab;uid=root;pwd=action98", x => x.ServerVersion("8.0.23-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>(entity =>
            {
                entity.ToTable("blogs");

                entity.HasIndex(e => e.AuthorId, "IX_Blogs_AuthorId");

                entity.HasIndex(e => e.Url, "Index_BlogUrl")
                    .IsUnique();

                entity.Property(e => e.BackField)
                    .HasColumnType("longtext")
                    .HasColumnName("_backField")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.BlogType).HasComment("BlogType: 0-Blog; 1-RssBlog");

                entity.Property(e => e.Finances)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.PriceDecimal).HasPrecision(5, 2);

                entity.Property(e => e.RssUrl)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Blogs)
                    .HasForeignKey(d => d.AuthorId)
                    .HasConstraintName("FK_Blogs_BlogAuthorTable_AuthorId");
            });

            modelBuilder.Entity<Blogauthortable>(entity =>
            {
                entity.ToTable("blogauthortable");

                entity.HasComment("作者");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("longtext")
                    .HasComment("名字")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Title)
                    .HasColumnType("varchar(30)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<Blogimage>(entity =>
            {
                entity.ToTable("blogimage");

                entity.HasIndex(e => e.BlogId, "IX_BlogImage_BlogId")
                    .IsUnique();

                entity.Property(e => e.Caption)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.Blog)
                    .WithOne(p => p.Blogimage)
                    .HasForeignKey<Blogimage>(d => d.BlogId)
                    .HasConstraintName("FK_BlogImage_Blogs_BlogId");
            });

            modelBuilder.Entity<Car>(entity =>
            {
                entity.ToTable("cars");

                entity.HasIndex(e => e.LicensePlate, "AK_Cars_LicensePlate")
                    .IsUnique();

                entity.Property(e => e.LicensePlate)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Make)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Model)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<Carhistory>(entity =>
            {
                entity.HasKey(e => e.RecordOfSaleId)
                    .HasName("PRIMARY");

                entity.ToTable("carhistories");

                entity.HasIndex(e => e.CarLicensePlate, "IX_CarHistories_CarLicensePlate");

                entity.Property(e => e.CarLicensePlate)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.DateSold).HasMaxLength(6);

                entity.HasOne(d => d.CarLicensePlateNavigation)
                    .WithMany(p => p.Carhistories)
                    .HasPrincipalKey(p => p.LicensePlate)
                    .HasForeignKey(d => d.CarLicensePlate)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_CarHistories_Cars_CarLicensePlate");
            });

            modelBuilder.Entity<Efmigrationshistory>(entity =>
            {
                entity.HasKey(e => e.MigrationId)
                    .HasName("PRIMARY");

                entity.ToTable("__efmigrationshistory");

                entity.Property(e => e.MigrationId)
                    .HasColumnType("varchar(95)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.ProductVersion)
                    .IsRequired()
                    .HasColumnType("varchar(32)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("orders");

                entity.Property(e => e.BillingAddress)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Name)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.ShippingAddress)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.ShippingAddressCity)
                    .IsRequired()
                    .HasColumnType("longtext")
                    .HasColumnName("ShippingAddress_City")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Street)
                    .IsRequired()
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Version)
                    .HasColumnType("timestamp(6)")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
            });

            modelBuilder.Entity<OtherEntity>(entity =>
            {
                entity.ToTable("other-entity");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.ChangeNewColumnName)
                    .IsRequired()
                    .HasColumnType("varchar(200)")
                    .HasColumnName("change_new_column_name")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.LastUpdated)
                    .HasMaxLength(6)
                    .ValueGeneratedOnAddOrUpdate()
                    .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

                entity.Property(e => e.LastUpdatedDateTimeOffset)
                    .HasMaxLength(6)
                    .ValueGeneratedOnAddOrUpdate()
                    .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("post");

                entity.HasIndex(e => e.BlogId, "IX_Post_BlogId");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Url)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.Blog)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.BlogId)
                    .HasConstraintName("ConstraintForeignKeyForBlogId");
            });

            modelBuilder.Entity<Posttag>(entity =>
            {
                entity.HasKey(e => new { e.PostId, e.TagId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("posttag");

                entity.HasIndex(e => e.TagId, "IX_PostTag_TagId");

                entity.Property(e => e.TagId)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.PublicationDate)
                    .HasMaxLength(6)
                    .ValueGeneratedOnAddOrUpdate()
                    .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Posttags)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("FK_PostTag_Post_PostId");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.Posttags)
                    .HasForeignKey(d => d.TagId)
                    .HasConstraintName("FK_PostTag_Tag_TagId");
            });

            modelBuilder.Entity<Posttag2>(entity =>
            {
                entity.HasKey(e => new { e.PostId, e.TagId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("posttag2");

                entity.HasIndex(e => e.TagId, "IX_PostTag2_TagId");

                entity.Property(e => e.TagId)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.PublicationDate)
                    .HasMaxLength(6)
                    .ValueGeneratedOnAddOrUpdate()
                    .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Posttag2s)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("FK_PostTag2_Post_PostId");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.Posttag2s)
                    .HasForeignKey(d => d.TagId)
                    .HasConstraintName("FK_PostTag2_Tag2_TagId");
            });

            modelBuilder.Entity<Rider>(entity =>
            {
                entity.ToTable("rider");

                entity.Property(e => e.Mount)
                    .IsRequired()
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.ToTable("tag");

                entity.Property(e => e.TagId)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<Tag2>(entity =>
            {
                entity.ToTable("tag2");

                entity.HasIndex(e => e.PostId, "IX_Tag2_PostId");

                entity.Property(e => e.Id)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Tag2s)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("FK_Tag2_Post_PostId");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
