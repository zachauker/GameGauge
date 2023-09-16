using Domain.Entities;
using ApiRating = IGDB.Models.AgeRating;
using DomainAgeRating = Domain.Entities.AgeRating;
using IGDB;


namespace Persistence.Seeders;

public static class AgeRatingSeed
{
    public static async Task SeedData(DataContext context)
    {
        if (context.AgeRatings.Any()) return;

        const int limit = 100;
        var offset = 0;

        var igdb = new IGDBClient("3p2ubjeep5tco48ebgolo2o4a1cjek", "9lzmwsx9eai2lkqg1ye5waxovtltrp");

        var ratings = await FetchPage(igdb, limit, offset);
        ProcessRatings(ratings, context);

        while (ratings.Length == limit)
        {
            offset += limit;
            ratings = await FetchPage(igdb, limit, offset);
            ProcessRatings(ratings, context);
        }
    }

    private static async Task<ApiRating[]> FetchPage(IGDBClient client, int limit, int offset)
    {
        var query = $"""
                     
                                     fields *;
                                     limit {limit};
                                     offset {offset};
                                     
                     """;

        var apiRatings = await client.QueryAsync<ApiRating>(IGDBClient.Endpoints.AgeRating, query);
        return apiRatings;
    }

    private static async void ProcessRatings(IEnumerable<ApiRating> apiRatings, DataContext context)
    {
        foreach (var apiRating in apiRatings)
        {
            if (apiRating.Category == null) continue;
            var rating = new DomainAgeRating
            {
                Category = apiRating.Category.Value.ToString(),
                Synopsis = apiRating.Synopsis,
                IgdbId = apiRating.Id
            };
            await context.AgeRatings.AddRangeAsync(rating);
        }
        
        await context.SaveChangesAsync();
    }
}