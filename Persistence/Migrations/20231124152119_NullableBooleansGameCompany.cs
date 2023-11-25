using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class NullableBooleansGameCompany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameAgeRating_AgeRatings_AgeRatingId",
                table: "GameAgeRating");

            migrationBuilder.DropForeignKey(
                name: "FK_GameAgeRating_Games_GameId",
                table: "GameAgeRating");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameAgeRating",
                table: "GameAgeRating");

            migrationBuilder.RenameTable(
                name: "GameAgeRating",
                newName: "GameAgeRatings");

            migrationBuilder.RenameIndex(
                name: "IX_GameAgeRating_AgeRatingId",
                table: "GameAgeRatings",
                newName: "IX_GameAgeRatings_AgeRatingId");

            migrationBuilder.AlterColumn<bool>(
                name: "IsSupporter",
                table: "GameCompanies",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<bool>(
                name: "IsPublisher",
                table: "GameCompanies",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<bool>(
                name: "IsPorter",
                table: "GameCompanies",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeveloper",
                table: "GameCompanies",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameAgeRatings",
                table: "GameAgeRatings",
                columns: new[] { "GameId", "AgeRatingId" });

            migrationBuilder.AddForeignKey(
                name: "FK_GameAgeRatings_AgeRatings_AgeRatingId",
                table: "GameAgeRatings",
                column: "AgeRatingId",
                principalTable: "AgeRatings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameAgeRatings_Games_GameId",
                table: "GameAgeRatings",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameAgeRatings_AgeRatings_AgeRatingId",
                table: "GameAgeRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_GameAgeRatings_Games_GameId",
                table: "GameAgeRatings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameAgeRatings",
                table: "GameAgeRatings");

            migrationBuilder.RenameTable(
                name: "GameAgeRatings",
                newName: "GameAgeRating");

            migrationBuilder.RenameIndex(
                name: "IX_GameAgeRatings_AgeRatingId",
                table: "GameAgeRating",
                newName: "IX_GameAgeRating_AgeRatingId");

            migrationBuilder.AlterColumn<bool>(
                name: "IsSupporter",
                table: "GameCompanies",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsPublisher",
                table: "GameCompanies",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsPorter",
                table: "GameCompanies",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeveloper",
                table: "GameCompanies",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameAgeRating",
                table: "GameAgeRating",
                columns: new[] { "GameId", "AgeRatingId" });

            migrationBuilder.AddForeignKey(
                name: "FK_GameAgeRating_AgeRatings_AgeRatingId",
                table: "GameAgeRating",
                column: "AgeRatingId",
                principalTable: "AgeRatings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameAgeRating_Games_GameId",
                table: "GameAgeRating",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
