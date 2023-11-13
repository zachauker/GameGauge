using Domain.Entities;

namespace Application.GameGenres;

public class GameGenreDto
{
    public Guid GameId { get; set; }
    public Game Game { get; set; }
    public Guid GenreId { get; set; }
    public Genre Genre { get; set; }
}