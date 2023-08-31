namespace Domain;

public class PlatformFamily
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }

    public long? IgdbId { get; set; }
}