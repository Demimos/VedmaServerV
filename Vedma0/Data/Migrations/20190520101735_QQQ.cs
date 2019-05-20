using Microsoft.EntityFrameworkCore.Migrations;

namespace Vedma0.Data.Migrations
{
    public partial class QQQ : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MasterId",
                table: "Game",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Game_MasterId",
                table: "Game",
                column: "MasterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Game_AspNetUsers_MasterId",
                table: "Game",
                column: "MasterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Game_AspNetUsers_MasterId",
                table: "Game");

            migrationBuilder.DropIndex(
                name: "IX_Game_MasterId",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "MasterId",
                table: "Game");
        }
    }
}
