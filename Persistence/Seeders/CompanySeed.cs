using IGDB;
using DomainCompany = Domain.Entities.Company;
using ApiCompany = IGDB.Models.Company;
namespace Persistence.Seeders;

public class CompanySeed
{
    public static async Task SeedData(DataContext context)
    {
        if (context.Companies.Any()) return;
        
        var igdb = new IGDBClient("3p2ubjeep5tco48ebgolo2o4a1cjek", "7d32ezra4dgof88c1dlkvwkve8g4zb");

        var apiCompanies = await igdb.QueryAsync<ApiCompany>(IGDBClient.Endpoints.Companies, "fields *; limit 250;");
        foreach (var apiCompany in apiCompanies)
        {
            var company = new DomainCompany
            {
                Name = apiCompany.Name,
                Slug = apiCompany.Slug,
                Description = apiCompany.Description,
                IgdbId = apiCompany.Id
            };
            
            await context.Companies.AddRangeAsync(company);
        }

        await context.SaveChangesAsync();
    }
}