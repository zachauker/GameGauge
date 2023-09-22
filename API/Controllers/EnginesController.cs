using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Application.Engines;

namespace API.Controllers;

public class EnginesController : BaseApiController
{
     [HttpGet]
    public async Task<ActionResult<List<Engine>>> GetGames()
    {
        return await Mediator.Send(new List.Query());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Engine>> GetGame(Guid id)
    {
        return await Mediator.Send(new Details.Query{Id = id});
    }
}