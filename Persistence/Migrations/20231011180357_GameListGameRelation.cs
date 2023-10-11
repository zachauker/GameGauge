using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class GameListGameRelation : Migration
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

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "GameLists",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GameListGames",
                columns: table => new
                {
                    GameListId = table.Column<Guid>(type: "TEXT", nullable: false),
                    GameId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Position = table.Column<int>(type: "INTEGER", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValue: DateTime.UtcNow)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameListGames", x => new { x.GameListId, x.GameId });
                    table.ForeignKey(
                        name: "FK_GameListGames_GameLists_GameListId",
                        column: x => x.GameListId,
                        principalTable: "GameLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameListGames_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameLists_UserId",
                table: "GameLists",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_GameListGames_GameId",
                table: "GameListGames",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameLists_AspNetUsers_UserId",
                table: "GameLists",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameLists_AspNetUsers_UserId",
                table: "GameLists");

            migrationBuilder.DropTable(
                name: "GameListGames");

            migrationBuilder.DropIndex(
                name: "IX_GameLists_UserId",
                table: "GameLists");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "GameLists");

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
