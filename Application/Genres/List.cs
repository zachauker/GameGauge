using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Genres;

public class List
{
    public class Query : IRequest<List<Genre>>
    {
    }

    public class Handler : IRequestHandler<Query, List<Genre>>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Genre>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await _context.Genres.ToListAsync(cancellationToken: cancellationToken);
        }
    }
}