using Domain.Entities;

namespace Application.Games;

public class GameDto
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
    public ICollection<GamePlatform> Platforms { get; set; }
    public ICollection<Genre> Genres { get; set; }
    public ICollection<GameEngine> Engines { get; set; }
    public ICollection<GameCompany> Companies { get; set; }
    public ICollection<AgeRating> AgeRatings { get; set; }
    public ICollection<Review> Reviews { get; set; }
}