using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using IGDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ApiGame = IGDB.Models.Game;
using DomainGame = Domain.Entities.Game;

namespace Persistence.Seeders
{
    public class GameSeed
    {
        private ILogger<GameCompanySeed> _logger;
        private readonly DataContext _context;

        public GameSeed(DataContext context, ILogger<GameCompanySeed> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task SeedData()
        {
            if (_context.Games.Any()) return;

            const int limit = 250;
            var offset = 0;

            // var igdb = new IGDBClient(
            //     Environment.GetEnvironmentVariable("IGDB_CLIENT_ID"),
            //     Environment.GetEnvironmentVariable("IGDB_CLIENT_SECRET"));
            var igdb = new IGDBClient("3p2ubjeep5tco48ebgolo2o4a1cjek", "7d32ezra4dgof88c1dlkvwkve8g4zb");

            var games = await FetchPage(igdb, limit, offset);
            ProcessGames(games);

            while (games.Length == limit)
            {
                offset += limit;
                games = await FetchPage(igdb, limit, offset);
                ProcessGames(games);
            }
        }

        private async Task<ApiGame[]> FetchPage(IGDBClient client, int limit, int offset)
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

        private async void ProcessGames(IEnumerable<ApiGame> apiGames)
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

                var game = _context.Games.AddRangeAsync(myGame);
            }

            await _context.SaveChangesAsync();
        }
    }
}