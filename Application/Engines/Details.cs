using Domain.Entities;
using MediatR;
using Persistence;

namespace Application.Engines;

public class Details
{
    public class Query : IRequest<Engine>
    {
        public Guid Id { get; set; }
    }

    public class Handler : IRequestHandler<Query, Engine>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Engine> Handle(Query request, CancellationToken cancellationToken)
        {
            return await _context.Engines.FindAsync(new object[] { request.Id },
                cancellationToken: cancellationToken);
        }
    }
}