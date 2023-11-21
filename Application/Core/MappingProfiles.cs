using Application.AgeRatings;
using Application.Companies;
using Application.Engines;
using Application.GameAgeRatings;
using Application.GameCompanies;
using Application.GameEngines;
using Application.GameGenres;
using Application.GameListGames;
using Application.GameLists;
using Application.GamePlatforms;
using Application.Games;
using Application.Reviews;
using Application.Genres;
using Application.Platforms;
using AutoMapper;
using UserProfile = Application.Profiles.Profile;
using Domain.Entities;

namespace Application.Core;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<GameList, GameListDto>()
            .ForMember(d => d.ListGames,
                o => o.MapFrom(s => s.ListGames.Select(lg => lg.Game)));

        CreateMap<Game, GameDto>()
            .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genres.Select(gg => gg.Genre)))
            .ForMember(dest => dest.Companies,
                opt => opt.MapFrom(src => src.InvolvedCompanies.Select(ic => ic.Company)))
            .ForMember(dest => dest.Engines, opt => opt.MapFrom(g => g.Engines.Select(ge => ge.Engine)))
            .ForMember(dest => dest.AgeRatings, opt => opt.MapFrom(src => src.AgeRatings.Select(ga => ga.AgeRating)))
            .ForMember(dest => dest.Platforms, opt => opt.MapFrom(src => src.Platforms.Select(gp => gp.Platform)));
        
        CreateMap<GameListGame, GameListGameDto>()
            .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.Position))
            .ForMember(dest => dest.DateAdded, opt => opt.MapFrom(src => src.DateAdded));

        CreateMap<Genre, GenreDto>();
        CreateMap<GameGenre, GameGenreDto>();

        CreateMap<Platform, PlatformDto>();
        CreateMap<GamePlatform, GamePlatformDto>();

        CreateMap<Engine, EngineDto>();
        CreateMap<GameEngine, GameEngineDto>();

        CreateMap<Company, CompanyDto>();
        CreateMap<GameCompany, GameCompanyDto>();

        CreateMap<AgeRating, AgeRatingDto>();
        CreateMap<GameAgeRating, GameAgeRatingDto>();

        CreateMap<Review, ReviewDto>()
            .ForMember(d => d.UserProfile, o => o.MapFrom(s => s.User))
            .ForMember(d => d.Game, o => o.MapFrom(s => s.Game));

        CreateMap<AppUser, UserProfile>()
            .ForMember(up => up.DisplayName, o => o.MapFrom(a => a.DisplayName))
            .ForMember(up => up.UserName, o => o.MapFrom(a => a.UserName))
            .ForMember(up => up.Bio, o => o.MapFrom(a => a.Bio));
    }
}