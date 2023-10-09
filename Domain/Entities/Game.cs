using System.Runtime.InteropServices.JavaScript;
using Domain.Attributes;

namespace Domain.Entities;

public class Game
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string StoryLine { get; set; }
    public string Slug { get; set; }
    public long? IgdbId { get; set; }
    public DateTimeOffset? ReleaseDate { get; set; }
    public int? Rating { get; set; }
    public int NumberRatings { get; set; }
    public List<Platform> Platforms { get; set; } = new();
    public List<Genre> Genres { get; set; } = new();
    public List<Engine> Engines { get; set; } = new();
    public List<Company> Companies { get; set; } = new();
    public List<AgeRating> AgeRatings { get; set; } = new();
    public List<GameList> GameLists { get; set; } = new();
    [Timestamp] 
    public DateTime CreatedAt { get; set; }
    [Timestamp] 
    public DateTime UpdatedAt { get; set; }
}