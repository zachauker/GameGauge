using Domain.Entities;

namespace Application.GamePlatforms;

public class GamePlatformDto
{
    public Guid GameId { get; set; }
    public Game Game { get; set; }
    public Guid PlatformId { get; set; }
    public Platform Platform { get; set; }
}