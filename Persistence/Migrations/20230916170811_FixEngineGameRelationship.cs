using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixEngineGameRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Engines_Games_GameId",
                table: "Engines");

            migrationBuilder.DropIndex(
                name: "IX_Engines_GameId",
                table: "Engines");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "Engines");

            migrationBuilder.CreateTable(
                name: "EngineGame",
                columns: table => new
                {
                    EnginesId = table.Column<Guid>(type: "TEXT", nullable: false),
                    GamesId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EngineGame", x => new { x.EnginesId, x.GamesId });
                    table.ForeignKey(
                        name: "FK_EngineGame_Engines_EnginesId",
                        column: x => x.EnginesId,
                        principalTable: "Engines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EngineGame_Games_GamesId",
                        column: x => x.GamesId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EngineGame_GamesId",
                table: "EngineGame",
                column: "GamesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EngineGame");

            migrationBuilder.AddColumn<Guid>(
                name: "GameId",
                table: "Engines",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Engines_GameId",
                table: "Engines",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Engines_Games_GameId",
                table: "Engines",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id");
        }
    }
}
