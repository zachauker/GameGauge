namespace Application.Genres;
using Domain.Entities;
using MediatR;
using Persistence;

public class Details
{
    public class Query : IRequest<Genre>
    {
        public Guid Id { get; set; }
    }

     public class Handler : IRequestHandler<Query, Genre>
     {
         private readonly DataContext _context;

         public Handler(DataContext context)
         {
             _context = context;
         }

         public async Task<Genre> Handle(Query request, CancellationToken cancellationToken)
         {
             return await _context.Genres.FindAsync(new object[] { request.Id },
                 cancellationToken: cancellationToken);
         }
     }
}