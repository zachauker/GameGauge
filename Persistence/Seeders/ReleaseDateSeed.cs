using IGDB;
using IGDB.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using DomainReleaseDate = Domain.Entities.ReleaseDate;
using ApiDate = IGDB.Models.ReleaseDate;

namespace Persistence.Seeders
{
    public class ReleaseDateSeed
    {
        private ILogger<GameCompanySeed> _logger;
        private readonly DataContext _context;

        public ReleaseDateSeed(ILogger<GameCompanySeed> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task SeedData()
        {
            if (await _context.ReleaseDates.AnyAsync()) return;

            const int limit = 250;
            var offset = 0;

            var igdb = new IGDBClient("3p2ubjeep5tco48ebgolo2o4a1cjek", "7d32ezra4dgof88c1dlkvwkve8g4zb");
            var dates = await FetchPage(igdb, limit, offset);
            ProcessDates(dates);

            while (dates.Length == limit)
            {
                offset += limit;
                dates = await FetchPage(igdb, limit, offset);
                ProcessDates(dates);
            }
        }

        private async Task<ApiDate[]> FetchPage(IGDBClient client, int limit, int offset)
        {
            var query = $"""
                         
                                         fields *;
                                         limit {limit};
                                         offset {offset};
                                     
                         """;

            var apiDates = await client.QueryAsync<ReleaseDate>(IGDBClient.Endpoints.ReleaseDates, query);
            return apiDates;
        }

        private async void ProcessDates(IEnumerable<ApiDate> apiDates)
        {
            foreach (var apiDate in apiDates)
            {
                var releaseDate = new DomainReleaseDate
                {
                    IgdbId = apiDate.Id,
                    Game = _context.Games.FirstOrDefault(game => game.IgdbId == apiDate.Game.Id),
                    Platform = _context.Platforms.FirstOrDefault(platform => platform.IgdbId == apiDate.Platform.Id),
                    Date = apiDate.Date,
                    ReadableDate = apiDate.Human
                };

                await _context.ReleaseDates.AddRangeAsync(releaseDate);
            }

            await _context.SaveChangesAsync();
        }
    }
}