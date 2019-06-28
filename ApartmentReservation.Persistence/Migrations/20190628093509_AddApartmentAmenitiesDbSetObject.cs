using Microsoft.EntityFrameworkCore.Migrations;

namespace ApartmentReservation.Persistence.Migrations
{
    public partial class AddApartmentAmenitiesDbSetObject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApartmentAmenity_Amenities_AmenityId",
                table: "ApartmentAmenity");

            migrationBuilder.DropForeignKey(
                name: "FK_ApartmentAmenity_Apartments_ApartmentId",
                table: "ApartmentAmenity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApartmentAmenity",
                table: "ApartmentAmenity");

            migrationBuilder.RenameTable(
                name: "ApartmentAmenity",
                newName: "ApartmentAmenities");

            migrationBuilder.RenameIndex(
                name: "IX_ApartmentAmenity_AmenityId",
                table: "ApartmentAmenities",
                newName: "IX_ApartmentAmenities_AmenityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApartmentAmenities",
                table: "ApartmentAmenities",
                columns: new[] { "ApartmentId", "AmenityId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ApartmentAmenities_Amenities_AmenityId",
                table: "ApartmentAmenities",
                column: "AmenityId",
                principalTable: "Amenities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApartmentAmenities_Apartments_ApartmentId",
                table: "ApartmentAmenities",
                column: "ApartmentId",
                principalTable: "Apartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApartmentAmenities_Amenities_AmenityId",
                table: "ApartmentAmenities");

            migrationBuilder.DropForeignKey(
                name: "FK_ApartmentAmenities_Apartments_ApartmentId",
                table: "ApartmentAmenities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApartmentAmenities",
                table: "ApartmentAmenities");

            migrationBuilder.RenameTable(
                name: "ApartmentAmenities",
                newName: "ApartmentAmenity");

            migrationBuilder.RenameIndex(
                name: "IX_ApartmentAmenities_AmenityId",
                table: "ApartmentAmenity",
                newName: "IX_ApartmentAmenity_AmenityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApartmentAmenity",
                table: "ApartmentAmenity",
                columns: new[] { "ApartmentId", "AmenityId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ApartmentAmenity_Amenities_AmenityId",
                table: "ApartmentAmenity",
                column: "AmenityId",
                principalTable: "Amenities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApartmentAmenity_Apartments_ApartmentId",
                table: "ApartmentAmenity",
                column: "ApartmentId",
                principalTable: "Apartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
