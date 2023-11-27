using Domain.Entities;
using ApiVideo = IGDB.Models.GameVideo;
using DomainVideo = Domain.Entities.GameVideo;
using IGDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace Persistence.Seeders;

public class GameVideoSeed
{
    private ILogger<GameCompanySeed> _logger;
    private readonly DataContext _context;

    public GameVideoSeed(ILogger<GameCompanySeed> logger, DataContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task SeedData()
    {
        if (await _context.GameVideos.AnyAsync()) return;

        const int limit = 250;
        var offset = 0;

        var igdb = new IGDBClient("3p2ubjeep5tco48ebgolo2o4a1cjek", "7d32ezra4dgof88c1dlkvwkve8g4zb");

        var videos = await FetchPage(igdb, limit, offset);
        ProcessRatings(videos);

        while (videos.Length == limit)
        {
            offset += limit;
            videos = await FetchPage(igdb, limit, offset);
            ProcessRatings(videos);
        }
    }

    private async Task<ApiVideo[]> FetchPage(IGDBClient client, int limit, int offset)
    {
        var query = $"""
                     
                                     fields *;
                                     limit {limit};
                                     offset {offset};
                                     
                     """;

        var apiVideos = await client.QueryAsync<ApiVideo>(IGDBClient.Endpoints.GameVideos, query);
        return apiVideos;
    }

    private async void ProcessRatings(IEnumerable<ApiVideo> apiVideos)
    {
        foreach (var apiVideo in apiVideos)
        {
            if (apiVideo == null) continue;

            var game = _context.Games.FirstOrDefault(game => game.IgdbId == apiVideo.Game.Value.Id);
            if (game != null)
            {
                var video = new DomainVideo
                {
                    Game = game,
                    GameId = game.Id,
                    Name = apiVideo.Name,
                    VideoId = apiVideo.VideoId
                };

                await _context.GameVideos.AddRangeAsync(video);
            }
        }

        await _context.SaveChangesAsync();
    }
}