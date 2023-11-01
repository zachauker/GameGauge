namespace Domain.Entities;

public class EnginePlatform
{
    public Guid EngineId { get; set; }
    public Engine Engine { get; set; }
    public Guid PlatformId { get; set; }
    public Platform Platform { get; set; }
}