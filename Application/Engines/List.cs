using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Engines;

public class List
{
    public class Query : IRequest<List<Engine>> {}

    public class Handler : IRequestHandler<Query, List<Engine>>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }
        
        public async Task<List<Engine>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await _context.Engines.ToListAsync(cancellationToken: cancellationToken);
        }

    }
}