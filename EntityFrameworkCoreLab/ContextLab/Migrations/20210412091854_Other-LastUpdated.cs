using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ContextLab.Migrations
{
    public partial class OtherLastUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "other-entity",
                keyColumn: "Id",
                keyValue: new Guid("a82d4414-ac32-480d-8813-4bbc6b9bf643"));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdated",
                table: "other-entity",
                type: "datetime(6)",
                nullable: false)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LastUpdatedDateTimeOffset",
                table: "other-entity",
                type: "datetime(6)",
                nullable: false)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn);

            migrationBuilder.InsertData(
                table: "other-entity",
                columns: new[] { "Id", "change_new_column_name" },
                values: new object[] { new Guid("cf6f929a-0100-4aa5-80c1-f9cd8f8bea88"), "cf6f929a01004aa580c1f9cd8f8bea88" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "other-entity",
                keyColumn: "Id",
                keyValue: new Guid("cf6f929a-0100-4aa5-80c1-f9cd8f8bea88"));

            migrationBuilder.DropColumn(
                name: "LastUpdated",
                table: "other-entity");

            migrationBuilder.DropColumn(
                name: "LastUpdatedDateTimeOffset",
                table: "other-entity");

            migrationBuilder.InsertData(
                table: "other-entity",
                columns: new[] { "Id", "change_new_column_name" },
                values: new object[] { new Guid("a82d4414-ac32-480d-8813-4bbc6b9bf643"), "a82d4414ac32480d88134bbc6b9bf643" });
        }
    }
}
