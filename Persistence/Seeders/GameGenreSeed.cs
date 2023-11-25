using Domain.Entities;
using IGDB;
using Microsoft.EntityFrameworkCore;
using ApiGenre = IGDB.Models.Genre;
using ApiGame = IGDB.Models.Game;
using DomainGenre = Domain.Entities.Genre;

namespace Persistence.Seeders;

public class GameGenreSeed
{
    public static async Task SeedData(DataContext context)
    {
        if (context.GameGenres.Any()) return;

        const int limit = 250;
        var offset = 0;

        var igdb = new IGDBClient("3p2ubjeep5tco48ebgolo2o4a1cjek", "7d32ezra4dgof88c1dlkvwkve8g4zb");

        var genres = context.Genres.ToList();

        foreach (var genre in genres)
        {
            var apiGames = await FetchPage(igdb, genre.IgdbId, limit, offset);
            ProcessGames(apiGames, genre, context);

            while (apiGames.Length == limit)
            {
                offset += limit;
                apiGames = await FetchPage(igdb, genre.IgdbId, limit, offset);
                ProcessGames(apiGames, genre, context);
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

        var apiGames = await client.QueryAsync<ApiGame>(IGDBClient.Endpoints.Games, query);
        return apiGames;
    }

    private static async void ProcessGames(IEnumerable<ApiGame> apiGames, Genre genre, DataContext context)
    {
        foreach (var apiGame in apiGames)
        {
            if (apiGame == null) continue;

            var game = context.Games.FirstOrDefault(game => game.IgdbId == apiGame.Id);

            if (game != null)
            {
                var existingGameGenre = context.GameGenres
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

                    await context.GameGenres.AddAsync(gameGenre);
                }
            }
        }
        
        await context.SaveChangesAsync();
    }
}