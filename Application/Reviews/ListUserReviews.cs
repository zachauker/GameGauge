using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Reviews;

public class ListUserReviews
{
    public class Query : IRequest<List<ReviewDto>>
    {
        public AppUser User { get; set; }
    }

    public class Handler : IRequestHandler<Query, List<ReviewDto>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public Handler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ReviewDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await _context.Reviews.Where(r => r.User == request.User)
                .Include(r => r.Game)
                .ProjectTo<ReviewDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken: cancellationToken);
        }
    }
}