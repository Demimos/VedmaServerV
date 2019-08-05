using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Vedma0.Migrations
{
    public partial class six : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Adress",
                table: "Presets",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "AllowAnonymus",
                table: "Presets",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Presets",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Presets",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Presets",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Body",
                table: "GameEntities",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTime",
                table: "GameEntities",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "GameEntities",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PublisherId",
                table: "GameEntities",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GameEntities_PublisherId",
                table: "GameEntities",
                column: "PublisherId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameEntities_Presets_PublisherId",
                table: "GameEntities",
                column: "PublisherId",
                principalTable: "Presets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameEntities_Presets_PublisherId",
                table: "GameEntities");

            migrationBuilder.DropIndex(
                name: "IX_GameEntities_PublisherId",
                table: "GameEntities");

            migrationBuilder.DropColumn(
                name: "Adress",
                table: "Presets");

            migrationBuilder.DropColumn(
                name: "AllowAnonymus",
                table: "Presets");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Presets");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Presets");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Presets");

            migrationBuilder.DropColumn(
                name: "Body",
                table: "GameEntities");

            migrationBuilder.DropColumn(
                name: "DateTime",
                table: "GameEntities");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "GameEntities");

            migrationBuilder.DropColumn(
                name: "PublisherId",
                table: "GameEntities");
        }
    }
}
