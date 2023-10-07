namespace Domain.Entities;

public class ReleaseDate : EntityBase
{
    public Guid Id { get; set; }
    public Game Game { get; set; }
    public Platform Platform { get; set; }
    public long? IgdbId { get; set; }
    public DateTimeOffset? Date { get; set; }
    public string ReadableDate { get; set; }
}