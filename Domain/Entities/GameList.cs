using Domain.Attributes;

namespace Domain.Entities;

public class GameList
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public List<Game> Games { get; set; }
    [Timestamp] 
    public DateTime CreatedAt { get; set; }
    [Timestamp]
    public DateTime UpdatedAt { get; set; }
}