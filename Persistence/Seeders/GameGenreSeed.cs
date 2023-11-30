using Domain.Entities;
using IGDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ApiGenre = IGDB.Models.Genre;
using ApiGame = IGDB.Models.Game;
using DomainGenre = Domain.Entities.Genre;

namespace Persistence.Seeders;

public class GameGenreSeed
{
    private ILogger<GameGenreSeed> _logger;
    private DataContext _context;

    public GameGenreSeed(ILogger<GameGenreSeed> logger, DataContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task SeedData()
    {
        if (_context.GameGenres.Any()) return;

        const int limit = 250;
        var offset = 0;

        var igdb = new IGDBClient("3p2ubjeep5tco48ebgolo2o4a1cjek", "7d32ezra4dgof88c1dlkvwkve8g4zb");

        var genres = _context.Genres.ToList();

        foreach (var genre in genres)
        {
            var apiGames = await FetchPage(igdb, genre.IgdbId, limit, offset);
            ProcessGames(apiGames, genre);

            while (apiGames.Length == limit)
            {
                offset += limit;
                apiGames = await FetchPage(igdb, genre.IgdbId, limit, offset);
                ProcessGames(apiGames, genre);
            }

            offset = 0;
        }
    }

    private static async Task<ApiGame[]> FetchPage(IGDBClient client, long? genreId, int limit, int offset)
    {
        var query = $"""
                     
                                     fields name,id;
                                     where genres = ({genreId});
                                     limit {limit};
                                     offset {offset};
                                     
                     """;

        return await client.QueryAsync<ApiGame>(IGDBClient.Endpoints.Games, query);
    }

    private async void ProcessGames(IEnumerable<ApiGame> apiGames, Genre genre)
    {
        foreach (var apiGame in apiGames)
        {
            if (apiGame == null) continue;

            var game = _context.Games.FirstOrDefault(game => game.IgdbId == apiGame.Id);

            if (game != null)
            {
                var existingGameGenre = _context.GameGenres
                    .FirstOrDefault(gg => gg.GameId == game.Id && gg.GenreId == genre.Id);

                if (existingGameGenre == null)
                {
                    var gameGenre = new GameGenre()
                    {
                        Game = game,
                        GameId = game.Id,
                        Genre = genre,
                        GenreId = genre.Id
                    };

                    await _context.GameGenres.AddAsync(gameGenre);
                }
            }
        }

        await _context.SaveChangesAsync();
    }
}