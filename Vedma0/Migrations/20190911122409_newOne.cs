using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Vedma0.Migrations
{
    public partial class newOne : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Presets",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ArticleCount",
                table: "Presets",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CharacterReflection",
                columns: table => new
                {
                    OwnerId = table.Column<long>(nullable: false),
                    ReflectionId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterReflection", x => new { x.OwnerId, x.ReflectionId });
                    table.ForeignKey(
                        name: "FK_CharacterReflection_GameEntities_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "GameEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CharacterReflection_GameEntities_ReflectionId",
                        column: x => x.ReflectionId,
                        principalTable: "GameEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterReflection_ReflectionId",
                table: "CharacterReflection",
                column: "ReflectionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterReflection");

            migrationBuilder.DropColumn(
                name: "ArticleCount",
                table: "Presets");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Image",
                table: "Presets",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
