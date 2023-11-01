namespace Domain.Entities;

public class GameCompany
{
    public Guid GameId { get; set; }
    public Game Game { get; set; }
    public Guid CompanyId { get; set; }
    public Company Company { get; set; }
    public bool IsDeveloper { get; set; }
    public bool IsPublisher { get; set; }
    public bool IsSupporter { get; set; }
    public bool IsPorter { get; set; }
}