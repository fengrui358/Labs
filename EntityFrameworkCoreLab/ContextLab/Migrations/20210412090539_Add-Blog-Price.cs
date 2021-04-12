using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ContextLab.Migrations
{
    public partial class AddBlogPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "other-entity",
                keyColumn: "Id",
                keyValue: new Guid("0cee4ffc-00bb-409e-a0f2-8ad16fa2361a"));

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Blogs",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<decimal>(
                name: "PriceDecimal",
                table: "Blogs",
                type: "decimal(5,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.InsertData(
                table: "other-entity",
                columns: new[] { "Id", "change_new_column_name" },
                values: new object[] { new Guid("7d56ad6e-8d94-451e-be27-3e85485243c8"), "7d56ad6e8d94451ebe273e85485243c8" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "other-entity",
                keyColumn: "Id",
                keyValue: new Guid("7d56ad6e-8d94-451e-be27-3e85485243c8"));

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "PriceDecimal",
                table: "Blogs");

            migrationBuilder.InsertData(
                table: "other-entity",
                columns: new[] { "Id", "change_new_column_name" },
                values: new object[] { new Guid("0cee4ffc-00bb-409e-a0f2-8ad16fa2361a"), "0cee4ffc00bb409ea0f28ad16fa2361a" });
        }
    }
}
