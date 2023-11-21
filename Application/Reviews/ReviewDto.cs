using Application.Profiles;
using Domain.Entities;

namespace Application.Reviews;

public class ReviewDto
{
    public Guid Id { get; set; }
    public Game Game { get; set; }
    public Profile UserProfile { get; set; }
    public int Rating { get; set; }
    public string Description { get; set; }
}