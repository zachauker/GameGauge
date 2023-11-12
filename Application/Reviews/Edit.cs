using AutoMapper;
using Domain.Entities;
using MediatR;
using Persistence;

namespace Application.Reviews;

public class Edit
{
    public class Command : IRequest
    {
        public Review Review { get; set; }
    }

    public class Handler : IRequestHandler<Command>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public Handler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var review = await _context.GameLists.FindAsync(new object[] { request.Review.Id }, cancellationToken: cancellationToken);

            _mapper.Map(request.Review, review);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}