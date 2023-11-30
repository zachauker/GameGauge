using Domain.Entities;
using IGDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ApiPlatform = IGDB.Models.Platform;
using ApiGame = IGDB.Models.Game;
using DomainPlatform = Domain.Entities.Platform;

namespace Persistence.Seeders;

public class GamePlatformSeed
{
    private ILogger<GameCompanySeed> _logger;
    private readonly DataContext _context;

    public GamePlatformSeed(ILogger<GameCompanySeed> logger, DataContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task SeedData()
    {
        if (_context.GamePlatforms.Any()) return;

        const int limit = 250;
        var offset = 0;

        var igdb = new IGDBClient("3p2ubjeep5tco48ebgolo2o4a1cjek", "7d32ezra4dgof88c1dlkvwkve8g4zb");

        var platforms = await _context.Platforms.ToListAsync();

        foreach (var platform in platforms)
        {
            var apiGames = await FetchPage(igdb, platform.IgdbId, limit, offset);
            ProcessGames(apiGames, platform);

            while (apiGames.Length == limit)
            {
                offset += limit;
                apiGames = await FetchPage(igdb, platform.IgdbId, limit, offset);
                ProcessGames(apiGames, platform);
            }

            offset = 0;
        }
    }

    private async Task<ApiGame[]> FetchPage(IGDBClient client, long? platformId, int limit, int offset)
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

    private async void ProcessGames(IEnumerable<ApiGame> apiGames, Platform platform)
    {
        foreach (var apiGame in apiGames)
        {
            if (apiGame == null) continue;

            var game = _context.Games.FirstOrDefault(game => game.IgdbId == apiGame.Id);


            if (game != null)
            {
                var existingGamePlatform =
                    _context.GamePlatforms.FirstOrDefault(gp => gp.GameId == game.Id && gp.PlatformId == platform.Id);

                if (existingGamePlatform == null)
                {
                    var gamePlatform = new GamePlatform
                    {
                        Game = game,
                        GameId = game.Id,
                        Platform = platform,
                        PlatformId = platform.Id
                    };

                    await _context.GamePlatforms.AddAsync(gamePlatform);
                }
            }
        }

        await _context.SaveChangesAsync();
    }
}