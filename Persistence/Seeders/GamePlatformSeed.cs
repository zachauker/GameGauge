using Domain.Entities;
using IGDB;
using Microsoft.EntityFrameworkCore;
using ApiPlatform = IGDB.Models.Platform;
using ApiGame = IGDB.Models.Game;
using DomainPlatform = Domain.Entities.Platform;

namespace Persistence.Seeders;

public class GamePlatformSeed
{
    public static async Task SeedData(DataContext context)
    {
        if (context.GamePlatforms.Any()) return;

        const int limit = 250;
        var offset = 0;

        var igdb = new IGDBClient("3p2ubjeep5tco48ebgolo2o4a1cjek", "7d32ezra4dgof88c1dlkvwkve8g4zb");

        var platforms = context.Platforms.ToList();

        foreach (var platform in platforms)
        {
            var apiGames = await FetchPage(igdb, platform.IgdbId, limit, offset);
            ProcessGames(apiGames, platform, context);

            while (apiGames.Length == limit)
            {
                offset += limit;
                apiGames = await FetchPage(igdb, platform.IgdbId, limit, offset);
                ProcessGames(apiGames, platform, context);
            }

            offset = 0;
        }
    }

    private static async Task<ApiGame[]> FetchPage(IGDBClient client, long? platformId, int limit, int offset)
    {
        var query = $"""
                     
                                     fields name,id;
                                     where platforms = ({platformId});
                                     limit {limit};
                                     offset {offset};
                                     
                     """;

        var apiGames = await client.QueryAsync<ApiGame>(IGDBClient.Endpoints.Games, query);
        return apiGames;
    }

    private static async void ProcessGames(IEnumerable<ApiGame> apiGames, Platform platform, DataContext context)
    {
        foreach (var apiGame in apiGames)
        {
            if (apiGame == null) continue;

            var game = context.Games.FirstOrDefault(game => game.IgdbId == apiGame.Id);

            var existingGamePlatform =
                context.GamePlatforms.FirstOrDefault(gp => gp.GameId == game.Id && gp.PlatformId == platform.Id);

            if (game != null)
            {
                if (existingGamePlatform == null)
                {
                    var gamePlatform = new GamePlatform
                    {
                        Game = game,
                        GameId = game.Id,
                        Platform = platform,
                        PlatformId = platform.Id
                    };

                    await context.GamePlatforms.AddRangeAsync(gamePlatform);
                }
            }
        }

        await context.SaveChangesAsync();
    }
}