using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Artworks;

public class GetGameArtwork
{
    public class Query : IRequest<List<ArtworkDto>>
    {
        public Guid Id { get; set; }
    }

    public class Handler : IRequestHandler<Query, List<ArtworkDto>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public Handler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ArtworkDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var artwork = await _context.Artworks
                .ProjectTo<ArtworkDto>(_mapper.ConfigurationProvider)
                .Where(screenshot => screenshot.GameId == request.Id)
                .ToListAsync(cancellationToken: cancellationToken);

            return artwork;
        }
    }
}