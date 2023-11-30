using Application.Games;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Screenshots;

public class GameScreenshots
{
    public class Query : IRequest<List<ScreenshotDto>>
    {
        public Guid Id { get; set; }
    }

    public class Handler : IRequestHandler<Query, List<ScreenshotDto>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public Handler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ScreenshotDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var screenshots = await _context.Screenshots
                .ProjectTo<ScreenshotDto>(_mapper.ConfigurationProvider)
                .Where(screenshot => screenshot.GameId == request.Id)
                .ToListAsync(cancellationToken: cancellationToken);

            return screenshots;
        }
    }
}