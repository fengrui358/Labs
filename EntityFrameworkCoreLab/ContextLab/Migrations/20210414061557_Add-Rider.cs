using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ContextLab.Migrations
{
    public partial class AddRider : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rider",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Mount = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rider", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Rider",
                columns: new[] { "Id", "Mount" },
                values: new object[] { 1, "Mule" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rider");
        }
    }
}
