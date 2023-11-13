using System.Text.Json.Nodes;
using Application.GameListGames;
using Application.Games;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.GameLists;

public class AddGames
{
    public class Command : IRequest
    {
        public Guid ListId { get; set; }
        public Game Game { set; get; }
    }

    public class Handler : IRequestHandler<Command>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public Handler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var gameList = await _context.GameLists.ProjectTo<GameListDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(gl => gl.Id == request.ListId,
                    cancellationToken: cancellationToken);

            if (gameList != null)
            {
                if (request.Game != null)
                {
                    var maxPosition = 0;
                    if (gameList.ListGames.Count > 0)
                    {
                        maxPosition = gameList.ListGames.Max(glg => glg.Position);
                    }

                    var listGame = new GameListGame
                    {
                        GameId = request.Game.Id,
                        GameListId = gameList.Id,
                        Position = maxPosition + 1,
                        DateAdded = DateTime.Now
                    };

                    gameList.ListGames.Add(listGame);
                }
            }

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}