using Domain.Entities;
using IGDB;
using Microsoft.EntityFrameworkCore;
using ApiCompany = IGDB.Models.Genre;
using ApiGame = IGDB.Models.Game;
using DomainCompany = Domain.Entities.Company;

namespace Persistence.Seeders;

public class GameCompanySeed
{
    public static async Task SeedData(DataContext context)
    {
        if (context.GameCompanies.Any()) return;

        const int limit = 250;
        var offset = 0;

        var igdb = new IGDBClient("3p2ubjeep5tco48ebgolo2o4a1cjek", "7d32ezra4dgof88c1dlkvwkve8g4zb");

        var companies = context.Companies.ToList();

        foreach (var company in companies)
        {
            var apiGames = await FetchPage(igdb, company.IgdbId, limit, offset);
            ProcessGames(apiGames, company, context);

            while (apiGames.Length == limit)
            {
                offset += limit;
                apiGames = await FetchPage(igdb, company.IgdbId, limit, offset);
                ProcessGames(apiGames, company, context);
            }

            offset = 0;
        }
    }

    private static async Task<ApiGame[]> FetchPage(IGDBClient client, long? companyId, int limit, int offset)
    {
        var query = $"""
                     
                                     fields *,involved_companies.*;
                                     where involved_companies = ({companyId});
                                     limit {limit};
                                     offset {offset};
                                     
                     """;

        var apiGames = await client.QueryAsync<ApiGame>(IGDBClient.Endpoints.Games, query);
        return apiGames;
    }

    private static async void ProcessGames(IEnumerable<ApiGame> apiGames, Company company, DataContext context)
    {
        foreach (var apiGame in apiGames)
        {
            if (apiGame == null) continue;

            var game = context.Games.FirstOrDefault(game => game.IgdbId == apiGame.Id);

            var existingGameCompany =
                context.GameCompanies.FirstOrDefault(gc => gc.GameId == game.Id && gc.CompanyId == company.Id);

            if (game != null)
            {
                if (existingGameCompany == null)
                {
                    var gameCompany = new GameCompany
                    {
                        Game = game,
                        GameId = game.Id,
                        Company = company,
                        CompanyId = company.Id,
                        IsDeveloper = apiGame.InvolvedCompanies.Values.FirstOrDefault(c => c.Id == company.IgdbId)
                            ?.Developer,
                        IsPorter =
                            apiGame.InvolvedCompanies.Values.FirstOrDefault(c => c.Id == company.IgdbId)?.Porting,
                        IsPublisher = apiGame.InvolvedCompanies.Values.FirstOrDefault(c => c.Id == company.IgdbId)
                            ?.Publisher,
                        IsSupporter = apiGame.InvolvedCompanies.Values.FirstOrDefault(c => c.Id == company.IgdbId)
                            ?.Supporting
                    };

                    await context.GameCompanies.AddRangeAsync(gameCompany);
                }
            }
        }

        await context.SaveChangesAsync();
    }
}