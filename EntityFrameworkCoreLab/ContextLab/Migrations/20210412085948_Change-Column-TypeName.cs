using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ContextLab.Migrations
{
    public partial class ChangeColumnTypeName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "other-entity",
                keyColumn: "Id",
                keyValue: new Guid("4322be36-8577-42ad-8815-e0f1b1b06720"));

            migrationBuilder.AlterColumn<string>(
                name: "change_new_column_name",
                table: "other-entity",
                type: "varchar(200)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4");

            migrationBuilder.InsertData(
                table: "other-entity",
                columns: new[] { "Id", "change_new_column_name" },
                values: new object[] { new Guid("0cee4ffc-00bb-409e-a0f2-8ad16fa2361a"), "0cee4ffc00bb409ea0f28ad16fa2361a" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "other-entity",
                keyColumn: "Id",
                keyValue: new Guid("0cee4ffc-00bb-409e-a0f2-8ad16fa2361a"));

            migrationBuilder.AlterColumn<string>(
                name: "change_new_column_name",
                table: "other-entity",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(200)");

            migrationBuilder.InsertData(
                table: "other-entity",
                columns: new[] { "Id", "change_new_column_name" },
                values: new object[] { new Guid("4322be36-8577-42ad-8815-e0f1b1b06720"), "4322be36857742ad8815e0f1b1b06720" });
        }
    }
}
