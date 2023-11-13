using Domain.Entities;

namespace Application.Genres;

public class GenreDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    public long? IgdbId { get; set; }
    public ICollection<Game> Games { get; set; }
}