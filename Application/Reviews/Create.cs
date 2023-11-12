using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Reviews;

public class Create
{
    public class Command : IRequest<ReviewDto>
    {
        public Review Review { get; set; }
        public Guid GameId { get; set; }
    }

    public class Handler : IRequestHandler<Command, ReviewDto>
    {
        private readonly DataContext _context;
        private readonly IUserAccessor _userAccessor;
        private readonly IMapper _mapper;

        public Handler(DataContext context, IUserAccessor userAccessor, IMapper mapper)
        {
            _context = context;
            _userAccessor = userAccessor;
            _mapper = mapper;
        }

        public async Task<ReviewDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == _userAccessor.GetUsername(),
                cancellationToken: cancellationToken);
            
            request.Review.User = user;

            var game = await _context.Games.FirstOrDefaultAsync(g => g.Id == request.GameId, cancellationToken: cancellationToken);

            request.Review.Game = game;

            var review = _context.Reviews.Add(request.Review);

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ReviewDto>(review.Entity);
        }
    }
}