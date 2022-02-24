using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataBasePerformanceLab.Migrations
{
    public partial class RemoveIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AppDeviceGps_CreationTime",
                table: "AppDeviceGps");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AppDeviceGps_CreationTime",
                table: "AppDeviceGps",
                column: "CreationTime");
        }
    }
}
