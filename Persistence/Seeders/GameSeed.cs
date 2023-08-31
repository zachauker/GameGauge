using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IGDB;
using IGDB.Models;
using ApiGame = IGDB.Models.Game;
using DomainGame = Domain.Game;

namespace Persistence.Seeders
{
    public class GameSeed
    {
        public static async Task SeedData(DataContext context)
        {
            if (context.Games.Any()) return;

            const int limit = 100;
            var offset = 0;

            // var igdb = new IGDBClient(
            //     Environment.GetEnvironmentVariable("IGDB_CLIENT_ID"),
            //     Environment.GetEnvironmentVariable("IGDB_CLIENT_SECRET"));

            var igdb = new IGDBClient("3p2ubjeep5tco48ebgolo2o4a1cjek", "9lzmwsx9eai2lkqg1ye5waxovtltrp");


            var games = await FetchPage(igdb, limit, offset);
            ProcessGames(games, context);


            while (games.Length == limit)
            {
                offset += limit;
                games = await FetchPage(igdb, limit, offset);
                ProcessGames(games, context);
            }
        }

        private static async Task<ApiGame[]> FetchPage(IGDBClient client, int limit, int offset)
        {
            var query = $"""
                         
                                         fields *,platforms.*; 
                                         sort first_release_date asc;
                                         limit {limit};
                                         offset {offset};
                                     
                         """;

            var games = await client.QueryAsync<ApiGame>(IGDBClient.Endpoints.Games, query);
            return games;
        }

        private static async void ProcessGames(ApiGame[] apiGames, DataContext context)
        {
            foreach (var apiGame in apiGames)
            {
                // Create a new Game entity and manually fill in the fields
                var myGame = new DomainGame
                {
                    Title = apiGame.Name,
                    IgdbId = apiGame.Id,
                    Description = apiGame.Summary,
                    Slug = apiGame.Slug,
                    StoryLine = apiGame.Storyline,
                    ReleaseDate = apiGame.FirstReleaseDate,
                };

                if (apiGame.Platforms.Values.Length > 0)
                {
                    foreach (var apiPlatform in apiGame.Platforms.Values)
                    {
                        var matchingPlatform =
                            context.Platforms.FirstOrDefault(platform => platform.IgdbId == apiPlatform.Id);

                        if (matchingPlatform != null)
                        {
                            myGame.Platforms.Add(matchingPlatform);
                        }
                    }
                }

                await context.Games.AddRangeAsync(myGame);

                // Now you have a custom Game entity with data from the API response
                Console.WriteLine($"Custom Game Name: {myGame.Title}");
            }

            await context.SaveChangesAsync();
        }
    }
}