using Application.AgeRatings;
using Application.Companies;
using Application.Engines;
using Application.GameAgeRatings;
using Application.GameCompanies;
using Application.Genres;
using Application.Platforms;
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
    public ICollection<PlatformDto> Platforms { get; set; }
    public ICollection<GenreDto> Genres { get; set; }
    public ICollection<EngineDto> Engines { get; set; }
    public ICollection<GameCompanyDto> Companies { get; set; }
    public ICollection<AgeRatingDto> AgeRatings { get; set; }
    public ICollection<Cover> Covers { get; set; }
}