using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ContextLab.Migrations
{
    public partial class AuthorComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "other-entity",
                keyColumn: "Id",
                keyValue: new Guid("de7f49b9-20c7-4898-a2fd-8b408af30613"));

            migrationBuilder.AlterTable(
                name: "BlogAuthorTable",
                comment: "作者");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "BlogAuthorTable",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: false,
                comment: "名字",
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4");

            migrationBuilder.InsertData(
                table: "other-entity",
                columns: new[] { "Id", "TestString" },
                values: new object[] { new Guid("46d68bbb-0865-4b78-bf7b-0476c77917de"), "46d68bbb08654b78bf7b0476c77917de" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "other-entity",
                keyColumn: "Id",
                keyValue: new Guid("46d68bbb-0865-4b78-bf7b-0476c77917de"));

            migrationBuilder.AlterTable(
                name: "BlogAuthorTable",
                oldComment: "作者");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "BlogAuthorTable",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldComment: "名字");

            migrationBuilder.InsertData(
                table: "other-entity",
                columns: new[] { "Id", "TestString" },
                values: new object[] { new Guid("de7f49b9-20c7-4898-a2fd-8b408af30613"), "de7f49b920c74898a2fd8b408af30613" });
        }
    }
}
