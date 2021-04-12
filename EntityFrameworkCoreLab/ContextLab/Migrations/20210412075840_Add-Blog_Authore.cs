using Microsoft.EntityFrameworkCore.Migrations;

namespace ContextLab.Migrations
{
    public partial class AddBlog_Authore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AuthorId",
                table: "Blogs",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_AuthorId",
                table: "Blogs",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_BlogAuthorTable_AuthorId",
                table: "Blogs",
                column: "AuthorId",
                principalTable: "BlogAuthorTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_BlogAuthorTable_AuthorId",
                table: "Blogs");

            migrationBuilder.DropIndex(
                name: "IX_Blogs_AuthorId",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Blogs");
        }
    }
}
