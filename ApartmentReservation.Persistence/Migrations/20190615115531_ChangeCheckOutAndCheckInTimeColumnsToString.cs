using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApartmentReservation.Persistence.Migrations
{
    public partial class ChangeCheckOutAndCheckInTimeColumnsToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CheckOutTime",
                table: "Apartments",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CheckInTime",
                table: "Apartments",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CheckOutTime",
                table: "Apartments",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CheckInTime",
                table: "Apartments",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
