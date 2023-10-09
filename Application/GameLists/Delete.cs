using MediatR;
using Persistence;

namespace Application.GameLists;

public class Delete
{
    public class Command : IRequest
    {
        public Guid Id { get; set; }
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
            var gameList = await _context.GameLists.FindAsync(request.Id);

            if (gameList != null) _context.Remove((object)gameList);
        }
    }
}