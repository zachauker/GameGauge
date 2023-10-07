using Domain.Entities;
using MediatR;
using Persistence;

namespace Application.GameLists;

public class Edit
{
    public class Command : IRequest
    {
        public GameList GameList { get; set; }
    }

    public class Handler : IRequestHandler<Command>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var gameList = await _context.GameLists.FindAsync(new object[] { request.GameList.Id }, cancellationToken: cancellationToken);

            if (gameList != null)
            {
                gameList.Title = request.GameList.Title ?? gameList.Title;
                gameList.Description = request.GameList.Description ?? gameList.Description;
                gameList.Games = request.GameList.Games ?? gameList.Games;
            }

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}