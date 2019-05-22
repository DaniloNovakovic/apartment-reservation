using Microsoft.EntityFrameworkCore.Migrations;

namespace ApartmentReservation.Persistence.Migrations
{
    public partial class ChangedAdminHostAndGuestToUseInheritence : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Administrators_Users_UserId",
                table: "Administrators");

            migrationBuilder.DropForeignKey(
                name: "FK_Guests_Users_UserId",
                table: "Guests");

            migrationBuilder.DropForeignKey(
                name: "FK_Hosts_Users_UserId",
                table: "Hosts");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Hosts_UserId",
                table: "Hosts");

            migrationBuilder.DropIndex(
                name: "IX_Guests_UserId",
                table: "Guests");

            migrationBuilder.DropIndex(
                name: "IX_Administrators_UserId",
                table: "Administrators");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Hosts",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Guests",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Administrators",
                newName: "Password");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Hosts",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Hosts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Hosts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Hosts",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Guests",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Guests",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Guests",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Guests",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Administrators",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Administrators",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Administrators",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Administrators",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Hosts");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Hosts");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Hosts");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Guests");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Guests");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Guests");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Administrators");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Administrators");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Administrators");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Hosts",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Guests",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Administrators",
                newName: "UserId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Hosts",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Guests",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Administrators",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Role = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Hosts_UserId",
                table: "Hosts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Guests_UserId",
                table: "Guests",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Administrators_UserId",
                table: "Administrators",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Administrators_Users_UserId",
                table: "Administrators",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Guests_Users_UserId",
                table: "Guests",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Hosts_Users_UserId",
                table: "Hosts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
