using Domain.Entities;
using ApiRating = IGDB.Models.AgeRating;
using DomainAgeRating = Domain.Entities.AgeRating;
using IGDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace Persistence.Seeders;

public class AgeRatingSeed
{
    private ILogger<GameCompanySeed> _logger;
    private readonly DataContext _context;

    public AgeRatingSeed(ILogger<GameCompanySeed> logger, DataContext context)
    {
        _logger = logger;
        _context = context;
    }
    
    public async Task SeedData()
    {
        if (await _context.AgeRatings.AnyAsync()) return;

        const int limit = 250;
        var offset = 0;

        var igdb = new IGDBClient("3p2ubjeep5tco48ebgolo2o4a1cjek", "7d32ezra4dgof88c1dlkvwkve8g4zb");

        var ratings = await FetchPage(igdb, limit, offset);
        ProcessRatings(ratings);

        while (ratings.Length == limit)
        {
            offset += limit;
            ratings = await FetchPage(igdb, limit, offset);
            ProcessRatings(ratings);
        }
    }

    private async Task<ApiRating[]> FetchPage(IGDBClient client, int limit, int offset)
    {
        var query = $"""
                     
                                     fields *;
                                     limit {limit};
                                     offset {offset};
                                     
                     """;

        var apiRatings = await client.QueryAsync<ApiRating>(IGDBClient.Endpoints.AgeRating, query);
        return apiRatings;
    }

    private async void ProcessRatings(IEnumerable<ApiRating> apiRatings)
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
            await _context.AgeRatings.AddRangeAsync(rating);
        }

        await _context.SaveChangesAsync();
    }
}