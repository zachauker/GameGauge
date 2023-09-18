using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Games;

public class Details
{
    public class Query : IRequest<Game>
    {
        public Guid Id { get; set; }
    }

    public class Handler : IRequestHandler<Query, Game>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }
        
        public async Task<Game> Handle(Query request, CancellationToken cancellationToken)
        {
            return await _context.Games.FindAsync(new object[] { request.Id }, cancellationToken: cancellationToken);
        }

    }
}