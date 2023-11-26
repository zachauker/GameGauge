using Domain.Entities;
using IGDB;
using IGDB.Models;
using Microsoft.EntityFrameworkCore;
using ApiEngine = IGDB.Models.GameEngine;
using ApiGame = IGDB.Models.Game;
using DomainEngine = Domain.Entities.Engine;
using GameEngine = Domain.Entities.GameEngine;

namespace Persistence.Seeders;

public class GameEngineSeed
{
    public static async Task SeedData(DataContext context)
    {
        if (context.GameEngines.Any()) return;

        const int limit = 250;
        var offset = 0;

        var igdb = new IGDBClient("3p2ubjeep5tco48ebgolo2o4a1cjek", "7d32ezra4dgof88c1dlkvwkve8g4zb");

        var engines = context.Engines.ToList();

        foreach (var engine in engines)
        {
            var apiGames = await FetchPage(igdb, engine.IgdbId, limit, offset);
            ProcessGames(apiGames, engine, context);

            while (apiGames.Length == limit)
            {
                offset += limit;
                apiGames = await FetchPage(igdb, engine.IgdbId, limit, offset);
                ProcessGames(apiGames, engine, context);
            }

            offset = 0;
        }
    }

    private static async Task<ApiGame[]> FetchPage(IGDBClient client, long? engineId, int limit, int offset)
    {
        var query = $"""
                     
                                     fields name,id;
                                     where game_engines = ({engineId});
                                     limit {limit};
                                     offset {offset};
                                     
                     """;

        var apiGames = await client.QueryAsync<ApiGame>(IGDBClient.Endpoints.Games, query);
        return apiGames;
    }

    private static async void ProcessGames(IEnumerable<ApiGame> apiGames, Engine engine, DataContext context)
    {
        foreach (var apiGame in apiGames)
        {
            if (apiGame == null) continue;

            var game = context.Games.FirstOrDefault(game => game.IgdbId == apiGame.Id);

            if (game != null)
            {
                var existingGameEngine =
                    context.GameEngines.FirstOrDefault(ge => ge.EngineId == engine.Id && ge.GameId == game.Id);

                if (existingGameEngine == null)
                {
                    var gameEngine = new GameEngine
                    {
                        Game = game,
                        GameId = game.Id,
                        Engine = engine,
                        EngineId = engine.Id
                    };

                    await context.GameEngines.AddAsync(gameEngine);
                }
            }
        }

        await context.SaveChangesAsync();
    }
}