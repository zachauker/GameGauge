using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.GameLists;

public class Create
{
    public class Command : IRequest<GameListDto>
    {
        public GameList GameList { get; set; }
    }

    public class Handler : IRequestHandler<Command, GameListDto>
    {
        private readonly DataContext _context;
        private readonly IUserAccessor _userAccessor;
        private readonly IMapper _mapper;

        public Handler(DataContext context, IUserAccessor userAccessor, IMapper _mapper)
        {
            _context = context;
            _userAccessor = userAccessor;
            this._mapper = _mapper;
        }

        public async Task<GameListDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == _userAccessor.GetUsername(),
                cancellationToken: cancellationToken);

            request.GameList.User = user;

            var gameList = _context.GameLists.Add(request.GameList);

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<GameListDto>(gameList.Entity);
        }
    }
}