using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Persistence.Seeders;

using ApiEngine = IGDB.Models.GameEngine;
using DomainEngine = Domain.Entities.Engine;
using IGDB;

public class EngineSeed
{
    private ILogger<GameCompanySeed> _logger;
    private readonly DataContext _context;

    public EngineSeed(DataContext context, ILogger<GameCompanySeed> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task SeedData()
    {
        if (await _context.Engines.AnyAsync()) return;

        var igdb = new IGDBClient("3p2ubjeep5tco48ebgolo2o4a1cjek", "7d32ezra4dgof88c1dlkvwkve8g4zb");
        const int limit = 250;
        var offset = 0;

        var engines = await FetchPage(igdb, limit, offset);
        ProcessEngines(engines);

        while (engines.Length == limit)
        {
            offset += limit;
            engines = await FetchPage(igdb, limit, offset);
            ProcessEngines(engines);
        }
    }


    private async Task<ApiEngine[]> FetchPage(IGDBClient client, int limit, int offset)
    {
        var query = $"""
                     
                                     fields *;
                                     limit {limit};
                                     offset {offset};
                                     
                     """;

        var apiEngines = await client.QueryAsync<ApiEngine>(IGDBClient.Endpoints.GameEngines, query);
        return apiEngines;
    }

    private async void ProcessEngines(IEnumerable<ApiEngine> apiEngines)
    {
        foreach (var apiEngine in apiEngines)
        {
            var engine = new DomainEngine
            {
                Name = apiEngine.Name,
                Description = apiEngine.Description,
                Slug = apiEngine.Slug,
                IgdbId = apiEngine.Id
            };


            await _context.Engines.AddRangeAsync(engine);
        }

        await _context.SaveChangesAsync();
    }
}