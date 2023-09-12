using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers;

public class GamesController : BaseApiController
{
    private readonly DataContext _context;
    public GamesController(DataContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<Game>>> GetGames()
    {
        return await _context.Games.Include(game => game.Platforms).ToListAsync();
    }

    [HttpGet("{slug}")]
    public async Task<ActionResult<Game>> GetGame(string slug)
    {
        return await _context.Games.FirstAsync(game => game.Slug == slug);
    }
}