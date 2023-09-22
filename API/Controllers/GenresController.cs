using Application.Genres;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace API.Controllers;

public class GenresController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<List<Genre>>> GetGenres()
    {
        return await Mediator.Send(new List.Query());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Genre>> GetGenre(Guid id)
    {
        return await Mediator.Send(new Details.Query { Id = id });
    }
}