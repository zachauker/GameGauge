using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Platforms;

public class List
{
    public class Query : IRequest<List<Platform>>
    {
    }

    public class Handler : IRequestHandler<Query, List<Platform>>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Platform>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await _context.Platforms.ToListAsync(cancellationToken: cancellationToken);
        }
    }
}