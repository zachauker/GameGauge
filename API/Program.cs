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
        await UserSeed.SeedData(context, userManager);
    }

    if (args.Contains("AgeRatingSeed"))
    {
        await AgeRatingSeed.SeedData(context);
    }

    if (args.Contains("CompanySeed"))
    {
        await CompanySeed.SeedData(context);
    }

    if (args.Contains("EngineSeed"))
    {
        await EngineSeed.SeedData(context);
    }

    if (args.Contains("GenreSeed"))
    {
        await GenreSeed.SeedData(context);
    }

    if (args.Contains("PlatformSeed"))
    {
        await PlatformSeed.SeedData(context);
    }

    if (args.Contains("GameSeed"))
    {
        await GameSeed.SeedData(context);
    }

    if (args.Contains("ReleaseDateSeed"))
    {
        await ReleaseDateSeed.SeedData(context);
    }

    if (args.Contains("MediaSeed"))
    {
        await ArtworkSeed.SeedData(context);
        await CoverSeed.SeedData(context);
        await GameVideoSeed.SeedData(context);
    }
}
catch (Exception e)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(e, "Error during migration!");
}

app.Run();