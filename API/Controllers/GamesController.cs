using Application;
using Application.GamePlatforms;
using Application.Games;
using Application.Platforms;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Details = Application.Games.Details;
using List = Application.Games.List;

namespace API.Controllers;

public class GamesController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<PaginatedResult<GameDto>>> ListGames([FromQuery] List.Query query)
    {
        return await Mediator.Send(query);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<GameDto>> GetGame(Guid id)
    {
        return await Mediator.Send(new Details.Query { Id = id });
    }

    [HttpGet("search")]
    public async Task<ActionResult<PaginatedResult<GameDto>>> SearchGames([FromQuery] Search.Query query)
    {
        return await Mediator.Send(query);
    }
}