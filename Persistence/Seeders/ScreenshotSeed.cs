using Domain.Entities;
using ApiScreenshot = IGDB.Models.Screenshot;
using DomainScreenshot = Domain.Entities.Screenshot;
using IGDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace Persistence.Seeders;

public class ScreenshotSeed
{
    private ILogger<ScreenshotSeed> _logger;
    private readonly DataContext _context;

    public ScreenshotSeed(DataContext context, ILogger<ScreenshotSeed> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task SeedData()
    {
        if (_context.Screenshots.Any()) return;

        const int limit = 250;
        var offset = 0;

        var igdb = new IGDBClient("3p2ubjeep5tco48ebgolo2o4a1cjek", "7d32ezra4dgof88c1dlkvwkve8g4zb");

        var screenshots = await FetchPage(igdb, limit, offset);
        ProcessRatings(screenshots);

        while (screenshots.Length == limit)
        {
            offset += limit;
            screenshots = await FetchPage(igdb, limit, offset);
            ProcessRatings(screenshots);
        }
    }

    private async Task<ApiScreenshot[]> FetchPage(IGDBClient client, int limit, int offset)
    {
        var query = $"""
                     
                                     fields *;
                                     limit {limit};
                                     offset {offset};
                                     
                     """;

        var apiScreenshots = await client.QueryAsync<ApiScreenshot>(IGDBClient.Endpoints.Screenshots, query);
        return apiScreenshots;
    }

    private async void ProcessRatings(IEnumerable<ApiScreenshot> apiScreenshots)
    {
        var screenshotsToAdd = new List<Screenshot>();
        foreach (var apiScreenshot in apiScreenshots)
        {
            if (apiScreenshot == null) continue;

            var game = _context.Games.FirstOrDefault(game => game.IgdbId == apiScreenshot.Game.Id);
            if (game != null)
            {
                var screenshot = new DomainScreenshot
                {
                    Game = game,
                    GameId = game.Id,
                    Height = apiScreenshot.Height,
                    Width = apiScreenshot.Width,
                    Url = apiScreenshot.Url,
                    ImageId = apiScreenshot.ImageId
                };

                screenshotsToAdd.Add(screenshot);
            }
        }

        await _context.AddRangeAsync(screenshotsToAdd);
        await _context.SaveChangesAsync();
    }
}