using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Interfaces;
using ApartmentReservation.Common.Constants;
using ApartmentReservation.Common.Exceptions;
using MediatR;
using MongoDB.Driver;

namespace ApartmentReservation.Application.Features.Guests.Queries
{
    public class GetGuestQuery : IRequest<GuestDto>
    {
        public long Id { get; set; }
    }

    public class GetGuestQueryHandler : IRequestHandler<GetGuestQuery, GuestDto>
    {
        private readonly IQueryDbContext context;

        public GetGuestQueryHandler(IQueryDbContext context)
        {
            this.context = context;
        }

        public async Task<GuestDto> Handle(GetGuestQuery request, CancellationToken cancellationToken)
        {
            var dbUser = await context.Users.Find(u => u.Id == request.Id && u.RoleName == RoleNames.Guest).SingleOrDefaultAsync(cancellationToken);

            if (dbUser is null)
            {
                throw new NotFoundException($"User with id={request.Id} could not be found!");
            }

            return CustomMapper.Map<GuestDto>(dbUser);
        }
    }
}