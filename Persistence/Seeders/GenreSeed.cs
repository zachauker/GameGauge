using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Persistence.Seeders;

using IGDB;
using ApiGenre = IGDB.Models.Genre;
using DomainGenre = Domain.Entities.Genre;

public class GenreSeed
{
    private ILogger<GameCompanySeed> _logger;
    private readonly DataContext _context;

    public GenreSeed(DataContext context, ILogger<GameCompanySeed> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task SeedData()
    {
        if (await _context.Genres.AnyAsync()) return;

        var igdb = new IGDBClient("3p2ubjeep5tco48ebgolo2o4a1cjek", "7d32ezra4dgof88c1dlkvwkve8g4zb");

        var apiGenres = await igdb.QueryAsync<ApiGenre>(IGDBClient.Endpoints.Genres, "fields *; limit 100;");
        foreach (var apiGenre in apiGenres)
        {
            var genre = new DomainGenre
            {
                Name = apiGenre.Name,
                Slug = apiGenre.Slug,
                IgdbId = apiGenre.Id
            };

            await _context.Genres.AddRangeAsync(genre);
        }

        await _context.SaveChangesAsync();
    }
}