using Domain.Attributes;

namespace Domain.Entities;

public class PlatformFamily
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    public long? IgdbId { get; set; }
    [Timestamp] 
    public DateTime CreatedAt { get; set; }
    [Timestamp] 
    public DateTime UpdatedAt { get; set; }
}