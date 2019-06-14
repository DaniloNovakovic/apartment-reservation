using Microsoft.EntityFrameworkCore.Migrations;

namespace ApartmentReservation.Persistence.Migrations
{
    public partial class AddCountryNameColumnToAddressTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CountryName",
                table: "Addresses",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountryName",
                table: "Addresses");
        }
    }
}
