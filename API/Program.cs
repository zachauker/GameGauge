using System.Runtime.CompilerServices;
using API.Extensions;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Seeders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddApplicationServices(builder.Configuration);


var myAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:3000")
                .WithMethods("GET", "POST", "PUT", "DELETE")
                .WithHeaders("Content-Type");
            ;
        });
});
var app = builder.Build();

app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapDefaultControllerRoute();
app.UseCors(myAllowSpecificOrigins);

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    var context = services.GetRequiredService<DataContext>();
    await context.Database.MigrateAsync();
    var userManager = services.GetRequiredService<UserManager<AppUser>>();

    if (args.Contains("UserSeed"))
    {
        var userSeed = services.GetRequiredService<UserSeed>();
        await userSeed.SeedData(context, userManager);
    }

    if (args.Contains("AgeRatingSeed"))
    {
        var ageRatingSeed = services.GetRequiredService<AgeRatingSeed>();
        await ageRatingSeed.SeedData();
    }

    if (args.Contains("CompanySeed"))
    {
        var companySeed = services.GetRequiredService<CompanySeed>();
        await companySeed.SeedData();
    }

    if (args.Contains("EngineSeed"))
    {
        var engineSeed = services.GetRequiredService<EngineSeed>();
        await engineSeed.SeedData();
    }

    if (args.Contains("GenreSeed"))
    {
        var genreSeed = services.GetRequiredService<GenreSeed>();
        await genreSeed.SeedData();
    }

    if (args.Contains("PlatformSeed"))
    {
        var platformSeed = services.GetRequiredService<PlatformSeed>();
        await platformSeed.SeedData();
    }

    if (args.Contains("GameSeed"))
    {
        var gameSeed = services.GetRequiredService<GameSeed>();
        await gameSeed.SeedData();
    }

    if (args.Contains("ReleaseDateSeed"))
    {
        var releaseDateSeed = services.GetRequiredService<ReleaseDateSeed>();
        await releaseDateSeed.SeedData();
    }

    if (args.Contains("MediaSeed"))
    {
        var artworkSeed = services.GetRequiredService<ArtworkSeed>();
        await artworkSeed.SeedData();

        var coverSeed = services.GetRequiredService<CoverSeed>();
        await coverSeed.SeedData();

        var gameVideoSeed = services.GetRequiredService<GameVideoSeed>();
        await gameVideoSeed.SeedData();
    }
    
    if (args.Contains("GameRelationSeed"))
    {
        var gameCompanySeeder = services.GetRequiredService<GameCompanySeed>();
        await gameCompanySeeder.SeedData();
        
        var gameGenreSeeder = services.GetRequiredService<GameGenreSeed>();
        await gameGenreSeeder.SeedData();

        var gameAgeRatingSeed = services.GetRequiredService<GameAgeRatingSeed>();
        await gameAgeRatingSeed.SeedData();

        var gameEngineSeed = services.GetRequiredService<GameEngineSeed>();
        await gameEngineSeed.SeedData();

        var gamePlatformSeed = services.GetRequiredService<GamePlatformSeed>();
        await gamePlatformSeed.SeedData();
    }
}
catch (Exception e)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(e, "Error during migration!");
}

app.Run();