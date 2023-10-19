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
    public ICollection<Platform> Platforms { get; set; }
    public ICollection<Genre> Genres { get; set; }
    public ICollection<Engine> Engines { get; set; }
    public ICollection<AgeRating> AgeRatings { get; set; }
    public ICollection<GameListGame> GameLists { get; set; }
    [Timestamp] 
    public DateTime CreatedAt { get; set; }
    [Timestamp] 
    public DateTime UpdatedAt { get; set; }
}