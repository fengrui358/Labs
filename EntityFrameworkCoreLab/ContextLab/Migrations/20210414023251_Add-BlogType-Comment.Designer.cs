﻿// <auto-generated />
using System;
using ContextLab.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ContextLab.Migrations
{
    [DbContext(typeof(DefaultDbContext))]
    [Migration("20210414023251_Add-BlogType-Comment")]
    partial class AddBlogTypeComment
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.5");

            modelBuilder.Entity("ContextLab.Entities.Author", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4")
                        .HasComment("名字");

                    b.Property<string>("Title")
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30) CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("BlogAuthorTable");

                    b
                        .HasComment("作者");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Name = "test"
                        });
                });

            modelBuilder.Entity("ContextLab.Entities.Blog", b =>
                {
                    b.Property<int>("BlogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<long>("AuthorId")
                        .HasColumnType("bigint");

                    b.Property<int>("BlogType")
                        .HasColumnType("int")
                        .HasComment("BlogType: 0-Blog; 1-RssBlog");

                    b.Property<double>("Price")
                        .HasColumnType("double");

                    b.Property<decimal>("PriceDecimal")
                        .HasColumnType("decimal(5,2)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("BlogId");

                    b.HasIndex("AuthorId");

                    b.HasIndex("Url")
                        .IsUnique();

                    b.HasIndex(new[] { "Url" }, "Index_BlogUrl")
                        .IsUnique();

                    b.ToTable("Blogs");

                    b.HasDiscriminator<int>("BlogType").HasValue(0);

                    b.HasData(
                        new
                        {
                            BlogId = 1,
                            AuthorId = 1L,
                            BlogType = 0,
                            Price = 0.0,
                            PriceDecimal = 0m,
                            Url = "test1"
                        },
                        new
                        {
                            BlogId = 2,
                            AuthorId = 1L,
                            BlogType = 0,
                            Price = 12.545443000000001,
                            PriceDecimal = 125.454m,
                            Url = "test2"
                        });
                });

            modelBuilder.Entity("ContextLab.Entities.BlogImage", b =>
                {
                    b.Property<int>("BlogImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("BlogId")
                        .HasColumnType("int");

                    b.Property<string>("Caption")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<byte[]>("Image")
                        .HasColumnType("longblob");

                    b.HasKey("BlogImageId");

                    b.HasIndex("BlogId")
                        .IsUnique();

                    b.ToTable("BlogImage");
                });

            modelBuilder.Entity("ContextLab.Entities.Car", b =>
                {
                    b.Property<int>("CarId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("LicensePlate")
                        .IsRequired()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Make")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Model")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("CarId");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("ContextLab.Entities.Order", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("ContextLab.Entities.Other", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("LastUpdated")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    b.Property<DateTimeOffset>("LastUpdatedDateTimeOffset")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    b.Property<string>("TestString")
                        .IsRequired()
                        .HasColumnType("varchar(200)")
                        .HasColumnName("change_new_column_name");

                    b.HasKey("Id");

                    b.ToTable("other-entity");

                    b.HasData(
                        new
                        {
                            Id = new Guid("1379c923-3595-468c-8b41-ecc3d8d6f455"),
                            LastUpdated = new DateTime(2021, 4, 12, 17, 28, 35, 0, DateTimeKind.Unspecified),
                            LastUpdatedDateTimeOffset = new DateTimeOffset(new DateTime(2021, 4, 12, 17, 28, 35, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)),
                            TestString = "1379c9233595468c8b41ecc3d8d6f455"
                        });
                });

            modelBuilder.Entity("ContextLab.Entities.Post", b =>
                {
                    b.Property<long>("PostId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<int>("BlogId")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(500000)
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Url")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("PostId");

                    b.HasIndex("BlogId");

                    b.ToTable("Post");

                    b.HasData(
                        new
                        {
                            PostId = 1L,
                            BlogId = 1,
                            Content = "PostC1",
                            Title = "Post1"
                        },
                        new
                        {
                            PostId = 2L,
                            BlogId = 1,
                            Content = "PostC2",
                            Title = "Post2"
                        },
                        new
                        {
                            PostId = 3L,
                            BlogId = 2,
                            Content = "PostC3",
                            Title = "Post3"
                        });
                });

            modelBuilder.Entity("ContextLab.Entities.PostTag", b =>
                {
                    b.Property<long>("PostId")
                        .HasColumnType("bigint");

                    b.Property<string>("TagId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<DateTime>("PublicationDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    b.HasKey("PostId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("PostTag");
                });

            modelBuilder.Entity("ContextLab.Entities.PostTag2", b =>
                {
                    b.Property<long>("PostId")
                        .HasColumnType("bigint");

                    b.Property<string>("TagId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<DateTime>("PublicationDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    b.HasKey("PostId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("PostTag2");
                });

            modelBuilder.Entity("ContextLab.Entities.RecordOfSale", b =>
                {
                    b.Property<int>("RecordOfSaleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CarLicensePlate")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<DateTime>("DateSold")
                        .HasColumnType("datetime(6)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("RecordOfSaleId");

                    b.HasIndex("CarLicensePlate");

                    b.ToTable("CarHistories");
                });

            modelBuilder.Entity("ContextLab.Entities.Tag", b =>
                {
                    b.Property<string>("TagId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("TagId");

                    b.ToTable("Tag");
                });

            modelBuilder.Entity("ContextLab.Entities.Tag2", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<long?>("PostId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.ToTable("Tag2");
                });

            modelBuilder.Entity("ContextLab.Entities.RssBlog", b =>
                {
                    b.HasBaseType("ContextLab.Entities.Blog");

                    b.Property<string>("RssUrl")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasDiscriminator().HasValue(1);

                    b.HasData(
                        new
                        {
                            BlogId = 3,
                            AuthorId = 1L,
                            BlogType = 0,
                            Price = 0.0,
                            PriceDecimal = 0m,
                            Url = "RssBlog1",
                            RssUrl = "RssUrl1"
                        });
                });

            modelBuilder.Entity("ContextLab.Entities.Blog", b =>
                {
                    b.HasOne("ContextLab.Entities.Author", "Author")
                        .WithMany("Blogs")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("ContextLab.Entities.BlogImage", b =>
                {
                    b.HasOne("ContextLab.Entities.Blog", "Blog")
                        .WithOne("BlogImage")
                        .HasForeignKey("ContextLab.Entities.BlogImage", "BlogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Blog");
                });

            modelBuilder.Entity("ContextLab.Entities.Order", b =>
                {
                    b.OwnsOne("ContextLab.Entities.ShippingAddress", "ShippingAddress", b1 =>
                        {
                            b1.Property<long>("OrderId")
                                .HasColumnType("bigint");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("longtext CHARACTER SET utf8mb4");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasColumnType("longtext CHARACTER SET utf8mb4")
                                .HasColumnName("Street");

                            b1.HasKey("OrderId");

                            b1.ToTable("Order");

                            b1.WithOwner()
                                .HasForeignKey("OrderId");
                        });

                    b.Navigation("ShippingAddress")
                        .IsRequired();
                });

            modelBuilder.Entity("ContextLab.Entities.Post", b =>
                {
                    b.HasOne("ContextLab.Entities.Blog", "Blog")
                        .WithMany("Posts")
                        .HasForeignKey("BlogId")
                        .HasConstraintName("ConstraintForeignKeyForBlogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Blog");
                });

            modelBuilder.Entity("ContextLab.Entities.PostTag", b =>
                {
                    b.HasOne("ContextLab.Entities.Post", "Post")
                        .WithMany("PostTags")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ContextLab.Entities.Tag", "Tag")
                        .WithMany("PostTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("ContextLab.Entities.PostTag2", b =>
                {
                    b.HasOne("ContextLab.Entities.Post", "Post")
                        .WithMany("PostTags2")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ContextLab.Entities.Tag2", "Tag")
                        .WithMany("PostTags2")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("ContextLab.Entities.RecordOfSale", b =>
                {
                    b.HasOne("ContextLab.Entities.Car", "Car")
                        .WithMany("SaleHistory")
                        .HasForeignKey("CarLicensePlate")
                        .HasPrincipalKey("LicensePlate")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Car");
                });

            modelBuilder.Entity("ContextLab.Entities.Tag2", b =>
                {
                    b.HasOne("ContextLab.Entities.Post", null)
                        .WithMany("Tags2")
                        .HasForeignKey("PostId");
                });

            modelBuilder.Entity("ContextLab.Entities.Author", b =>
                {
                    b.Navigation("Blogs");
                });

            modelBuilder.Entity("ContextLab.Entities.Blog", b =>
                {
                    b.Navigation("BlogImage");

                    b.Navigation("Posts");
                });

            modelBuilder.Entity("ContextLab.Entities.Car", b =>
                {
                    b.Navigation("SaleHistory");
                });

            modelBuilder.Entity("ContextLab.Entities.Post", b =>
                {
                    b.Navigation("PostTags");

                    b.Navigation("PostTags2");

                    b.Navigation("Tags2");
                });

            modelBuilder.Entity("ContextLab.Entities.Tag", b =>
                {
                    b.Navigation("PostTags");
                });

            modelBuilder.Entity("ContextLab.Entities.Tag2", b =>
                {
                    b.Navigation("PostTags2");
                });
#pragma warning restore 612, 618
        }
    }
}
