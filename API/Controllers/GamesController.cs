using Application;
using Application.Games;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class GamesController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<List<Game>>> GetGames()
    {
        return await Mediator.Send(new List.Query());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Game>> GetGame(Guid id)
    {
        return await Mediator.Send(new Details.Query { Id = id });
    }

    [HttpGet("search")]
    public async Task<ActionResult<PaginatedResult<GameDto>>> SearchGames([FromQuery] Search.Query query)
    {
        return await Mediator.Send(query);
    }
}