namespace Domain.Entities;

public class ReleaseDate
{
    public Guid Id { get; set; }
    public Game Game { get; set; }
    public Platform Platform { get; set; }
    public DateOnly Date { get; set; }
}