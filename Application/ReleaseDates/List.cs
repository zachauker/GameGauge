using Domain.Entities;
using MediatR;
using Persistence;
using Microsoft.EntityFrameworkCore;

namespace Application.ReleaseDates;

public class List
{
    public class Query : IRequest<List<ReleaseDate>>
    {
    }

    public class Handler : IRequestHandler<Query, List<ReleaseDate>>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<List<ReleaseDate>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await _context.ReleaseDates.ToListAsync(cancellationToken: cancellationToken);
        }
    }
}