using Application.AgeRatings;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class AgeRatingsController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<List<AgeRating>>> GetAgeRatings()
    {
        return await Mediator.Send(new List.Query());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<AgeRating>> GetAgeRatings(Guid id)
    {
        return await Mediator.Send(new Details.Query { Id = id });
    }
    
}