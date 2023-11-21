using Domain.Entities;

namespace Application.GameAgeRatings;

public class GameAgeRatingDto
{
    public Guid GameId { get; set; }
    public Game Game { get; set; }
    public Guid AgeRatingId { get; set; }
    public AgeRating AgeRating { get; set; }
}