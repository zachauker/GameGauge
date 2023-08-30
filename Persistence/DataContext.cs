using Domain;
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
}