using Microsoft.EntityFrameworkCore.Migrations;

namespace Vedma0.Migrations
{
    public partial class third : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "SortValue",
                table: "Presets",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "SortValue",
                table: "Presets",
                nullable: false,
                oldClrType: typeof(long));
        }
    }
}
