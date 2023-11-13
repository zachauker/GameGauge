using Domain.Entities;

namespace Application.Companies;

public class CompanyDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }
    public string Description { get; set; }
    public string Slug { get; set; }
    public long? IgdbId { get; set; }
    public Company? Parent { get; set; }
    public string Url { get; set; }
    public ICollection<GameCompany> InvolvedGames { get; set; }
}