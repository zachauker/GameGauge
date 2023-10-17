using System.Text.Json.Nodes;
using Application.GameListGames;
using Application.Games;
using AutoMapper;
using Domain.Entities;
using MediatR;
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
            var gameList = await _context.GameLists.FindAsync(new object[] { request.ListId },
                cancellationToken: cancellationToken);
    
            if (gameList != null && request.Game != null)
            {

            }
    
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}