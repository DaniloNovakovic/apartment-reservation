using Microsoft.EntityFrameworkCore.Migrations;

namespace ApartmentReservation.Persistence.Migrations
{
    public partial class RemovedIdFromAdministrator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hosts_Users_UserId",
                table: "Hosts");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Administrators");

            migrationBuilder.AddForeignKey(
                name: "FK_Hosts_Users_UserId",
                table: "Hosts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hosts_Users_UserId",
                table: "Hosts");

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "Administrators",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddForeignKey(
                name: "FK_Hosts_Users_UserId",
                table: "Hosts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
