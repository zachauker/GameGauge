using IGDB;
using IGDB.Models;
using DomainReleaseDate = Domain.Entities.ReleaseDate;
using ApiDate = IGDB.Models.ReleaseDate;

namespace Persistence.Seeders
{
    public class ReleaseDateSeed
    {
        public static async Task SeedData(DataContext context)
        {
            if (context.ReleaseDates.Any()) return;

            const int limit = 250;
            var offset = 0;

            var igdb = new IGDBClient("3p2ubjeep5tco48ebgolo2o4a1cjek", "9lzmwsx9eai2lkqg1ye5waxovtltrp");
            var dates = await FetchPage(igdb, limit, offset);
            ProcessDates(dates, context);

            while (dates.Length == limit)
            {
                offset += limit;
                dates = await FetchPage(igdb, limit, offset);
                ProcessDates(dates, context);
            }
        }

        private static async Task<ApiDate[]> FetchPage(IGDBClient client, int limit, int offset)
        {
            var query = $"""
                         
                                         fields *;
                                         limit {limit};
                                         offset {offset};
                                     
                         """;

            var apiDates = await client.QueryAsync<ReleaseDate>(IGDBClient.Endpoints.ReleaseDates, query);
            return apiDates;
        }

        private static async void ProcessDates(IEnumerable<ApiDate> apiDates, DataContext context)
        {
            foreach (var apiDate in apiDates)
            {
                var releaseDate = new DomainReleaseDate
                {
                    IgdbId = apiDate.Id,
                    Game = context.Games.FirstOrDefault(game => game.IgdbId == apiDate.Game.Id),
                    Platform = context.Platforms.FirstOrDefault(platform => platform.IgdbId == apiDate.Platform.Id),
                    Date = apiDate.Date,
                    ReadableDate = apiDate.Human
                };

                await context.ReleaseDates.AddRangeAsync(releaseDate);
            }

            await context.SaveChangesAsync();
        }
    }
}