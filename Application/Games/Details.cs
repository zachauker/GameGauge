using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Games;

public class Details
{
    public class Query : IRequest<GameDto>
    {
        public Guid Id { get; set; }
    }

    public class Handler : IRequestHandler<Query, GameDto>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public Handler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<GameDto> Handle(Query request, CancellationToken cancellationToken)
        {
            var game =  await _context.Games.FindAsync(new object[] { request.Id }, cancellationToken: cancellationToken);

            return _mapper.Map<GameDto>(game);
        }

    }
}