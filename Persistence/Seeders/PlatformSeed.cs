using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IGDB;
using IGDB.Models;
using ApiPlatform = IGDB.Models.Platform;
using ApiPlatformFamily = IGDB.Models.PlatformFamily;
using DomainPlatform = Domain.Platform;
using DomainPlatformFamily = Domain.PlatformFamily;

namespace Persistence.Seeders
{
    public class PlatformSeed
    {
        public static async Task SeedData(DataContext context)
        {
            if (context.Platforms.Any()) return;
            
            // var igdb = new IGDBClient(
            //     Environment.GetEnvironmentVariable("IGDB_CLIENT_ID"),
            //     Environment.GetEnvironmentVariable("IGDB_CLIENT_SECRET"));

            var igdb = new IGDBClient("3p2ubjeep5tco48ebgolo2o4a1cjek", "9lzmwsx9eai2lkqg1ye5waxovtltrp");

            var apiFamilies = await igdb.QueryAsync<ApiPlatformFamily>(IGDBClient.Endpoints.PlatformFamilies,"fields *; limit 100;");
            foreach (var apiFamily in apiFamilies)
            {
                var family = new DomainPlatformFamily
                {
                    Name = apiFamily.Name,
                    Slug = apiFamily.Slug,
                    IgdbId = apiFamily.Id
                };
                await context.PlatformFamilies.AddRangeAsync(family);
            }
            
            await context.SaveChangesAsync();
            
            // Seed API platforms 
            var apiPlatforms = await igdb.QueryAsync<ApiPlatform>( IGDBClient.Endpoints.Platforms, "fields *,platform_family.*; limit 500;");
            foreach (var apiPlatform in apiPlatforms)
            {
                if (apiPlatform.Id == null) continue;
                
                DomainPlatformFamily platformFamily = null;
                if (apiPlatform.PlatformFamily != null)
                {
                    platformFamily =
                    context.PlatformFamilies.FirstOrDefault(family =>
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
                    PlatformFamily =  platformFamily ?? null 
                };
                await context.Platforms.AddRangeAsync(platform);
            }

            await context.SaveChangesAsync();

        }
        
    }
}