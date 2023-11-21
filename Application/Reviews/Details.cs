using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Reviews;

public class Details
{
    public class Query : IRequest<ReviewDto>
    {
        public Guid Id { get; set; }
    }

    public class Handler : IRequestHandler<Query, ReviewDto>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public Handler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ReviewDto> Handle(Query request, CancellationToken cancellationToken)
        {
            var review = await _context.Reviews
                .Include(r => r.User)
                .ProjectTo<ReviewDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(review => review.Id == request.Id, cancellationToken: cancellationToken);

            return review;
        }
    }
}