using Application.Artworks;
using Application.GameVideos;
using Application.Screenshots;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class MediaController : BaseApiController
{
    [HttpGet("screenshots/{id:Guid}")]
    public async Task<ActionResult<List<ScreenshotDto>>> GetGameScreenshots(Guid id)
    {
        return await Mediator.Send(new GameScreenshots.Query { Id = id });
    }

    [HttpGet("artworks/{id:Guid}")]
    public async Task<ActionResult<List<ArtworkDto>>> GetGameArtwork(Guid id)
    {
        return await Mediator.Send(new GetGameArtwork.Query { Id = id });
    }

    [HttpGet("videos/{id:Guid}")]
    public async Task<ActionResult<List<GameVideoDto>>> GetGameVideos(Guid id)
    {
        return await Mediator.Send(new GetGameVideos.Query { Id = id });
    }
}