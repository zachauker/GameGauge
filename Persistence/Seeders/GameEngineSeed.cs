using Domain.Entities;
using IGDB;
using IGDB.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ApiEngine = IGDB.Models.GameEngine;
using ApiGame = IGDB.Models.Game;
using DomainEngine = Domain.Entities.Engine;
using GameEngine = Domain.Entities.GameEngine;

namespace Persistence.Seeders;

public class GameEngineSeed
{
    private ILogger<GameCompanySeed> _logger;
    private readonly DataContext _context;

    public GameEngineSeed(ILogger<GameCompanySeed> logger, DataContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task SeedData()
    {
        if (await _context.GameEngines.AnyAsync()) return;

        const int limit = 250;
        var offset = 0;

        var igdb = new IGDBClient("3p2ubjeep5tco48ebgolo2o4a1cjek", "7d32ezra4dgof88c1dlkvwkve8g4zb");

        var engines = await _context.Engines.ToListAsync();

        foreach (var engine in engines)
        {
            var apiGames = await FetchPage(igdb, engine.IgdbId, limit, offset);
            ProcessGames(apiGames, engine);

            while (apiGames.Length == limit)
            {
                offset += limit;
                apiGames = await FetchPage(igdb, engine.IgdbId, limit, offset);
                ProcessGames(apiGames, engine);
            }

            offset = 0;
        }
    }

    private async Task<ApiGame[]> FetchPage(IGDBClient client, long? engineId, int limit, int offset)
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

    private async void ProcessGames(IEnumerable<ApiGame> apiGames, Engine engine)
    {
        foreach (var apiGame in apiGames)
        {
            if (apiGame == null) continue;

            var game = _context.Games.FirstOrDefault(game => game.IgdbId == apiGame.Id);

            if (game != null)
            {
                var existingGameEngine =
                    _context.GameEngines.FirstOrDefault(ge => ge.EngineId == engine.Id && ge.GameId == game.Id);

                if (existingGameEngine == null)
                {
                    var gameEngine = new GameEngine
                    {
                        Game = game,
                        GameId = game.Id,
                        Engine = engine,
                        EngineId = engine.Id
                    };

                    await _context.GameEngines.AddAsync(gameEngine);
                }
            }
        }

        await _context.SaveChangesAsync();
    }
}