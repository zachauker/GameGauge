using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.GameLists;

public class List
{
     public class Query : IRequest<List<GameList>> {}

    public class Handler : IRequestHandler<Query, List<GameList>>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }
        
        public async Task<List<GameList>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await _context.GameLists.ToListAsync(cancellationToken: cancellationToken);
        }

    }
}