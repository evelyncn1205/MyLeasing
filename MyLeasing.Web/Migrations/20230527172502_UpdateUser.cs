using Microsoft.EntityFrameworkCore.Migrations;

namespace MyLeasing.Web.Migrations
{
    public partial class UpdateUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Owners_AspNetUsers_UserId1",
                table: "Owners");

            migrationBuilder.DropIndex(
                name: "IX_Owners_UserId1",
                table: "Owners");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Owners");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Owners",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Owners_UserId",
                table: "Owners",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Owners_AspNetUsers_UserId",
                table: "Owners",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Owners_AspNetUsers_UserId",
                table: "Owners");

            migrationBuilder.DropIndex(
                name: "IX_Owners_UserId",
                table: "Owners");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Owners",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Owners",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Owners_UserId1",
                table: "Owners",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Owners_AspNetUsers_UserId1",
                table: "Owners",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
