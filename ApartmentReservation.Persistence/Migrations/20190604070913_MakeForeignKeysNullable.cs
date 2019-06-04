using Microsoft.EntityFrameworkCore.Migrations;

namespace ApartmentReservation.Persistence.Migrations
{
    public partial class MakeForeignKeysNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apartments_Locations_LocationId",
                table: "Apartments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Apartments_ApartmentId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Guests_GuestUserId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_ForRentalDates_Apartments_ApartmentId",
                table: "ForRentalDates");

            migrationBuilder.DropForeignKey(
                name: "FK_Guests_Users_UserId",
                table: "Guests");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Apartments_ApartmentId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Addresses_AddressId1",
                table: "Locations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Apartments_ApartmentId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Guests_GuestId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Locations_AddressId1",
                table: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Comments_GuestUserId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "AddressId1",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "GuestUserId",
                table: "Comments");

            migrationBuilder.AlterColumn<long>(
                name: "GuestId",
                table: "Reservations",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<long>(
                name: "ApartmentId",
                table: "Reservations",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<long>(
                name: "AddressId",
                table: "Locations",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ApartmentId",
                table: "Images",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<long>(
                name: "ApartmentId",
                table: "ForRentalDates",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<long>(
                name: "GuestId",
                table: "Comments",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ApartmentId",
                table: "Comments",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<long>(
                name: "LocationId",
                table: "Apartments",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.CreateIndex(
                name: "IX_Locations_AddressId",
                table: "Locations",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_GuestId",
                table: "Comments",
                column: "GuestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Apartments_Locations_LocationId",
                table: "Apartments",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Apartments_ApartmentId",
                table: "Comments",
                column: "ApartmentId",
                principalTable: "Apartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Guests_GuestId",
                table: "Comments",
                column: "GuestId",
                principalTable: "Guests",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ForRentalDates_Apartments_ApartmentId",
                table: "ForRentalDates",
                column: "ApartmentId",
                principalTable: "Apartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Guests_Users_UserId",
                table: "Guests",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Apartments_ApartmentId",
                table: "Images",
                column: "ApartmentId",
                principalTable: "Apartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Addresses_AddressId",
                table: "Locations",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Apartments_ApartmentId",
                table: "Reservations",
                column: "ApartmentId",
                principalTable: "Apartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Guests_GuestId",
                table: "Reservations",
                column: "GuestId",
                principalTable: "Guests",
                principalColumn: "UserId",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apartments_Locations_LocationId",
                table: "Apartments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Apartments_ApartmentId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Guests_GuestId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_ForRentalDates_Apartments_ApartmentId",
                table: "ForRentalDates");

            migrationBuilder.DropForeignKey(
                name: "FK_Guests_Users_UserId",
                table: "Guests");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Apartments_ApartmentId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Addresses_AddressId",
                table: "Locations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Apartments_ApartmentId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Guests_GuestId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Locations_AddressId",
                table: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Comments_GuestId",
                table: "Comments");

            migrationBuilder.AlterColumn<long>(
                name: "GuestId",
                table: "Reservations",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ApartmentId",
                table: "Reservations",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AddressId",
                table: "Locations",
                nullable: true,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "AddressId1",
                table: "Locations",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ApartmentId",
                table: "Images",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ApartmentId",
                table: "ForRentalDates",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GuestId",
                table: "Comments",
                nullable: true,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ApartmentId",
                table: "Comments",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "GuestUserId",
                table: "Comments",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "LocationId",
                table: "Apartments",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Locations_AddressId1",
                table: "Locations",
                column: "AddressId1");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_GuestUserId",
                table: "Comments",
                column: "GuestUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Apartments_Locations_LocationId",
                table: "Apartments",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Apartments_ApartmentId",
                table: "Comments",
                column: "ApartmentId",
                principalTable: "Apartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Guests_GuestUserId",
                table: "Comments",
                column: "GuestUserId",
                principalTable: "Guests",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ForRentalDates_Apartments_ApartmentId",
                table: "ForRentalDates",
                column: "ApartmentId",
                principalTable: "Apartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Guests_Users_UserId",
                table: "Guests",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Apartments_ApartmentId",
                table: "Images",
                column: "ApartmentId",
                principalTable: "Apartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Addresses_AddressId1",
                table: "Locations",
                column: "AddressId1",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Apartments_ApartmentId",
                table: "Reservations",
                column: "ApartmentId",
                principalTable: "Apartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Guests_GuestId",
                table: "Reservations",
                column: "GuestId",
                principalTable: "Guests",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
