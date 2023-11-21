using Application.Platforms;
using Domain.Entities;

namespace Application.Engines;

public class EngineDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Slug { get; set; }
    public long? IgdbId { get; set; }
}