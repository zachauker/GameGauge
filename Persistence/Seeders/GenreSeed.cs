namespace Persistence.Seeders;
using IGDB;
using ApiGenre = IGDB.Models.Genre;
using DomainGenre = Domain.Entities.Genre;

public class GenreSeed
{
    public static async Task SeedData(DataContext context)
    {
        if (context.Genres.Any()) return;
        
        var igdb = new IGDBClient("3p2ubjeep5tco48ebgolo2o4a1cjek", "9lzmwsx9eai2lkqg1ye5waxovtltrp");

        var apiGenres = await igdb.QueryAsync<ApiGenre>(IGDBClient.Endpoints.Genres, "fields *; limit 100;");
        foreach (var apiGenre in apiGenres)
        {
            var genre = new DomainGenre
            {
                Name = apiGenre.Name,
                Slug = apiGenre.Slug,
                IgdbId = apiGenre.Id
            };
            
            await context.Genres.AddRangeAsync(genre);
        }

        await context.SaveChangesAsync();
    }
}