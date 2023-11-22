namespace Domain.Entities;

public class GameVideo
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string VideoId { get; set; }
    public Guid GameId { get; set; }
    public Game Game { get; set; }
}