using Microsoft.EntityFrameworkCore.Migrations;

namespace ContextLab.Migrations
{
    public partial class AddForeign_Key : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Post_Blogs_BlogId",
                table: "Post");

            migrationBuilder.AddForeignKey(
                name: "ConstraintForeignKeyForBlogId",
                table: "Post",
                column: "BlogId",
                principalTable: "Blogs",
                principalColumn: "BlogId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "ConstraintForeignKeyForBlogId",
                table: "Post");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_Blogs_BlogId",
                table: "Post",
                column: "BlogId",
                principalTable: "Blogs",
                principalColumn: "BlogId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
