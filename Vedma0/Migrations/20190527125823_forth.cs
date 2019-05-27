using Microsoft.EntityFrameworkCore.Migrations;

namespace Vedma0.Migrations
{
    public partial class forth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "SortValue",
                table: "Properties",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<long>(
                name: "SortValue",
                table: "BaseProperties",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "SortValue",
                table: "Properties",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<int>(
                name: "SortValue",
                table: "BaseProperties",
                nullable: false,
                oldClrType: typeof(long));
        }
    }
}
