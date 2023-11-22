using Domain.Entities;
using ApiVideo = IGDB.Models.GameVideo;
using DomainVideo = Domain.Entities.GameVideo;
using IGDB;
using Microsoft.EntityFrameworkCore;


namespace Persistence.Seeders;

public static class GameVideoSeed
{
    public static async Task SeedData(DataContext context)
    {
        if (context.GameVideos.Any()) return;

        const int limit = 250;
        var offset = 0;

        var igdb = new IGDBClient("3p2ubjeep5tco48ebgolo2o4a1cjek", "7d32ezra4dgof88c1dlkvwkve8g4zb");

        var videos = await FetchPage(igdb, limit, offset);
        ProcessRatings(videos, context);

        while (videos.Length == limit)
        {
            offset += limit;
            videos = await FetchPage(igdb, limit, offset);
            ProcessRatings(videos, context);
        }
    }

    private static async Task<ApiVideo[]> FetchPage(IGDBClient client, int limit, int offset)
    {
        var query = $"""
                     
                                     fields *;
                                     limit {limit};
                                     offset {offset};
                                     
                     """;

        var apiVideos = await client.QueryAsync<ApiVideo>(IGDBClient.Endpoints.GameVideos, query);
        return apiVideos;
    }

    private static async void ProcessRatings(IEnumerable<ApiVideo> apiVideos, DataContext context)
    {
        foreach (var apiVideo in apiVideos)
        {
            if (apiVideo == null) continue;

            var game = context.Games.FirstOrDefault(game => game.IgdbId == apiVideo.Game.Value.Id);
            if (game != null)
            {
                var video = new DomainVideo
                {
                    Game = game,
                    GameId = game.Id,
                    Name = apiVideo.Name,
                    VideoId = apiVideo.VideoId
                };
                
                await context.GameVideos.AddRangeAsync(video);
            }
        }
        
        await context.SaveChangesAsync();
    }
}