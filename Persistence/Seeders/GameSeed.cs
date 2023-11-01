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
                         
                                         fields *,platforms.*,genres.*,game_engines.*,age_ratings.*,involved_companies.*;
                                         sort first_release_date desc;
                                         limit {limit};
                                         offset {offset};
                                     
                         """;

            var games = await client.QueryAsync<ApiGame>(IGDBClient.Endpoints.Games, query);
            return games;
        }

        private static void ProcessGames(IEnumerable<ApiGame> apiGames, DataContext context)
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

                var game = context.Games.Add(myGame);

                context.SaveChanges();

                ProcessRelations(apiGame, game.Entity.Id, context);
            }
        }

        private static void ProcessRelations(ApiGame apiGame, Guid gameId, DataContext context)
        {
            var game = context.Games.Include(g => g.Engines)
                .Include(g => g.Platforms)
                .Include(g => g.Genres)
                .Include(g => g.InvolvedCompanies)
                .Include(g => g.AgeRatings)
                .FirstOrDefault(g => g.Id == gameId);

            if (game != null)
            {
                if (apiGame.Platforms != null)
                {
                    foreach (var apiPlatform in apiGame.Platforms.Values)
                    {
                        var matchingPlatform =
                            context.Platforms.FirstOrDefault(platform => platform.IgdbId == apiPlatform.Id);

                        if (matchingPlatform != null)
                        {
                            var gamePlatform = new GamePlatform
                            {
                                GameId = game.Id,
                                Game = game,
                                PlatformId = matchingPlatform.Id,
                                Platform = matchingPlatform
                            };

                            game.Platforms.Add(gamePlatform);
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
                            var gameGenre = new GameGenre
                            {
                                GameId = game.Id,
                                Game = game,
                                GenreId = matchingGenre.Id,
                                Genre = matchingGenre
                            };

                            game.Genres.Add(gameGenre);
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
                            var gameEngine = new GameEngine
                            {
                                GameId = game.Id,
                                Game = game,
                                EngineId = matchingEngine.Id,
                                Engine = matchingEngine
                            };

                            game.Engines.Add(gameEngine);
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
                            game.AgeRatings.Add(matchingRating);
                        }
                    }
                }

                if (apiGame.InvolvedCompanies != null)
                {
                    foreach (var apiCompany in apiGame.InvolvedCompanies.Values)
                    {
                        var matchingCompany =
                            context.Companies.FirstOrDefault(company => company.IgdbId == apiCompany.Id);

                        if (matchingCompany != null)
                        {
                            var gameCompany = new GameCompany
                            {
                                GameId = game.Id,
                                Game = game,
                                CompanyId = matchingCompany.Id,
                                Company = matchingCompany,
                                IsPorter = apiCompany.Porting != null && apiCompany.Porting.Value,
                                IsDeveloper = apiCompany.Developer.Value,
                                IsPublisher = apiCompany.Publisher != null && apiCompany.Publisher.Value,
                                IsSupporter = apiCompany.Supporting.Value
                            };
                            
                            game.InvolvedCompanies.Add(gameCompany);
                        }
                    }
                }

                context.SaveChanges();
            }
        }
    }
}