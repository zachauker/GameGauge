namespace Application.GameVideos;

public class GameVideoDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string VideoId { get; set; }
    public Guid GameId { get; set; }
}