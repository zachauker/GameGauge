using Application.PlatformFamilies;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace API.Controllers;

public class PlatformFamiliesController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<List<PlatformFamily>>> GetPlatformFamilies()
    {
        return await Mediator.Send(new List.Query());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<PlatformFamily>> GetPlatformFamily(Guid id)
    {
        return await Mediator.Send(new Details.Query { Id = id });
    }
}