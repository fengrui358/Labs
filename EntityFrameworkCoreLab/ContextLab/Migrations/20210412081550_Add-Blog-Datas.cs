using Microsoft.EntityFrameworkCore.Migrations;

namespace ContextLab.Migrations
{
    public partial class AddBlogDatas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Blogs",
                columns: new[] { "BlogId", "AuthorId", "Url" },
                values: new object[] { 1, 1L, "test1" });

            migrationBuilder.InsertData(
                table: "Blogs",
                columns: new[] { "BlogId", "AuthorId", "Url" },
                values: new object[] { 2, 1L, "test2" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "BlogId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "BlogId",
                keyValue: 2);
        }
    }
}
