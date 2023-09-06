using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class DataContext: DbContext
{
    public DataContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Game> Games { get; set; }
    public DbSet<Platform> Platforms { get; set; }
    public DbSet<PlatformFamily> PlatformFamilies { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Engine> Engines { get; set; }
}