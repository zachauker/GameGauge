namespace Application.AgeRatings;

public class AgeRatingDto
{
    public Guid Id { get; set; }
    public string? Category { get; set; }
    public long? IgdbId { get; set; }
    public string Synopsis { get; set; }
}