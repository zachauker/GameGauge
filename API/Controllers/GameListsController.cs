using System.Diagnostics;
using System.Text.Json.Nodes;
using Application.GameLists;
using Application.Games;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Details = Application.GameLists.Details;
using List = Application.GameLists.List;

namespace API.Controllers;

public class GameListsController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<List<GameList>>> GetGameLists()
    {
        return await Mediator.Send(new List.Query());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<GameListDto>> GetGameListDetails(Guid id)
    {
        return await Mediator.Send(new Details.Query { Id = id });
    }

    [HttpPost]
    public async Task<ActionResult> CreateGameList(GameList gameList)
    {
        var newList = await Mediator.Send(new Create.Command { GameList = gameList });

        return Ok(newList);
    }

    [HttpPost("{id:guid}/add")]
    public async Task<IActionResult> AddGameToGameList(Guid id, [FromBody] Game game)
    {
        await Mediator.Send(new AddGames.Command { ListId = id, Game = game });

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