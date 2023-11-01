using Domain.Attributes;

namespace Domain.Entities;

public class Genre
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    public long? IgdbId { get; set; }
    public ICollection<GameGenre> Games { get; set; }
    [Timestamp] 
    public DateTime CreatedAt { get; set; }
    [Timestamp]
    public DateTime UpdatedAt { get; set; }
}