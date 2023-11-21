namespace Domain.Entities;

public class GameEngine
{
    public Guid GameId { get; set; }
    public Game Game { get; set; }
    public Guid EngineId { get; set; }
    public Engine Engine { get; set; }
}