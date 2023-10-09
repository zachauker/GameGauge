using System.Text.Json.Nodes;
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
        public List<Game> Games { set; get; }
    }

    public class Handler : IRequestHandler<Command>
    {
        private readonly DataContext _context;

        public Handler(DataContext context, IMapper mapper)
        {
            _context = context;
        }

        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var gameList = await _context.GameLists.FindAsync(new object[] { request.ListId },
                cancellationToken: cancellationToken);

            if (gameList != null && request.Games != null)
            {
                foreach (var listGame in request.Games.Select(game => _context.FindAsync<Game>(new object[] { game }, cancellationToken: cancellationToken)))
                {
                    if (!listGame.IsFaulted)
                    {
                        gameList.Games.Add(listGame.Result);
                    }

                }
            }

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}