using Microsoft.EntityFrameworkCore.Migrations;

namespace ApartmentReservation.Persistence.Migrations
{
    public partial class MakeApartmentAmenityManyToManyRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Amenities_Apartments_ApartmentId",
                table: "Amenities");

            migrationBuilder.DropIndex(
                name: "IX_Amenities_ApartmentId",
                table: "Amenities");

            migrationBuilder.DropColumn(
                name: "ApartmentId",
                table: "Amenities");

            migrationBuilder.CreateTable(
                name: "ApartmentAmenity",
                columns: table => new
                {
                    ApartmentId = table.Column<long>(nullable: false),
                    AmenityId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApartmentAmenity", x => new { x.ApartmentId, x.AmenityId });
                    table.ForeignKey(
                        name: "FK_ApartmentAmenity_Amenities_AmenityId",
                        column: x => x.AmenityId,
                        principalTable: "Amenities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApartmentAmenity_Apartments_ApartmentId",
                        column: x => x.ApartmentId,
                        principalTable: "Apartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApartmentAmenity_AmenityId",
                table: "ApartmentAmenity",
                column: "AmenityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApartmentAmenity");

            migrationBuilder.AddColumn<long>(
                name: "ApartmentId",
                table: "Amenities",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Amenities_ApartmentId",
                table: "Amenities",
                column: "ApartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Amenities_Apartments_ApartmentId",
                table: "Amenities",
                column: "ApartmentId",
                principalTable: "Apartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
