using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Exceptions;
using ApartmentReservation.Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApartmentReservation.Application.Features.Guests.Queries
{
    public class GetGuestQuery : IRequest<GuestDto>
    {
        public long Id { get; set; }
    }

    public class GetGuestQueryHandler : IRequestHandler<GetGuestQuery, GuestDto>
    {
        private readonly IApartmentReservationDbContext context;

        public GetGuestQueryHandler(IApartmentReservationDbContext context)
        {
            this.context = context;
        }

        public async Task<GuestDto> Handle(GetGuestQuery request, CancellationToken cancellationToken)
        {
            var guest = await this.context.Guests.SingleOrDefaultAsync(g => g.UserId == request.Id && !g.IsDeleted).ConfigureAwait(false);

            if (guest is null)
            {
                throw new NotFoundException($"Could not find user '{request.Id}'");
            }

            return new GuestDto(guest);
        }
    }
}