namespace Domain.Entities;

public class Genre
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    public long? IgdbId { get; set; }
    public List<Game> Games { get; set; }
}