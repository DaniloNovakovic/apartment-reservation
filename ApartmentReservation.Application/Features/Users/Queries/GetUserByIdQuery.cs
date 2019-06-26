using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Exceptions;
using ApartmentReservation.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApartmentReservation.Application.Features.Users.Queries
{
    public class GetUserByIdQuery : IRequest<UserDto>
    {
        public long Id { get; set; }
    }

    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto>
    {
        private readonly IApartmentReservationDbContext context;

        public GetUserByIdQueryHandler(IApartmentReservationDbContext context)
        {
            this.context = context;
        }

        public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var dbUser = await this.context.Users
                .SingleOrDefaultAsync(u => u.Id == request.Id && !u.IsDeleted, cancellationToken)
                .ConfigureAwait(false);

            if (dbUser is null)
            {
                throw new NotFoundException($"User with id={request.Id} could not be found!");
            }

            return new UserDto(dbUser) { Banned = dbUser.IsBanned };
        }
    }
}