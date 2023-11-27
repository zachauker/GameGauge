using Domain.Entities;
using ApiArtwork = IGDB.Models.Artwork;
using DomainArtwork = Domain.Entities.Artwork;
using IGDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace Persistence.Seeders;

public class ArtworkSeed
{
    private ILogger<GameCompanySeed> _logger;
    private readonly DataContext _context;

    public ArtworkSeed(DataContext context, ILogger<GameCompanySeed> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task SeedData()
    {
        if (_context.Artworks.Any()) return;

        const int limit = 250;
        var offset = 0;

        var igdb = new IGDBClient("3p2ubjeep5tco48ebgolo2o4a1cjek", "7d32ezra4dgof88c1dlkvwkve8g4zb");

        var artworks = await FetchPage(igdb, limit, offset);
        ProcessRatings(artworks);

        while (artworks.Length == limit)
        {
            offset += limit;
            artworks = await FetchPage(igdb, limit, offset);
            ProcessRatings(artworks);
        }
    }

    private async Task<ApiArtwork[]> FetchPage(IGDBClient client, int limit, int offset)
    {
        var query = $"""
                     
                                     fields *;
                                     limit {limit};
                                     offset {offset};
                                     
                     """;

        var apiArtworks = await client.QueryAsync<ApiArtwork>(IGDBClient.Endpoints.Artworks, query);
        return apiArtworks;
    }

    private async void ProcessRatings(IEnumerable<ApiArtwork> apiArtworks)
    {
        foreach (var apiArtwork in apiArtworks)
        {
            if (apiArtwork == null) continue;

            var game = _context.Games.FirstOrDefault(game => game.IgdbId == apiArtwork.Game.Id);
            if (game != null)
            {
                var artwork = new DomainArtwork
                {
                    Game = game,
                    GameId = game.Id,
                    Height = apiArtwork.Height,
                    Width = apiArtwork.Width,
                    Url = apiArtwork.Url,
                    ImageId = apiArtwork.ImageId
                };

                await _context.Artworks.AddRangeAsync(artwork);
            }
        }

        await _context.SaveChangesAsync();
    }
}