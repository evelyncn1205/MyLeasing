using Microsoft.EntityFrameworkCore.Migrations;

namespace MyLeasing.Web.Migrations
{
    public partial class InitOwner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FistName",
                table: "Owners");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Owners",
                newName: "OwnerName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OwnerName",
                table: "Owners",
                newName: "LastName");

            migrationBuilder.AddColumn<string>(
                name: "FistName",
                table: "Owners",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
