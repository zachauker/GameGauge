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
            modelBuilder.Entity<GameListGame>(x => x.HasKey(glg => new { glg.GameListId, glg.GameId }));

            modelBuilder.Entity<GameListGame>()
                .HasOne(glg => glg.GameList)
                .WithMany(gl => gl.ListGames)
                .HasForeignKey(glg => glg.GameListId);

            modelBuilder.Entity<GameListGame>()
                .HasOne(glg => glg.Game)
                .WithMany(g => g.GameLists)
                .HasForeignKey(glg => glg.GameId);

            modelBuilder.Entity<EnginePlatform>(x => x.HasKey(ep => new { ep.EngineId, ep.PlatformId }));

            modelBuilder.Entity<EnginePlatform>()
                .HasOne(ep => ep.Engine)
                .WithMany(e => e.Platforms)
                .HasForeignKey(ep => ep.EngineId);

            modelBuilder.Entity<EnginePlatform>()
                .HasOne(ep => ep.Platform)
                .WithMany(p => p.Engines)
                .HasForeignKey(ep => ep.PlatformId);

            modelBuilder.Entity<GameCompany>(x => x.HasKey(gc => new { gc.GameId, gc.CompanyId }));

            modelBuilder.Entity<GameCompany>()
                .HasOne(gc => gc.Game)
                .WithMany(g => g.InvolvedCompanies)
                .HasForeignKey(gc => gc.GameId);

            modelBuilder.Entity<GameCompany>()
                .HasOne(gc => gc.Company)
                .WithMany(c => c.InvolvedGames)
                .HasForeignKey(gc => gc.CompanyId);

            modelBuilder.Entity<GameEngine>(x => x.HasKey(ge => new { ge.EngineId, ge.GameId }));

            modelBuilder.Entity<GameEngine>()
                .HasOne(ge => ge.Game)
                .WithMany(g => g.Engines)
                .HasForeignKey(ge => ge.GameId);

            modelBuilder.Entity<GameEngine>()
                .HasOne(ge => ge.Engine)
                .WithMany(e => e.Games)
                .HasForeignKey(ge => ge.EngineId);

            modelBuilder.Entity<GameGenre>(x => x.HasKey(gg => new { gg.GenreId, gg.GameId }));

            modelBuilder.Entity<GameGenre>()
                .HasOne(gg => gg.Game)
                .WithMany(g => g.Genres)
                .HasForeignKey(gg => gg.GameId);

            modelBuilder.Entity<GameGenre>()
                .HasOne(gg => gg.Genre)
                .WithMany(g => g.Games)
                .HasForeignKey(gg => gg.GenreId);

            modelBuilder.Entity<GamePlatform>(x => x.HasKey(gp => new { gp.PlatformId, gp.GameId }));

            modelBuilder.Entity<GamePlatform>()
                .HasOne(gp => gp.Game)
                .WithMany(p => p.Platforms)
                .HasForeignKey(gg => gg.GameId);

            modelBuilder.Entity<GamePlatform>()
                .HasOne(gp => gp.Platform)
                .WithMany(p => p.Games)
                .HasForeignKey(gp => gp.PlatformId);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Game)
                .WithMany(g => g.Reviews)
                .HasForeignKey(r => r.GameId);

            modelBuilder.Entity<Game>()
                .HasMany(g => g.Reviews)
                .WithOne(r => r.Game)
                .HasForeignKey(r => r.GameId);

            modelBuilder.Entity<GameAgeRating>(x => x.HasKey(ga => new { ga.GameId, ga.AgeRatingId }));

            modelBuilder.Entity<GameAgeRating>()
                .HasOne(ga => ga.Game)
                .WithMany(g => g.AgeRatings)
                .HasForeignKey(ga => ga.GameId);

            modelBuilder.Entity<GameAgeRating>()
                .HasOne(ga => ga.AgeRating)
                .WithMany(a => a.Games)
                .HasForeignKey(ga => ga.AgeRatingId);

            modelBuilder.Entity<Artwork>()
                .HasOne(a => a.Game)
                .WithMany(g => g.Artworks)
                .HasForeignKey(a => a.GameId);

            modelBuilder.Entity<Cover>()
                .HasOne(c => c.Game)
                .WithMany(g => g.Covers)
                .HasForeignKey(c => c.GameId);

            modelBuilder.Entity<GameVideo>()
                .HasOne(gv => gv.Game)
                .WithMany(g => g.Videos)
                .HasForeignKey(gv => gv.GameId);

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
                            .HasDefaultValueSql("CURRENT_TIMESTAMP")
                            .ValueGeneratedOnAdd();
                        break;
                    case "UpdatedAt":
                        modelBuilder.Entity(entityType.Name)
                            .Property(property.Name)
                            .HasColumnType("timestamp")
                            .IsRequired()
                            .HasDefaultValueSql("CURRENT_TIMESTAMP")
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
    public DbSet<GameListGame> GameListGames { get; set; }
    public DbSet<GameGenre> GameGenres { get; set; }
    public DbSet<GameEngine> GameEngines { get; set; }
    public DbSet<GameCompany> GameCompanies { get; set; }
    public DbSet<GamePlatform> GamePlatforms { get; set; }
    public DbSet<GameAgeRating> GameAgeRatings { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Artwork> Artworks { get; set; }
    public DbSet<Cover> Covers { get; set; }
    public DbSet<GameVideo> GameVideos { get; set; }
    public DbSet<Screenshot> Screenshots { get; set; }
}