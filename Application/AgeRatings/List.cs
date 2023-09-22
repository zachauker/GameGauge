using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.AgeRatings;

public class List
{
    public class Query : IRequest<List<AgeRating>>
    {
    }

    public class Handler : IRequestHandler<Query, List<AgeRating>>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<List<AgeRating>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await _context.AgeRatings.ToListAsync(cancellationToken: cancellationToken);
        }
    }
}