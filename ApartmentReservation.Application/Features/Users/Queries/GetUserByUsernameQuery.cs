using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Exceptions;
using ApartmentReservation.Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApartmentReservation.Application.Features.Users.Queries
{
    public class GetUserByUsernameQuery : IRequest<UserDto>
    {
        public string Username { get; set; }
    }

    public class GetUserByUsernameQueryHandler : IRequestHandler<GetUserByUsernameQuery, UserDto>
    {
        private readonly IApartmentReservationDbContext context;

        public GetUserByUsernameQueryHandler(IApartmentReservationDbContext context)
        {
            this.context = context;
        }

        public async Task<UserDto> Handle(GetUserByUsernameQuery request, CancellationToken cancellationToken)
        {
            var dbUser = await this.context.Users
                .SingleOrDefaultAsync(u => u.Username == request.Username && !u.IsDeleted, cancellationToken)
                .ConfigureAwait(false);

            if (dbUser is null)
            {
                throw new NotFoundException($"Could not find user with username '{request.Username}'");
            }

            return new UserDto(dbUser);
        }
    }
}