using Domain;
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
        return await _context.Games.ToListAsync();
    }
}