using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Vedma0.Data.Migrations
{
    public partial class QQ : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Game",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    OwnerId = table.Column<string>(nullable: true),
                    IncludeVR = table.Column<bool>(nullable: false),
                    IncludeGeo = table.Column<bool>(nullable: false),
                    IncludeGeoFence = table.Column<bool>(nullable: false),
                    IncludeNews = table.Column<bool>(nullable: false),
                    IncludeNewsPublishing = table.Column<bool>(nullable: false),
                    IncludeNewsRate = table.Column<bool>(nullable: false),
                    IncludeNewsComments = table.Column<bool>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Game", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Game");
        }
    }
}
