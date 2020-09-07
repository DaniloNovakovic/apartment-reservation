using Microsoft.EntityFrameworkCore.Migrations;

namespace ApartmentReservation.Persistence.Migrations
{
    public partial class AddIsSyncNeededToUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSyncNeeded",
                table: "Users",
                nullable: false,
                defaultValue: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSyncNeeded",
                table: "Users");
        }
    }
}
