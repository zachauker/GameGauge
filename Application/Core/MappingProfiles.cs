using Application.GameListGames;
using Application.GameLists;
using Application.Games;
using AutoMapper;
using Domain.Entities;

namespace Application.Core;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<GameList, GameList>();
        CreateMap<GameList, GameListDto>()
            .ForMember(d => d.ListGames, o => o.MapFrom(s => s.ListGames.Select(lg => lg.Game)));
        CreateMap<Game, Game>();
        CreateMap<Game, GameDto>()
            .ForMember(d => d.GameLists, o => o.MapFrom(s => s.GameLists));
        CreateMap<GameListGame, GameListGameDto>();
    }
}