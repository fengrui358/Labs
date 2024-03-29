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
    [Migration("20210412085136_Schema-Test")]
    partial class SchemaTest
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
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Title")
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30) CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("BlogAuthorTable");

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

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("BlogId");

                    b.HasIndex("AuthorId");

                    b.ToTable("Blogs");

                    b.HasData(
                        new
                        {
                            BlogId = 1,
                            AuthorId = 1L,
                            Url = "test1"
                        },
                        new
                        {
                            BlogId = 2,
                            AuthorId = 1L,
                            Url = "test2"
                        });
                });

            modelBuilder.Entity("ContextLab.Entities.Other", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("TestString")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("other-entity");

                    b.HasData(
                        new
                        {
                            Id = new Guid("de7f49b9-20c7-4898-a2fd-8b408af30613"),
                            TestString = "de7f49b920c74898a2fd8b408af30613"
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

            modelBuilder.Entity("ContextLab.Entities.Author", b =>
                {
                    b.Navigation("Blogs");
                });
#pragma warning restore 612, 618
        }
    }
}
