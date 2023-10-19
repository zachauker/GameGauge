using Domain.Attributes;

namespace Domain.Entities
{
    public class AgeRating
    {
        public Guid Id { get; set; }
        public string? Category { get; set; }
        public long? IgdbId { get; set; }
        public string Synopsis { get; set; }
        [Timestamp] 
        public DateTime CreatedAt { get; set; }
        [Timestamp] 
        public DateTime UpdatedAt { get; set; }
    }
}