using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using Application.Platforms;

namespace API.Controllers;

public class PlatformsController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<List<Platform>>> GetPlatforms()
    {
        return await Mediator.Send(new List.Query());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Platform>> GetPlatform(Guid id)
    {
        return await Mediator.Send(new Details.Query{Id = id});
    }
}