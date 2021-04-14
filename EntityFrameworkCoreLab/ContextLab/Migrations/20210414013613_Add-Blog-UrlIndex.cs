using Microsoft.EntityFrameworkCore.Migrations;

namespace ContextLab.Migrations
{
    public partial class AddBlogUrlIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "Index_BlogUrl",
                table: "Blogs",
                column: "Url",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "Index_BlogUrl",
                table: "Blogs");
        }
    }
}
