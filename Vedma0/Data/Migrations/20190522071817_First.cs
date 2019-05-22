using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Vedma0.Data.Migrations
{
    public partial class First : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CurrentGame",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailSignal",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PushToken",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
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
                    table.PrimaryKey("PK_Games", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameEntities",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    GameId = table.Column<Guid>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: true),
                    HasSuspendedSignal = table.Column<bool>(nullable: true),
                    InActiveMessage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameEntities_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GameEntities_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GameUsers",
                columns: table => new
                {
                    GameId = table.Column<Guid>(nullable: false),
                    VedmaUserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameUsers", x => new { x.GameId, x.VedmaUserId });
                    table.ForeignKey(
                        name: "FK_GameUsers_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameUsers_AspNetUsers_VedmaUserId",
                        column: x => x.VedmaUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: false),
                    Message = table.Column<string>(nullable: false),
                    DateTime = table.Column<DateTime>(nullable: false),
                    GameId = table.Column<Guid>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Logs_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Presets",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GameId = table.Column<Guid>(nullable: false),
                    SortValue = table.Column<int>(nullable: false),
                    _Abilities = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    SelfInsight = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Presets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Presets_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Diary",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: false),
                    Message = table.Column<string>(nullable: false),
                    DateTime = table.Column<DateTime>(nullable: false),
                    GameId = table.Column<Guid>(nullable: false),
                    CharacterId = table.Column<long>(nullable: true),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diary", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Diary_GameEntities_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "GameEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Diary_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BaseProperties",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    GameId = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    PresetId = table.Column<long>(nullable: true),
                    SortValue = table.Column<int>(nullable: false),
                    Visible = table.Column<bool>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    DefaultValue = table.Column<double>(nullable: true),
                    BaseTextProperty_DefaultValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseProperties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BaseProperties_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BaseProperties_Presets_PresetId",
                        column: x => x.PresetId,
                        principalTable: "Presets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EntityPreset",
                columns: table => new
                {
                    GameEntityId = table.Column<long>(nullable: false),
                    PresetId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityPreset", x => new { x.GameEntityId, x.PresetId });
                    table.ForeignKey(
                        name: "FK_EntityPreset_GameEntities_GameEntityId",
                        column: x => x.GameEntityId,
                        principalTable: "GameEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EntityPreset_Presets_PresetId",
                        column: x => x.PresetId,
                        principalTable: "Presets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Properties",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    GameId = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    PresetId = table.Column<long>(nullable: true),
                    SortValue = table.Column<int>(nullable: false),
                    Visible = table.Column<bool>(nullable: false),
                    GameEntityId = table.Column<long>(nullable: false),
                    BasePropertyId = table.Column<long>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    Value = table.Column<double>(nullable: true),
                    TextProperty_Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Properties_BaseProperties_BasePropertyId",
                        column: x => x.BasePropertyId,
                        principalTable: "BaseProperties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Properties_GameEntities_GameEntityId",
                        column: x => x.GameEntityId,
                        principalTable: "GameEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Properties_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Properties_Presets_PresetId",
                        column: x => x.PresetId,
                        principalTable: "Presets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BaseProperties_GameId",
                table: "BaseProperties",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseProperties_PresetId",
                table: "BaseProperties",
                column: "PresetId");

            migrationBuilder.CreateIndex(
                name: "IX_Diary_CharacterId",
                table: "Diary",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_Diary_GameId",
                table: "Diary",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityPreset_PresetId",
                table: "EntityPreset",
                column: "PresetId");

            migrationBuilder.CreateIndex(
                name: "IX_GameEntities_UserId",
                table: "GameEntities",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_GameEntities_GameId",
                table: "GameEntities",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_GameUsers_VedmaUserId",
                table: "GameUsers",
                column: "VedmaUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Logs_GameId",
                table: "Logs",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Presets_GameId",
                table: "Presets",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_BasePropertyId",
                table: "Properties",
                column: "BasePropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_GameEntityId",
                table: "Properties",
                column: "GameEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_GameId",
                table: "Properties",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_PresetId",
                table: "Properties",
                column: "PresetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Diary");

            migrationBuilder.DropTable(
                name: "EntityPreset");

            migrationBuilder.DropTable(
                name: "GameUsers");

            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "Properties");

            migrationBuilder.DropTable(
                name: "BaseProperties");

            migrationBuilder.DropTable(
                name: "GameEntities");

            migrationBuilder.DropTable(
                name: "Presets");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropColumn(
                name: "CurrentGame",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "EmailSignal",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PushToken",
                table: "AspNetUsers");
        }
    }
}
