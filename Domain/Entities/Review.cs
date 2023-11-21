using Domain.Attributes;

namespace Domain.Entities;

public class Review
{
    public Guid Id { get; set; }
    public Game Game { get; set; }
    public Guid GameId { get; set; }
    public AppUser User { get; set; }
    public int Rating { get; set; }
    public string Description { get; set; }
    [Timestamp] 
    public DateTime CreatedAt { get; set; }
    [Timestamp]
    public DateTime UpdatedAt { get; set; }
}