using Application.Games;
using Application.Core;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Seeders;

namespace API.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<DataContext>(opt =>
        {
            opt.UseNpgsql(config.GetConnectionString("DefaultConnection"));
        });
        
        // Seeders
        services.AddScoped<GameSeed>();
        services.AddScoped<PlatformSeed>();
        services.AddScoped<GenreSeed>();
        services.AddScoped<EngineSeed>();
        services.AddScoped<CompanySeed>();
        services.AddScoped<ReleaseDateSeed>();
        services.AddScoped<AgeRatingSeed>();
        services.AddScoped<UserSeed>();
        services.AddScoped<GameGenreSeed>();
        services.AddScoped<GameCompanySeed>();
        services.AddScoped<GameAgeRatingSeed>();
        services.AddScoped<GameEngineSeed>();
        services.AddScoped<GamePlatformSeed>();
        services.AddScoped<ArtworkSeed>();
        services.AddScoped<CoverSeed>();
        services.AddScoped<GameVideoSeed>();
        services.AddScoped<ScreenshotSeed>();
        
        services.AddIdentityServices(config);
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddCoreAdmin();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(List.Handler).Assembly));
        services.AddAutoMapper(typeof(MappingProfiles).Assembly);
        services.AddHttpContextAccessor();
        services.AddScoped<IUserAccessor, UserAccessor>();

        return services;
    }
}