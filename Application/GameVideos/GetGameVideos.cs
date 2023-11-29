using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.GameVideos;

public class GetGameVideos
{
    public class Query : IRequest<List<GameVideoDto>>
    {
        public Guid Id { get; set; }
    }

    public class Handler : IRequestHandler<Query, List<GameVideoDto>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public Handler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<GameVideoDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var videos = await _context.GameVideos
                .ProjectTo<GameVideoDto>(_mapper.ConfigurationProvider)
                .Where(screenshot => screenshot.GameId == request.Id)
                .ToListAsync(cancellationToken: cancellationToken);

            return videos;
        }
    }
}