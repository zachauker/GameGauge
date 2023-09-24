namespace Domain.Entities;

public class GameList
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public List<Game> Games { get; set; }
}