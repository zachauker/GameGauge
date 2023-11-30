using Domain.Entities;
using IGDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ApiAgeRating = IGDB.Models.AgeRating;
using ApiGame = IGDB.Models.Game;
using DomainAgeRating = Domain.Entities.AgeRating;

namespace Persistence.Seeders;

public class GameAgeRatingSeed
{
    private ILogger<GameCompanySeed> _logger;
    private readonly DataContext _context;

    public GameAgeRatingSeed(ILogger<GameCompanySeed> logger, DataContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task SeedData()
    {
        if (await _context.GameAgeRatings.AnyAsync()) return;

        const int limit = 250;
        var offset = 0;

        var igdb = new IGDBClient("3p2ubjeep5tco48ebgolo2o4a1cjek", "7d32ezra4dgof88c1dlkvwkve8g4zb");

        var apiGames = await FetchPage(igdb, limit, offset);
        ProcessGames(apiGames);

        while (apiGames.Length == limit)
        {
            offset += limit;
            apiGames = await FetchPage(igdb, limit, offset);
            ProcessGames(apiGames);
        }
    }

    private async Task<ApiGame[]> FetchPage(IGDBClient client, int limit, int offset)
    {
        var query = $"""
                     
                                     fields name,id, age_ratings.*;
                                     limit {limit};
                                     offset {offset};
                                     
                     """;

        var apiGames = await client.QueryAsync<ApiGame>(IGDBClient.Endpoints.Games, query);
        return apiGames;
    }

    private async void ProcessGames(IEnumerable<ApiGame> apiGames)
    {
        var gameAgeRatingsToAdd = new List<GameAgeRating>();
        
        foreach (var apiGame in apiGames)
        {
            if (apiGame == null) continue;

            var game = _context.Games.FirstOrDefault(game => game.IgdbId == apiGame.Id);

            if (game != null)
            {
                if (apiGame.AgeRatings != null)
                {
                    foreach (var apiAgeRating in apiGame.AgeRatings.Values)
                    {
                        var ageRating = _context.AgeRatings.FirstOrDefault(ar => ar.IgdbId == apiAgeRating.Id);

                        if (ageRating != null)
                        {
                            var existingGameRating = _context.GameAgeRatings
                                .FirstOrDefault(gg => gg.GameId == game.Id && gg.AgeRatingId == ageRating.Id);

                            if (existingGameRating != null) continue;
                            
                            var gameAgeRating = new GameAgeRating
                            {
                                Game = game,
                                GameId = game.Id,
                                AgeRating = ageRating,
                                AgeRatingId = ageRating.Id
                            };

                            gameAgeRatingsToAdd.Add(gameAgeRating);
                        }
                    }
                }
            }
        }

        await _context.AddRangeAsync(gameAgeRatingsToAdd);
        await _context.SaveChangesAsync();
    }
}