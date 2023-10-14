using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Games;

public class Search
{
    public class Query : IRequest<PaginatedResult<GameDto>>
    {
        public string SearchTerm { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
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
            Console.WriteLine($"Search Term: {request.SearchTerm}"); // Log search term for debugging

            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                query = query.Where(g => g.Title.Contains(request.SearchTerm));
            }

            var games = await query
                .OrderByDescending(g => g.Title)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            var totalRecords = await query.CountAsync(cancellationToken);
            Console.WriteLine($"Count: {totalRecords}"); // Log search term for debugging

            var gamesDto = _mapper.Map<List<GameDto>>(games);

            return new PaginatedResult<GameDto>(gamesDto, totalRecords, request.PageNumber, request.PageSize);
        }
    }
}