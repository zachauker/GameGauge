using Domain.Entities;
using MediatR;
using Persistence;

namespace Application.GameLists;

public class Create
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
            _context.GameLists.Add(request.GameList);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}