namespace Domain.Entities;

public class GameAgeRating
{
    public Guid GameId { get; set; }
    public Game Game { get; set; }
    public Guid AgeRatingId { get; set; }
    public AgeRating AgeRating { get; set; }
}