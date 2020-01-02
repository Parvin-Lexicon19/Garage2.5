using Microsoft.EntityFrameworkCore.Migrations;

namespace Garage2._5.Migrations
{
    public partial class init1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VehicleTypeId",
                table: "ParkedVehicle",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VehicleTypeId",
                table: "ParkedVehicle");
        }
    }
}
