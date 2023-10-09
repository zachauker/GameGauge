using Domain.Attributes;

namespace Domain.Entities;

public class Engine
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Slug { get; set; }
    public long? IgdbId { get; set; }
    public List<Platform>? Platforms { get; set; }
    public List<Game>? Games { get; set; }
    [Timestamp] 
    public DateTime CreatedAt { get; set; }
    [Timestamp]
    public DateTime UpdatedAt { get; set; }
}