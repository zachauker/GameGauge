using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ReviewChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameCompany_Companies_CompanyId",
                table: "GameCompany");

            migrationBuilder.DropForeignKey(
                name: "FK_GameCompany_Games_GameId",
                table: "GameCompany");

            migrationBuilder.DropForeignKey(
                name: "FK_GameEngine_Engines_EngineId",
                table: "GameEngine");

            migrationBuilder.DropForeignKey(
                name: "FK_GameEngine_Games_GameId",
                table: "GameEngine");

            migrationBuilder.DropForeignKey(
                name: "FK_GameGenre_Games_GameId",
                table: "GameGenre");

            migrationBuilder.DropForeignKey(
                name: "FK_GameGenre_Genres_GenreId",
                table: "GameGenre");

            migrationBuilder.DropForeignKey(
                name: "FK_GamePlatform_Games_GameId",
                table: "GamePlatform");

            migrationBuilder.DropForeignKey(
                name: "FK_GamePlatform_Platforms_PlatformId",
                table: "GamePlatform");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GamePlatform",
                table: "GamePlatform");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameGenre",
                table: "GameGenre");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameEngine",
                table: "GameEngine");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameCompany",
                table: "GameCompany");

            migrationBuilder.RenameTable(
                name: "GamePlatform",
                newName: "GamePlatforms");

            migrationBuilder.RenameTable(
                name: "GameGenre",
                newName: "GameGenres");

            migrationBuilder.RenameTable(
                name: "GameEngine",
                newName: "GameEngines");

            migrationBuilder.RenameTable(
                name: "GameCompany",
                newName: "GameCompanies");

            migrationBuilder.RenameIndex(
                name: "IX_GamePlatform_GameId",
                table: "GamePlatforms",
                newName: "IX_GamePlatforms_GameId");

            migrationBuilder.RenameIndex(
                name: "IX_GameGenre_GameId",
                table: "GameGenres",
                newName: "IX_GameGenres_GameId");

            migrationBuilder.RenameIndex(
                name: "IX_GameEngine_GameId",
                table: "GameEngines",
                newName: "IX_GameEngines_GameId");

            migrationBuilder.RenameIndex(
                name: "IX_GameCompany_CompanyId",
                table: "GameCompanies",
                newName: "IX_GameCompanies_CompanyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GamePlatforms",
                table: "GamePlatforms",
                columns: new[] { "PlatformId", "GameId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameGenres",
                table: "GameGenres",
                columns: new[] { "GenreId", "GameId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameEngines",
                table: "GameEngines",
                columns: new[] { "EngineId", "GameId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameCompanies",
                table: "GameCompanies",
                columns: new[] { "GameId", "CompanyId" });

            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    GameId = table.Column<Guid>(type: "uuid", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    Rating = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Review", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Review_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Review_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Review_GameId",
                table: "Review",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Review_UserId",
                table: "Review",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameCompanies_Companies_CompanyId",
                table: "GameCompanies",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameCompanies_Games_GameId",
                table: "GameCompanies",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameEngines_Engines_EngineId",
                table: "GameEngines",
                column: "EngineId",
                principalTable: "Engines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameEngines_Games_GameId",
                table: "GameEngines",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameGenres_Games_GameId",
                table: "GameGenres",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameGenres_Genres_GenreId",
                table: "GameGenres",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GamePlatforms_Games_GameId",
                table: "GamePlatforms",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GamePlatforms_Platforms_PlatformId",
                table: "GamePlatforms",
                column: "PlatformId",
                principalTable: "Platforms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameCompanies_Companies_CompanyId",
                table: "GameCompanies");

            migrationBuilder.DropForeignKey(
                name: "FK_GameCompanies_Games_GameId",
                table: "GameCompanies");

            migrationBuilder.DropForeignKey(
                name: "FK_GameEngines_Engines_EngineId",
                table: "GameEngines");

            migrationBuilder.DropForeignKey(
                name: "FK_GameEngines_Games_GameId",
                table: "GameEngines");

            migrationBuilder.DropForeignKey(
                name: "FK_GameGenres_Games_GameId",
                table: "GameGenres");

            migrationBuilder.DropForeignKey(
                name: "FK_GameGenres_Genres_GenreId",
                table: "GameGenres");

            migrationBuilder.DropForeignKey(
                name: "FK_GamePlatforms_Games_GameId",
                table: "GamePlatforms");

            migrationBuilder.DropForeignKey(
                name: "FK_GamePlatforms_Platforms_PlatformId",
                table: "GamePlatforms");

            migrationBuilder.DropTable(
                name: "Review");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GamePlatforms",
                table: "GamePlatforms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameGenres",
                table: "GameGenres");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameEngines",
                table: "GameEngines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameCompanies",
                table: "GameCompanies");

            migrationBuilder.RenameTable(
                name: "GamePlatforms",
                newName: "GamePlatform");

            migrationBuilder.RenameTable(
                name: "GameGenres",
                newName: "GameGenre");

            migrationBuilder.RenameTable(
                name: "GameEngines",
                newName: "GameEngine");

            migrationBuilder.RenameTable(
                name: "GameCompanies",
                newName: "GameCompany");

            migrationBuilder.RenameIndex(
                name: "IX_GamePlatforms_GameId",
                table: "GamePlatform",
                newName: "IX_GamePlatform_GameId");

            migrationBuilder.RenameIndex(
                name: "IX_GameGenres_GameId",
                table: "GameGenre",
                newName: "IX_GameGenre_GameId");

            migrationBuilder.RenameIndex(
                name: "IX_GameEngines_GameId",
                table: "GameEngine",
                newName: "IX_GameEngine_GameId");

            migrationBuilder.RenameIndex(
                name: "IX_GameCompanies_CompanyId",
                table: "GameCompany",
                newName: "IX_GameCompany_CompanyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GamePlatform",
                table: "GamePlatform",
                columns: new[] { "PlatformId", "GameId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameGenre",
                table: "GameGenre",
                columns: new[] { "GenreId", "GameId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameEngine",
                table: "GameEngine",
                columns: new[] { "EngineId", "GameId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameCompany",
                table: "GameCompany",
                columns: new[] { "GameId", "CompanyId" });

            migrationBuilder.AddForeignKey(
                name: "FK_GameCompany_Companies_CompanyId",
                table: "GameCompany",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameCompany_Games_GameId",
                table: "GameCompany",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameEngine_Engines_EngineId",
                table: "GameEngine",
                column: "EngineId",
                principalTable: "Engines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameEngine_Games_GameId",
                table: "GameEngine",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameGenre_Games_GameId",
                table: "GameGenre",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameGenre_Genres_GenreId",
                table: "GameGenre",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GamePlatform_Games_GameId",
                table: "GamePlatform",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GamePlatform_Platforms_PlatformId",
                table: "GamePlatform",
                column: "PlatformId",
                principalTable: "Platforms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
