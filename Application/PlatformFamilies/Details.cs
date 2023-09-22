using Domain.Entities;
using MediatR;
using Persistence;

namespace Application.PlatformFamilies;

public class Details
{
    public class Query : IRequest<PlatformFamily>
    {
        public Guid Id { get; set; }
    }

    public class Handler : IRequestHandler<Query, PlatformFamily>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<PlatformFamily> Handle(Query request, CancellationToken cancellationToken)
        {
            return await _context.PlatformFamilies.FindAsync(new object[] { request.Id },
                cancellationToken: cancellationToken);
        }
    }
}