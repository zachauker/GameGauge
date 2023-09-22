using Domain.Entities;
using MediatR;
using Persistence;

namespace Application.Platforms;

public class Details
{
     public class Query : IRequest<Platform>
    {
        public Guid Id { get; set; }
    }

     public class Handler : IRequestHandler<Query, Platform>
     {
         private readonly DataContext _context;

         public Handler(DataContext context)
         {
             _context = context;
         }

         public async Task<Platform> Handle(Query request, CancellationToken cancellationToken)
         {
             return await _context.Platforms.FindAsync(new object[] { request.Id },
                 cancellationToken: cancellationToken);
         }
     }
}