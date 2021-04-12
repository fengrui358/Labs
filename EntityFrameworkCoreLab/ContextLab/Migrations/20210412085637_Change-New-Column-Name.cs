using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ContextLab.Migrations
{
    public partial class ChangeNewColumnName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "other-entity",
                keyColumn: "Id",
                keyValue: new Guid("46d68bbb-0865-4b78-bf7b-0476c77917de"));

            migrationBuilder.RenameColumn(
                name: "TestString",
                table: "other-entity",
                newName: "change_new_column_name");

            migrationBuilder.InsertData(
                table: "other-entity",
                columns: new[] { "Id", "change_new_column_name" },
                values: new object[] { new Guid("4322be36-8577-42ad-8815-e0f1b1b06720"), "4322be36857742ad8815e0f1b1b06720" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "other-entity",
                keyColumn: "Id",
                keyValue: new Guid("4322be36-8577-42ad-8815-e0f1b1b06720"));

            migrationBuilder.RenameColumn(
                name: "change_new_column_name",
                table: "other-entity",
                newName: "TestString");

            migrationBuilder.InsertData(
                table: "other-entity",
                columns: new[] { "Id", "TestString" },
                values: new object[] { new Guid("46d68bbb-0865-4b78-bf7b-0476c77917de"), "46d68bbb08654b78bf7b0476c77917de" });
        }
    }
}
