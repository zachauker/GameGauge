using Domain.Entities;
using ApiArtwork = IGDB.Models.Artwork;
using DomainArtwork = Domain.Entities.Artwork;
using IGDB;
using Microsoft.EntityFrameworkCore;


namespace Persistence.Seeders;

public static class ArtworkSeed
{
    public static async Task SeedData(DataContext context)
    {
        if (context.Artworks.Any()) return;

        const int limit = 250;
        var offset = 0;

        var igdb = new IGDBClient("3p2ubjeep5tco48ebgolo2o4a1cjek", "7d32ezra4dgof88c1dlkvwkve8g4zb");

        var artworks = await FetchPage(igdb, limit, offset);
        ProcessRatings(artworks, context);

        while (artworks.Length == limit)
        {
            offset += limit;
            artworks = await FetchPage(igdb, limit, offset);
            ProcessRatings(artworks, context);
        }
    }

    private static async Task<ApiArtwork[]> FetchPage(IGDBClient client, int limit, int offset)
    {
        var query = $"""
                     
                                     fields *;
                                     limit {limit};
                                     offset {offset};
                                     
                     """;

        var apiArtworks = await client.QueryAsync<ApiArtwork>(IGDBClient.Endpoints.Artworks, query);
        return apiArtworks;
    }

    private static async void ProcessRatings(IEnumerable<ApiArtwork> apiArtworks, DataContext context)
    {
        foreach (var apiArtwork in apiArtworks)
        {
            if (apiArtwork == null) continue;

            var game = context.Games.FirstOrDefault(game => game.IgdbId == apiArtwork.Game.Value.Id);
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
                
                await context.Artworks.AddRangeAsync(artwork);
            }
        }
        
        await context.SaveChangesAsync();
    }
}