using Microsoft.EntityFrameworkCore.Migrations;

namespace Vedma0.Data.Migrations
{
    public partial class Second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "_MasterIds",
                table: "Games",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "_MasterIds",
                table: "Games");
        }
    }
}
