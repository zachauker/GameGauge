namespace Domain.Entities;

public class Company
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }
    public string Description { get; set; }
    public string Slug { get; set; }
    public Company? Parent { get; set; }
    public List<Game> Developed { get; set; }
    public List<Game> Published { get; set; }
    public string Url { get; set; }
    public DateTimeOffset? FoundedDate { get; set; }
    
}