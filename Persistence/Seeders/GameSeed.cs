using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IGDB;
using ApiGame = IGDB.Models.Game;
using DomainGame = Domain.Entities.Game;

namespace Persistence.Seeders
{
    public class GameSeed
    {
        public static async Task SeedData(DataContext context)
        {
            if (context.Games.Any()) return;

            const int limit = 250;
            var offset = 0;

            // var igdb = new IGDBClient(
            //     Environment.GetEnvironmentVariable("IGDB_CLIENT_ID"),
            //     Environment.GetEnvironmentVariable("IGDB_CLIENT_SECRET"));
            var igdb = new IGDBClient("3p2ubjeep5tco48ebgolo2o4a1cjek", "7d32ezra4dgof88c1dlkvwkve8g4zb");

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
                         
                                         fields *,platforms.*,genres.*,game_engines.*,age_ratings.*,involved_companies.*; 
                                         sort first_release_date desc;
                                         limit {limit};
                                         offset {offset};
                                     
                         """;

            var games = await client.QueryAsync<ApiGame>(IGDBClient.Endpoints.Games, query);
            return games;
        }

        private static async void ProcessGames(IEnumerable<ApiGame> apiGames, DataContext context)
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
                
                if (apiGame.Platforms != null)
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

                if (apiGame.Genres != null)
                {
                    foreach (var apiGenre in apiGame.Genres.Values)
                    {
                        var matchingGenre = context.Genres.FirstOrDefault(genre => genre.IgdbId == apiGenre.Id);

                        if (matchingGenre != null)
                        {
                            myGame.Genres.Add(matchingGenre);
                        }
                    }
                }

                if (apiGame.GameEngines != null)
                {
                    foreach (var apiEngine in apiGame.GameEngines.Values)
                    {
                        var matchingEngine = context.Engines.FirstOrDefault(engine => engine.IgdbId == apiEngine.Id);

                        if (matchingEngine != null)
                        {
                            myGame.Engines.Add(matchingEngine);
                        }
                    }
                }

                if (apiGame.AgeRatings != null)
                {
                    foreach (var apiRating in apiGame.AgeRatings.Values)
                    {
                        var matchingRating = context.AgeRatings.FirstOrDefault(rating => rating.IgdbId == apiRating.Id);

                        if (matchingRating != null)
                        {
                            myGame.AgeRatings.Add(matchingRating);
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