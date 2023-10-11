using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.GameLists;

public class Details
{
    public class Query : IRequest<GameListDto>
    {
        public Guid Id { get; set; }
    }

    public class Handler : IRequestHandler<Query, GameListDto>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public Handler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GameListDto> Handle(Query request, CancellationToken cancellationToken)
        {
            var gameList = await _context.GameLists
                .ProjectTo<GameListDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(list => list.Id == request.Id, cancellationToken: cancellationToken);

            return gameList;
        }
    }
}