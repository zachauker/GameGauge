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
        const int limit = 250;
        var offset = 0;

        var companies = await FetchPage(igdb, limit, offset);
        ProcessCompanies(companies, context);

        while (companies.Length == limit)
        {
            offset += limit;
            companies = await FetchPage(igdb, limit, offset);
            ProcessCompanies(companies, context);
        }
    }

    private static async Task<ApiCompany[]> FetchPage(IGDBClient client, int limit, int offset)
    {
        var query = $"""
                     
                                     fields *;
                                     limit {limit};
                                     offset {offset};
                                     
                     """;

        var apiCompanies = await client.QueryAsync<ApiCompany>(IGDBClient.Endpoints.Companies, query);
        return apiCompanies;
    }

    private static async void ProcessCompanies(IEnumerable<ApiCompany> apiCompanies, DataContext context)
    {
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