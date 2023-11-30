namespace Application.Covers;

public class CoverDto
{
    public Guid Id { get; set; }
    public Guid? GameId { get; set; }
    public string Url { get; set; }
    public int? Height { get; set; }
    public int? Width { get; set; }
    public string ImageId { get; set; }
}