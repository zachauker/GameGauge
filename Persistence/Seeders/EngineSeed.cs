namespace Persistence.Seeders;
using ApiEngine = IGDB.Models.GameEngine;
using DomainEngine = Domain.Entities.Engine;
using IGDB;

public class EngineSeed
{
    public static async Task SeedData(DataContext context)
    {
        if (context.Engines.Any()) return;
        
        var igdb = new IGDBClient("3p2ubjeep5tco48ebgolo2o4a1cjek", "9lzmwsx9eai2lkqg1ye5waxovtltrp");

        var apiEngines = await igdb.QueryAsync<ApiEngine>(IGDBClient.Endpoints.GameEngines, "fields *; limit 100;");
        foreach (var apiEngine in apiEngines)
        {
            var engine = new DomainEngine
            {
                Name = apiEngine.Name,
                Slug = apiEngine.Slug,
                Description = apiEngine.Description,
                IgdbId = apiEngine.Id
            };
            
            await context.Engines.AddRangeAsync(engine);
        }

        await context.SaveChangesAsync();
    }
}