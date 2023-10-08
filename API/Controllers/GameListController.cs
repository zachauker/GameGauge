using System.Diagnostics;
using Application.GameLists;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers;

public class GameListController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<List<GameList>>> GetGames()
    {
        return await Mediator.Send(new List.Query());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<GameList>> GetGame(Guid id)
    {
        return await Mediator.Send(new Details.Query{Id = id});
    }

    [HttpPost]
    public async Task<IActionResult> CreateGameList(GameList gameList)
    {
        await Mediator.Send(new Create.Command { GameList = gameList });
        
        return Ok();
    }

    [HttpPut( "{id:guid}")]
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