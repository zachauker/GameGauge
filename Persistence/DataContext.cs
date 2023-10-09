using Domain.Attributes;
using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Persistence;

public class DataContext : IdentityDbContext<AppUser>

{
    public DataContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var properties = entityType.ClrType.GetProperties()
                .Where(p => p.GetCustomAttributes(typeof(TimestampAttribute), false).Any());

            foreach (var property in properties)
            {
                switch (property.Name)
                {
                    case "CreatedAt":
                        modelBuilder.Entity(entityType.Name)
                            .Property(property.Name)
                            .HasColumnType("timestamp")
                            .IsRequired()
                            .ValueGeneratedOnAdd();
                        break;
                    case "UpdatedAt":
                        modelBuilder.Entity(entityType.Name)
                            .Property(property.Name)
                            .HasColumnType("timestamp")
                            .IsRequired()
                            .ValueGeneratedOnAddOrUpdate();
                        break;
                }
            }
        }
    }

    public DbSet<Game> Games { get; set; }
    public DbSet<Platform> Platforms { get; set; }
    public DbSet<PlatformFamily> PlatformFamilies { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Engine> Engines { get; set; }
    public DbSet<AgeRating> AgeRatings { get; set; }
    public DbSet<ReleaseDate> ReleaseDates { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<GameList> GameLists { get; set; }
}