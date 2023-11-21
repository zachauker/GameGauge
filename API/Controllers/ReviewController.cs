using System.Diagnostics;
using System.Text.Json.Nodes;
using Application.Reviews;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers;

public class ReviewController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<List<ReviewDto>>> GetGameReviews(Game game)
    {
        return await Mediator.Send(new ListGameReviews.Query {Game = game});
    }

    [HttpGet]
    public async Task<ActionResult<List<ReviewDto>>> GetUserReviews(AppUser user)
    {
        return await Mediator.Send(new ListUserReviews.Query{User = user});
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ReviewDto>> GetReviewDetails(Guid id)
    {
        return await Mediator.Send(new Details.Query { Id = id });
    }

    [HttpPost]
    public async Task<ActionResult> CreateReview(Review review, Guid gameId)
    {
        var newList = await Mediator.Send(new Create.Command { Review = review , GameId = gameId});

        return Ok(newList);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> EditReview(Guid id, Review review)
    {
        review.Id = id;
        await Mediator.Send(new Edit.Command { Review = review });

        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await Mediator.Send(new Delete.Command { Id = id });

        return Ok();
    }
}