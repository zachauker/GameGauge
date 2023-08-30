using System.Runtime.InteropServices.JavaScript;

namespace Domain;

public class Game
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string StoryLine { get; set; }
    public string Slug { get; set; }
    public long? IgdbId { get; set; }
    public DateTimeOffset? ReleaseDate { get; set; }
    public List<Platform> Platforms { get; set; } = new();


}