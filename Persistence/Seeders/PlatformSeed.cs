using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IGDB;
using IGDB.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ApiPlatform = IGDB.Models.Platform;
using ApiPlatformFamily = IGDB.Models.PlatformFamily;
using DomainPlatform = Domain.Entities.Platform;
using DomainPlatformFamily = Domain.Entities.PlatformFamily;

namespace Persistence.Seeders
{
    public class PlatformSeed
    {
        private ILogger<GameCompanySeed> _logger;
        private readonly DataContext _context;

        public PlatformSeed(ILogger<GameCompanySeed> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task SeedData()
        {
            if (await _context.Platforms.AnyAsync()) return;

            // var igdb = new IGDBClient(
            //     Environment.GetEnvironmentVariable("IGDB_CLIENT_ID"),
            //     Environment.GetEnvironmentVariable("IGDB_CLIENT_SECRET"));

            var igdb = new IGDBClient("3p2ubjeep5tco48ebgolo2o4a1cjek", "7d32ezra4dgof88c1dlkvwkve8g4zb");

            var apiFamilies =
                await igdb.QueryAsync<ApiPlatformFamily>(IGDBClient.Endpoints.PlatformFamilies, "fields *; limit 100;");
            foreach (var apiFamily in apiFamilies)
            {
                var family = new DomainPlatformFamily
                {
                    Name = apiFamily.Name,
                    Slug = apiFamily.Slug,
                    IgdbId = apiFamily.Id
                };
                await _context.PlatformFamilies.AddRangeAsync(family);
            }

            await _context.SaveChangesAsync();

            // Seed API platforms 
            var apiPlatforms = await igdb.QueryAsync<ApiPlatform>(IGDBClient.Endpoints.Platforms,
                "fields *,platform_family.*; limit 500;");
            foreach (var apiPlatform in apiPlatforms)
            {
                if (apiPlatform.Id == null) continue;

                DomainPlatformFamily platformFamily = null;
                if (apiPlatform.PlatformFamily != null)
                {
                    platformFamily =
                        _context.PlatformFamilies.FirstOrDefault(family =>
                            family.IgdbId == apiPlatform.PlatformFamily.Value.Id);
                }

                var platform = new DomainPlatform
                {
                    Name = apiPlatform.Name,
                    Abbreviation = apiPlatform.Abbreviation,
                    Slug = apiPlatform.Slug,
                    Summary = apiPlatform.Summary,
                    AlternativeName = apiPlatform.AlternativeName,
                    IgdbId = apiPlatform.Id,
                    Generation = apiPlatform.Generation,
                    PlatformFamily = platformFamily ?? null
                };
                await _context.Platforms.AddRangeAsync(platform);
            }

            await _context.SaveChangesAsync();
        }
    }
}