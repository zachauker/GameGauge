namespace Domain.Entities;

public class GameListGame
{
    public Guid GameListId { get; set; }
    public GameList GameList { get; set; }
    public Guid GameId { get; set; }
    public Game Game { get; set; }
    public int Position { get; set; }
    public DateTime DateAdded { get; set; }
}