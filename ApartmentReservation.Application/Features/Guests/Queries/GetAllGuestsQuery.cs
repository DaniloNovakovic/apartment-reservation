using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApartmentReservation.Application.Features.Guests.Queries
{
    public class GetAllGuestsQuery : IRequest<IEnumerable<GuestDto>>
    {
    }

    public class GetAllGuestsQueryHandler : IRequestHandler<GetAllGuestsQuery, IEnumerable<GuestDto>>
    {
        private readonly IApartmentReservationDbContext context;

        public GetAllGuestsQueryHandler(IApartmentReservationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<GuestDto>> Handle(GetAllGuestsQuery request, CancellationToken cancellationToken)
        {
            var guests = await this.context.Guests.Where(g => !g.IsDeleted).ToListAsync(cancellationToken).ConfigureAwait(false);
            return guests.Select(g => new GuestDto(g));
        }
    }
}