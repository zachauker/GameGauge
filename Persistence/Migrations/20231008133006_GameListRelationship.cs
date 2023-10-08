using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class GameListRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_GameLists_GameListId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_GameListId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "GameListId",
                table: "Games");

            migrationBuilder.CreateTable(
                name: "GameListGames",
                columns: table => new
                {
                    GameListsId = table.Column<Guid>(type: "TEXT", nullable: false),
                    GamesId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Position = table.Column<int>(type: "INTEGER", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValue: DateTime.UtcNow)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameListGames", x => new { x.GameListsId, x.GamesId });
                    table.ForeignKey(
                        name: "FK_GameListGames_GameLists_GameListsId",
                        column: x => x.GameListsId,
                        principalTable: "GameLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameListGames_Games_GamesId",
                        column: x => x.GamesId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameListGames_GamesId",
                table: "GameListGames",
                column: "GamesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameGameList");

            migrationBuilder.AddColumn<Guid>(
                name: "GameListId",
                table: "Games",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Games_GameListId",
                table: "Games",
                column: "GameListId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_GameLists_GameListId",
                table: "Games",
                column: "GameListId",
                principalTable: "GameLists",
                principalColumn: "Id");
        }
    }
}
