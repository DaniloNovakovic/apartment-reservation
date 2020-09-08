using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Interfaces;
using ApartmentReservation.Common.Exceptions;
using MediatR;
using MongoDB.Driver;

namespace ApartmentReservation.Application.Features.Users.Queries
{
    public class GetUserByUsernameQuery : IRequest<UserDto>
    {
        public string Username { get; set; }
    }

    public class GetUserByUsernameQueryHandler : IRequestHandler<GetUserByUsernameQuery, UserDto>
    {
        private readonly IQueryDbContext context;

        public GetUserByUsernameQueryHandler(IQueryDbContext context)
        {
            this.context = context;
        }

        public async Task<UserDto> Handle(GetUserByUsernameQuery request, CancellationToken cancellationToken)
        {
            var dbUser = await this.context.Users
                .Find(u => u.Username == request.Username)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            if (dbUser is null)
            {
                throw new NotFoundException($"Could not find user with username '{request.Username}'");
            }

            return CustomMapper.Map<UserDto>(dbUser);
        }
    }
}