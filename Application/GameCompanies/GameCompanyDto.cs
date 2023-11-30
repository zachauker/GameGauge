using Domain.Entities;

namespace Application.GameCompanies;

public class GameCompanyDto
{
    public Guid GameId { get; set; }
    public Guid CompanyId { get; set; }
    public Company Company { get; set; }
    public bool IsDeveloper { get; set; }
    public bool IsPublisher { get; set; }
    public bool IsSupporter { get; set; }
    public bool IsPorter { get; set; }
}