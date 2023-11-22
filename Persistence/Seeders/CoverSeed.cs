using Domain.Entities;
using ApiCover = IGDB.Models.Cover;
using DomainCover = Domain.Entities.Cover;
using IGDB;
using Microsoft.EntityFrameworkCore;


namespace Persistence.Seeders;

public static class CoverSeed
{
    public static async Task SeedData(DataContext context)
    {
        if (context.Artworks.Any()) return;

        const int limit = 250;
        var offset = 0;

        var igdb = new IGDBClient("3p2ubjeep5tco48ebgolo2o4a1cjek", "7d32ezra4dgof88c1dlkvwkve8g4zb");

        var covers = await FetchPage(igdb, limit, offset);
        ProcessRatings(covers, context);

        while (covers.Length == limit)
        {
            offset += limit;
            covers = await FetchPage(igdb, limit, offset);
            ProcessRatings(covers, context);
        }
    }

    private static async Task<ApiCover[]> FetchPage(IGDBClient client, int limit, int offset)
    {
        var query = $"""
                     
                                     fields *;
                                     limit {limit};
                                     offset {offset};
                                     
                     """;

        var apiCovers = await client.QueryAsync<ApiCover>(IGDBClient.Endpoints.Covers, query);
        return apiCovers;
    }

    private static async void ProcessRatings(IEnumerable<ApiCover> apiCovers, DataContext context)
    {
        foreach (var apiCover in apiCovers)
        {
            if (apiCover == null) continue;

            var game = context.Games.FirstOrDefault(game => game.IgdbId == apiCover.Game.Value.Id);
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
                
                await context.Covers.AddRangeAsync(cover);
            }
        }
        
        await context.SaveChangesAsync();
    }
}