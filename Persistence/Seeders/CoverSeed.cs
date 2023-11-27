using Domain.Entities;
using ApiCover = IGDB.Models.Cover;
using DomainCover = Domain.Entities.Cover;
using IGDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace Persistence.Seeders;

public class CoverSeed
{
    private ILogger<GameCompanySeed> _logger;
    private readonly DataContext _context;

    public CoverSeed(ILogger<GameCompanySeed> logger, DataContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task SeedData()
    {
        if (_context.Artworks.Any()) return;

        const int limit = 250;
        var offset = 0;

        var igdb = new IGDBClient("3p2ubjeep5tco48ebgolo2o4a1cjek", "7d32ezra4dgof88c1dlkvwkve8g4zb");

        var covers = await FetchPage(igdb, limit, offset);
        ProcessRatings(covers);

        while (covers.Length == limit)
        {
            offset += limit;
            covers = await FetchPage(igdb, limit, offset);
            ProcessRatings(covers);
        }
    }

    private async Task<ApiCover[]> FetchPage(IGDBClient client, int limit, int offset)
    {
        var query = $"""
                     
                                     fields *;
                                     limit {limit};
                                     offset {offset};
                                     
                     """;

        var apiCovers = await client.QueryAsync<ApiCover>(IGDBClient.Endpoints.Covers, query);
        return apiCovers;
    }

    private async void ProcessRatings(IEnumerable<ApiCover> apiCovers)
    {
        foreach (var apiCover in apiCovers)
        {
            if (apiCover == null) continue;

            var game = _context.Games.FirstOrDefault(game => game.IgdbId == apiCover.Game.Value.Id);
            if (game != null)
            {
                var cover = new DomainCover
                {
                    Game = game,
                    GameId = game.Id,
                    Url = apiCover.Url,
                    Height = apiCover.Height,
                    Width = apiCover.Width,
                    ImageId = apiCover.ImageId
                };

                await _context.Covers.AddRangeAsync(cover);
            }
        }

        await _context.SaveChangesAsync();
    }
}