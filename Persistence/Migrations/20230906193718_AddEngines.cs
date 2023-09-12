using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddEngines : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Engine_EngineId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Platforms_Engine_EngineId",
                table: "Platforms");

            migrationBuilder.DropTable(
                name: "Engine");

            migrationBuilder.DropIndex(
                name: "IX_Games_EngineId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "EngineId",
                table: "Games");

            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                table: "Platforms",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Platforms",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.CreateTable(
                name: "Engines",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Slug = table.Column<string>(type: "TEXT", nullable: true),
                    IgdbId = table.Column<long>(type: "INTEGER", nullable: true),
                    GameId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Engines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Engines_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Engines_GameId",
                table: "Engines",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Platforms_Engines_EngineId",
                table: "Platforms",
                column: "EngineId",
                principalTable: "Engines",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Platforms_Engines_EngineId",
                table: "Platforms");

            migrationBuilder.DropTable(
                name: "Engines");

            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                table: "Platforms",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Platforms",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EngineId",
                table: "Games",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Engine",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Slug = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Engine", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_EngineId",
                table: "Games",
                column: "EngineId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Engine_EngineId",
                table: "Games",
                column: "EngineId",
                principalTable: "Engine",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Platforms_Engine_EngineId",
                table: "Platforms",
                column: "EngineId",
                principalTable: "Engine",
                principalColumn: "Id");
        }
    }
}
