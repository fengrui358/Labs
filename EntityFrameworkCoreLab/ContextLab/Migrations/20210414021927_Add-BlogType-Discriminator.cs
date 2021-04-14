using Microsoft.EntityFrameworkCore.Migrations;

namespace ContextLab.Migrations
{
    public partial class AddBlogTypeDiscriminator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Post",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BlogType",
                table: "Blogs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "RssUrl",
                table: "Blogs",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Blogs",
                columns: new[] { "BlogId", "AuthorId", "BlogType", "Price", "PriceDecimal", "RssUrl", "Url" },
                values: new object[] { 3, 1L, 1, 0.0, 0m, "RssUrl1", "RssBlog1" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "BlogId",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "Url",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "BlogType",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "RssUrl",
                table: "Blogs");
        }
    }
}
