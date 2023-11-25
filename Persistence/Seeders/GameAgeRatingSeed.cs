using Domain.Entities;
using IGDB;
using Microsoft.EntityFrameworkCore;
using ApiAgeRating = IGDB.Models.AgeRating;
using ApiGame = IGDB.Models.Game;
using DomainAgeRating = Domain.Entities.AgeRating;

namespace Persistence.Seeders;

public class GameAgeRatingSeed
{
    public static async Task SeedData(DataContext context)
    {
        if (context.GameAgeRatings.Any()) return;

        const int limit = 250;
        var offset = 0;

        var igdb = new IGDBClient("3p2ubjeep5tco48ebgolo2o4a1cjek", "7d32ezra4dgof88c1dlkvwkve8g4zb");

        var ageRatings = context.AgeRatings.ToList();

        foreach (var ageRating in ageRatings)
        {
            var apiGames = await FetchPage(igdb, ageRating.IgdbId, limit, offset);
            ProcessGames(apiGames, ageRating, context);

            while (apiGames.Length == limit)
            {
                offset += limit;
                apiGames = await FetchPage(igdb, ageRating.IgdbId, limit, offset);
                ProcessGames(apiGames, ageRating, context);
            }

            offset = 0;
        }
    }

    private static async Task<ApiGame[]> FetchPage(IGDBClient client, long? ageRatingId, int limit, int offset)
    {
        var query = $"""
                     
                                     fields name,id;
                                     where age_ratings = ({ageRatingId});
                                     limit {limit};
                                     offset {offset};
                                     
                     """;

        var apiGames = await client.QueryAsync<ApiGame>(IGDBClient.Endpoints.Games, query);
        return apiGames;
    }

    private static async void ProcessGames(IEnumerable<ApiGame> apiGames, AgeRating ageRating, DataContext context)
    {
        foreach (var apiGame in apiGames)
        {
            if (apiGame == null) continue;

            var game = context.Games.FirstOrDefault(game => game.IgdbId == apiGame.Id);

            if (game != null)
            {
                var existingGameRating = context.GameAgeRatings
                    .FirstOrDefault(gg => gg.GameId == game.Id && gg.AgeRatingId == ageRating.Id);

                if (existingGameRating == null)
                {
                    var gameAgeRating = new GameAgeRating
                    {
                        Game = game,
                        GameId = game.Id,
                        AgeRating = ageRating,
                        AgeRatingId = ageRating.Id
                    };

                    await context.GameAgeRatings.AddAsync(gameAgeRating);
                }
            }
        }

        await context.SaveChangesAsync();
    }
}