using Microsoft.EntityFrameworkCore.Migrations;

namespace ContextLab.Migrations
{
    public partial class AuthorData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "BlogAuthorTable",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1L, "test" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BlogAuthorTable",
                keyColumn: "Id",
                keyValue: 1L);
        }
    }
}
