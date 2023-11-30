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

        const int limit = 500;
        var offset = 0;

        var igdb = new IGDBClient("3p2ubjeep5tco48ebgolo2o4a1cjek", "7d32ezra4dgof88c1dlkvwkve8g4zb");

        var apiGames = await FetchPage(igdb, limit, offset);
        ProcessGames(apiGames);

        while (apiGames.Length == limit)
        {
            offset += limit;
            apiGames = await FetchPage(igdb, limit, offset);
            ProcessGames(apiGames);
        }
    }

    private static async Task<ApiGame[]> FetchPage(IGDBClient client, int limit, int offset)
    {
        var query = $"""
                     
                                     fields name,id,involved_companies.*;
                                     limit {limit};
                                     offset {offset};
                                     
                     """;

        var apiGames = await client.QueryAsync<ApiGame>(IGDBClient.Endpoints.Games, query);
        return apiGames;
    }

    private async void ProcessGames(IEnumerable<ApiGame> apiGames)
    {
        var gameCompaniesToAdd = new List<GameCompany>();

        foreach (var apiGame in apiGames)
        {
            if (apiGame == null || apiGame.InvolvedCompanies == null) continue;

            var game = _context.Games.FirstOrDefault(g => g.IgdbId == apiGame.Id);
            if (game == null) continue;

            // Collect and aggregate roles
            var companyRoles =
                new Dictionary<int, (bool? isDeveloper, bool? isPorter, bool? isPublisher, bool? isSupporter)>();

            foreach (var apiCompany in apiGame.InvolvedCompanies.Values)
            {
                // Skip if company ID is null or not in the int range
                if (!apiCompany.Company.Id.HasValue || apiCompany.Company.Id > int.MaxValue)
                    continue;

                var companyId = (int)apiCompany.Company.Id;

                if (companyRoles.TryGetValue(companyId, out var roles))
                {
                    roles.isDeveloper |= apiCompany.Developer;
                    roles.isPorter |= apiCompany.Porting;
                    roles.isPublisher |= apiCompany.Publisher;
                    roles.isSupporter |= apiCompany.Supporting;
                    companyRoles[companyId] = roles;
                }
                else
                {
                    companyRoles.Add(companyId,
                        (apiCompany.Developer, apiCompany.Porting, apiCompany.Publisher, apiCompany.Supporting));
                }
            }

            // Process each aggregated company role
            foreach (var (companyId, roles) in companyRoles)
            {
                var company = _context.Companies.FirstOrDefault(c => c.IgdbId == companyId);
                if (company == null) continue;

                var existingGameCompany = _context.GameCompanies
                    .AsNoTracking()
                    .FirstOrDefault(gc => gc.GameId == game.Id && gc.CompanyId == company.Id);

                if (existingGameCompany == null)
                {
                    var gameCompany = new GameCompany()
                    {
                        GameId = game.Id,
                        CompanyId = company.Id,
                        IsDeveloper = roles.isDeveloper,
                        IsPorter = roles.isPorter,
                        IsPublisher = roles.isPublisher,
                        IsSupporter = roles.isSupporter
                    };
                    gameCompaniesToAdd.Add(gameCompany);
                }
            }
        }

        await _context.GameCompanies.AddRangeAsync(gameCompaniesToAdd);
        await _context.SaveChangesAsync();
    }
}