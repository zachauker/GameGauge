using AutoMapper;
using Domain.Entities;
using MediatR;
using Persistence;

namespace Application.GameLists;

public class Edit
{
    public class Command : IRequest
    {
        public GameList GameList { get; set; }
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
            var gameList = await _context.GameLists.FindAsync(new object[] { request.GameList.Id }, cancellationToken: cancellationToken);

            _mapper.Map(request.GameList, gameList);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}