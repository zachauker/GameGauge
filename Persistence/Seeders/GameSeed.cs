using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using IGDB;
using Microsoft.EntityFrameworkCore;
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
                         
                                         fields *;
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

                var game = context.Games.AddRangeAsync(myGame);
            }
            
            await context.SaveChangesAsync();
        }
    }
}