using Domain.Attributes;

namespace Domain.Entities;

public class GameList
{
    public Guid Id { get; set; }
    public AppUser User { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public ICollection<GameListGame> ListGames { get; set; }
    [Timestamp] 
    public DateTime CreatedAt { get; set; }
    [Timestamp]
    public DateTime UpdatedAt { get; set; }
}