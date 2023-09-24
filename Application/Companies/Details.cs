using Domain.Entities;
using MediatR;
using Persistence;

namespace Application.Companies;

public class Details
{
        public class Query : IRequest<Company>
    {
        public Guid Id { get; set; }
    }

     public class Handler : IRequestHandler<Query, Company>
     {
         private readonly DataContext _context;

         public Handler(DataContext context)
         {
             _context = context;
         }

         public async Task<Company> Handle(Query request, CancellationToken cancellationToken)
         {
             return await _context.Companies.FindAsync(new object[] { request.Id },
                 cancellationToken: cancellationToken);
         }
     }
}