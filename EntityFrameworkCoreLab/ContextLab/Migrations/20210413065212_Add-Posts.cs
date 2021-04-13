using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ContextLab.Migrations
{
    public partial class AddPosts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "other-entity",
                keyColumn: "Id",
                keyValue: new Guid("cf6f929a-0100-4aa5-80c1-f9cd8f8bea88"));

            migrationBuilder.CreateTable(
                name: "Post",
                columns: table => new
                {
                    PostId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", maxLength: 255, nullable: false),
                    Content = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", maxLength: 500000, nullable: false),
                    BlogId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.PostId);
                    table.ForeignKey(
                        name: "FK_Post_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blogs",
                        principalColumn: "BlogId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Post",
                columns: new[] { "PostId", "BlogId", "Content", "Title" },
                values: new object[,]
                {
                    { 1L, 1, "PostC1", "Post1" },
                    { 2L, 1, "PostC2", "Post2" },
                    { 3L, 2, "PostC3", "Post3" }
                });

            migrationBuilder.InsertData(
                table: "other-entity",
                columns: new[] { "Id", "change_new_column_name" },
                values: new object[] { new Guid("1379c923-3595-468c-8b41-ecc3d8d6f455"), "1379c9233595468c8b41ecc3d8d6f455" });

            migrationBuilder.CreateIndex(
                name: "IX_Post_BlogId",
                table: "Post",
                column: "BlogId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Post");

            migrationBuilder.DeleteData(
                table: "other-entity",
                keyColumn: "Id",
                keyValue: new Guid("1379c923-3595-468c-8b41-ecc3d8d6f455"));

            migrationBuilder.InsertData(
                table: "other-entity",
                columns: new[] { "Id", "change_new_column_name" },
                values: new object[] { new Guid("cf6f929a-0100-4aa5-80c1-f9cd8f8bea88"), "cf6f929a01004aa580c1f9cd8f8bea88" });
        }
    }
}
