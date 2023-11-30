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
    public double CriticScore { get; set; }
    public int NumberCriticScores { get; set; }
    public ICollection<GamePlatform> Platforms { get; set; }
    public ICollection<GameGenre> Genres { get; set; }
    public ICollection<GameEngine> Engines { get; set; }
    public ICollection<GameAgeRating> AgeRatings { get; set; }
    public ICollection<GameListGame> GameLists { get; set; }
    public ICollection<GameCompany> InvolvedCompanies { get; set; }
    public ICollection<Review> Reviews { get; set; }
    public ICollection<Artwork> Artworks { get; set; }
    public ICollection<Cover> Covers { get; set; }
    public ICollection<GameVideo> Videos { get; set; }
    public ICollection<Screenshot> Screenshots { get; set; }
    [Timestamp] 
    public DateTime CreatedAt { get; set; }
    [Timestamp] 
    public DateTime UpdatedAt { get; set; }
}