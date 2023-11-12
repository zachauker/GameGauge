using Application.GameListGames;
using Application.GameLists;
using Application.Games;
using Application.Reviews;
using AutoMapper;
using UserProfile = Application.Profiles.Profile;
using Domain.Entities;

namespace Application.Core;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<GameList, GameList>();
        CreateMap<GameList, GameListDto>()
            .ForMember(d => d.ListGames,
                o => o.MapFrom(s => s.ListGames.Select(lg => lg.Game)));

        CreateMap<Game, Game>();
        CreateMap<Game, GameDto>()
            .ForMember(d => d.GameLists, o => o.MapFrom(s => s.GameLists));
        CreateMap<GameListGame, GameListGameDto>();

        CreateMap<Review, Review>();
        CreateMap<Review, ReviewDto>()
            .ForMember(d => d.UserProfile, o => o.MapFrom(s => s.User))
            .ForMember(d => d.Game, o => o.MapFrom(s => s.Game));

        CreateMap<AppUser, UserProfile>()
            .ForMember(up => up.DisplayName, o => o.MapFrom(a => a.DisplayName))
            .ForMember(up => up.UserName, o => o.MapFrom(a => a.UserName))
            .ForMember(up => up.Bio, o => o.MapFrom(a => a.Bio));
    }
}