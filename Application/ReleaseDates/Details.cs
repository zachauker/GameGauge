using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.ReleaseDates;

public class Details
{
        public class Query : IRequest<ReleaseDate>
    {
        public Guid Id { get; set; }
    }

     public class Handler : IRequestHandler<Query, ReleaseDate>
     {
         private readonly DataContext _context;

         public Handler(DataContext context)
         {
             _context = context;
         }

         public async Task<ReleaseDate> Handle(Query request, CancellationToken cancellationToken)
         {
             return await _context.ReleaseDates.FindAsync(new object[] { request.Id },
                 cancellationToken: cancellationToken);
         }
     }
}