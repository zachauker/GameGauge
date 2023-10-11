using System.Diagnostics;
using System.Text.Json.Nodes;
using Application.GameLists;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class GameListsController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<List<GameList>>> GetGames()
    {
        return await Mediator.Send(new List.Query());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<GameListDto>> GetGameList(Guid id)
    {
        return await Mediator.Send(new Details.Query { Id = id });
    }

    [HttpPost]
    public async Task<IActionResult> CreateGameList(GameList gameList)
    {
        await Mediator.Send(new Create.Command { GameList = gameList });

        return Ok();
    }

    [HttpPut("{id:guid}/add")]
    public async Task<IActionResult> AddGamesToGameList(Guid id, [FromBody] List<Game> games)
    {
        await Mediator.Send(new AddGames.Command { ListId = id, Games = games });

        return Ok();
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> EditGameList(Guid id, GameList gameList)
    {
        gameList.Id = id;
        await Mediator.Send(new Edit.Command { GameList = gameList });

        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await Mediator.Send(new Delete.Command { Id = id });

        return Ok();
    }
}