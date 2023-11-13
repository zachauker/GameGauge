using Application.Companies;
using Application.Engines;
using Application.GameGenres;
using Application.GameListGames;
using Application.GameLists;
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
            .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genres.Select(gg => gg.Genre)));

        CreateMap<GameListGame, GameListGameDto>()
            .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.Position))
            .ForMember(dest => dest.DateAdded, opt => opt.MapFrom(src => src.DateAdded));

        CreateMap<Genre, GenreDto>()
            .ForMember(d => d.Games, o => o.MapFrom(s => s.Games.Select(g => g.Game)));

        CreateMap<Platform, PlatformDto>()
            .ForMember(dest => dest.Games, opt => opt.MapFrom(p => p.Games.Select(gp => gp.Game)));

        CreateMap<Engine, EngineDto>()
            .ForMember(dest => dest.Games, opt => opt.MapFrom(src => src.Games.Select(ge => ge.Game)));

        CreateMap<Company, CompanyDto>()
            .ForMember(dest => dest.InvolvedGames, opt => opt.MapFrom(src => src.InvolvedGames.Select(ig => ig.Game)));
        
        CreateMap<Review, ReviewDto>()
            .ForMember(d => d.UserProfile, o => o.MapFrom(s => s.User))
            .ForMember(d => d.Game, o => o.MapFrom(s => s.Game));

        CreateMap<AppUser, UserProfile>()
            .ForMember(up => up.DisplayName, o => o.MapFrom(a => a.DisplayName))
            .ForMember(up => up.UserName, o => o.MapFrom(a => a.UserName))
            .ForMember(up => up.Bio, o => o.MapFrom(a => a.Bio));
    }
}