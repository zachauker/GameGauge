using Domain.Attributes;

namespace Domain.Entities;

public class Platform
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Abbreviation { get; set; }
    public PlatformFamily? PlatformFamily { get; set; }
    public int? Generation { get; set; }
    public string? Summary { get; set; }
    public string Slug { get; set; }
    public string? AlternativeName { get; set; }
    public long? IgdbId { get; set; }
    public ICollection<Game> Games { get; set; }
    [Timestamp] 
    public DateTime CreatedAt { get; set; }
    [Timestamp] 
    public DateTime UpdatedAt { get; set; }
}