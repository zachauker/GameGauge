using MediatR;
using Persistence;

namespace Application.Reviews;

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
            var review = await _context.Reviews.FindAsync(request.Id);

            if (review != null) _context.Remove((object)review);
        }
    }
}