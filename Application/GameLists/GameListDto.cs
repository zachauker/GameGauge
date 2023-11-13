using Application.Profiles;
using Domain.Entities;

namespace Application.GameLists;

public class GameListDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Profile UserProfile { get; set; }
    public ICollection<GameListGame> ListGames { get; set; }
}