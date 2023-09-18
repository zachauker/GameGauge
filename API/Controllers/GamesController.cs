using Application.Games;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers;

public class GamesController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<List<Game>>> GetGames()
    {
        return await Mediator.Send(new List.Query());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Game>> GetGame(Guid id)
    {
        return await Mediator.Send(new Details.Query{Id = id});
    }
}