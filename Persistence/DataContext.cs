using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class DataContext: IdentityDbContext<AppUser>
{
    public DataContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Game> Games { get; set; }
    public DbSet<Platform> Platforms { get; set; }
    public DbSet<PlatformFamily> PlatformFamilies { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Engine> Engines { get; set; }
    public DbSet<AgeRating> AgeRatings { get; set; }
    public DbSet<ReleaseDate> ReleaseDates { get; set; }
    public DbSet<Company> Companies { get; set; }
    
}