using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ContextLab.Migrations
{
    public partial class AddBlogPriceData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "other-entity",
                keyColumn: "Id",
                keyValue: new Guid("7d56ad6e-8d94-451e-be27-3e85485243c8"));

            migrationBuilder.UpdateData(
                table: "Blogs",
                keyColumn: "BlogId",
                keyValue: 2,
                columns: new[] { "Price", "PriceDecimal" },
                values: new object[] { 12.545443000000001, 125.454m });

            migrationBuilder.InsertData(
                table: "other-entity",
                columns: new[] { "Id", "change_new_column_name" },
                values: new object[] { new Guid("a82d4414-ac32-480d-8813-4bbc6b9bf643"), "a82d4414ac32480d88134bbc6b9bf643" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "other-entity",
                keyColumn: "Id",
                keyValue: new Guid("a82d4414-ac32-480d-8813-4bbc6b9bf643"));

            migrationBuilder.UpdateData(
                table: "Blogs",
                keyColumn: "BlogId",
                keyValue: 2,
                columns: new[] { "Price", "PriceDecimal" },
                values: new object[] { 0.0, 0m });

            migrationBuilder.InsertData(
                table: "other-entity",
                columns: new[] { "Id", "change_new_column_name" },
                values: new object[] { new Guid("7d56ad6e-8d94-451e-be27-3e85485243c8"), "7d56ad6e8d94451ebe273e85485243c8" });
        }
    }
}
