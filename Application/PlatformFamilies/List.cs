using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.PlatformFamilies;

public class List
{
    public class Query : IRequest<List<PlatformFamily>>
    {
    }

    public class Handler : IRequestHandler<Query, List<PlatformFamily>>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<List<PlatformFamily>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await _context.PlatformFamilies.ToListAsync(cancellationToken: cancellationToken);
        }
    }
}