using Domain.Entities;
using Domain.Entities;
using MediatR;
using Persistence;

namespace Application.AgeRatings;

public class Details
{
        public class Query : IRequest<AgeRating>
    {
        public Guid Id { get; set; }
    }

     public class Handler : IRequestHandler<Query, AgeRating>
     {
         private readonly DataContext _context;

         public Handler(DataContext context)
         {
             _context = context;
         }

         public async Task<AgeRating> Handle(Query request, CancellationToken cancellationToken)
         {
             return await _context.AgeRatings.FindAsync(new object[] { request.Id },
                 cancellationToken: cancellationToken);
         }
     }
}