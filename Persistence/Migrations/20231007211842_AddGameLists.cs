using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddGameLists : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "GameListId",
                table: "Games",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GameLists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameLists", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_GameLists_GameListId",
                table: "Games");

            migrationBuilder.DropTable(
                name: "GameLists");

            migrationBuilder.DropIndex(
                name: "IX_Games_GameListId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "GameListId",
                table: "Games");
        }
    }
}
