using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ContextLab.Migrations
{
    public partial class SchemaTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "other-entity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    TestString = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_other-entity", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "other-entity",
                columns: new[] { "Id", "TestString" },
                values: new object[] { new Guid("de7f49b9-20c7-4898-a2fd-8b408af30613"), "de7f49b920c74898a2fd8b408af30613" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "other-entity");
        }
    }
}
