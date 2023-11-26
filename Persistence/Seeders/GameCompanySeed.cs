using Domain.Entities;
using IGDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ApiCompany = IGDB.Models.Genre;
using ApiGame = IGDB.Models.Game;
using DomainCompany = Domain.Entities.Company;

namespace Persistence.Seeders;

public class GameCompanySeed
{
    private ILogger<GameCompanySeed> _logger;
    private readonly DataContext _context;

    public GameCompanySeed(ILogger<GameCompanySeed> logger, DataContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task SeedData()
    {
        if (await _context.GameCompanies.AnyAsync()) return;

        const int limit = 250;
        var offset = 0;

        var igdb = new IGDBClient("3p2ubjeep5tco48ebgolo2o4a1cjek", "7d32ezra4dgof88c1dlkvwkve8g4zb");

        var companies = await _context.Companies.AsNoTracking().ToListAsync();

        foreach (var company in companies)
        {
            var apiGames = await FetchPage(igdb, company.IgdbId, limit, offset);
            ProcessGames(apiGames, company);

            while (apiGames.Length == limit)
            {
                offset += limit;
                apiGames = await FetchPage(igdb, company.IgdbId, limit, offset);
                ProcessGames(apiGames, company);
            }

            offset = 0;
        }
    }

    private static async Task<ApiGame[]> FetchPage(IGDBClient client, long? companyId, int limit, int offset)
    {
        var query = $"""
                     
                                     fields name,id,involved_companies.*;
                                     where involved_companies = ({companyId});
                                     limit {limit};
                                     offset {offset};
                                     
                     """;

        var apiGames = await client.QueryAsync<ApiGame>(IGDBClient.Endpoints.Games, query);
        return apiGames;
    }

    private async void ProcessGames(IEnumerable<ApiGame> apiGames, Company company)
    {
        var gameCompaniesToAdd = new List<GameCompany>();
        
        foreach (var apiGame in apiGames)
        {
            if (apiGame == null) continue;

            var game = _context.Games.FirstOrDefault(game => game.IgdbId == apiGame.Id);

            if (game != null)
            {
                var existingGameCompany =
                    _context.GameCompanies.FirstOrDefault(gc => gc.GameId == game.Id && gc.CompanyId == company.Id);

                if (existingGameCompany != null) continue;

                var apiCompany = apiGame.InvolvedCompanies.Values.FirstOrDefault(c => c.Id == company.IgdbId);

                if (apiCompany == null) continue;

                var gameCompany = new GameCompany()
                {
                    GameId = game.Id,
                    CompanyId = company.Id,
                    IsDeveloper = apiCompany.Developer,
                    IsPorter = apiCompany.Porting,
                    IsPublisher = apiCompany.Publisher,
                    IsSupporter = apiCompany.Supporting
                };

                gameCompaniesToAdd.Add(gameCompany);
            }
        }

        await _context.GameCompanies.AddRangeAsync(gameCompaniesToAdd);
        await _context.SaveChangesAsync();
    }
}