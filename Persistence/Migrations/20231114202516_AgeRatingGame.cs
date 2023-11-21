using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AgeRatingGame : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AgeRatings_Games_GameId",
                table: "AgeRatings");

            migrationBuilder.DropIndex(
                name: "IX_AgeRatings_GameId",
                table: "AgeRatings");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "AgeRatings");

            migrationBuilder.CreateTable(
                name: "GameAgeRating",
                columns: table => new
                {
                    GameId = table.Column<Guid>(type: "uuid", nullable: false),
                    AgeRatingId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameAgeRating", x => new { x.GameId, x.AgeRatingId });
                    table.ForeignKey(
                        name: "FK_GameAgeRating_AgeRatings_AgeRatingId",
                        column: x => x.AgeRatingId,
                        principalTable: "AgeRatings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameAgeRating_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameAgeRating_AgeRatingId",
                table: "GameAgeRating",
                column: "AgeRatingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameAgeRating");

            migrationBuilder.AddColumn<Guid>(
                name: "GameId",
                table: "AgeRatings",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AgeRatings_GameId",
                table: "AgeRatings",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_AgeRatings_Games_GameId",
                table: "AgeRatings",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id");
        }
    }
}
