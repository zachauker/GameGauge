namespace Domain.Entities;

public class Screenshot
{
    public Guid Id { get; set; }
    public Guid GameId { get; set; }
    public Game Game { get; set; }
    public string ImageId { get; set; }
    public bool IsAnimated { get; set; }
    public int? Height { get; set; }
    public int? Width { get; set; }
    public string Url { get; set; }
}