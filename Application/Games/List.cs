using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Games;

public class List
{
    public class Query : IRequest<PaginatedResult<GameDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 100;
    }

    public class Handler : IRequestHandler<Query, PaginatedResult<GameDto>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public Handler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<GameDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var query = _context.Games.AsQueryable();

            var games = await query
                .OrderByDescending(g => g.Title)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);
            
            var totalRecords = await query.CountAsync(cancellationToken);

            var gamesDto = _mapper.Map<List<GameDto>>(games);

            return new PaginatedResult<GameDto>(gamesDto, totalRecords, request.PageNumber, request.PageSize);
        }
    }
}