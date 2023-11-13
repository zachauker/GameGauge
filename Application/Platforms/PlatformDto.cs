using Domain.Entities;

namespace Application.Platforms;

public class PlatformDto
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
    public ICollection<GamePlatform> Games { get; set; }
    public ICollection<EnginePlatform> Engines { get; set; }
}