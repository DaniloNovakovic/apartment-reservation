using Microsoft.EntityFrameworkCore.Migrations;

namespace ApartmentReservation.Persistence.Migrations
{
    public partial class AddIsSyncNeededToReservationsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSyncNeeded",
                table: "Reservations",
                nullable: false,
                defaultValue: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSyncNeeded",
                table: "Reservations");
        }
    }
}
