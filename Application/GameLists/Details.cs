using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
namespace Application.GameLists;

public class Details
{
     public class Query : IRequest<GameList>
    {
        public Guid Id { get; set; }
    }

    public class Handler : IRequestHandler<Query, GameList>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }
        
        public async Task<GameList> Handle(Query request, CancellationToken cancellationToken)
        {
            return await _context.GameLists.FindAsync(new object[] { request.Id }, cancellationToken: cancellationToken);
        }

    }
}