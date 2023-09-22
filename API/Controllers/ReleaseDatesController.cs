using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Application.ReleaseDates;

namespace API.Controllers;

public class ReleaseDatesController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<List<ReleaseDate>>> GetGames()
    {
        return await Mediator.Send(new List.Query());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ReleaseDate>> GetGame(Guid id)
    {
        return await Mediator.Send(new Details.Query { Id = id });
    }
}