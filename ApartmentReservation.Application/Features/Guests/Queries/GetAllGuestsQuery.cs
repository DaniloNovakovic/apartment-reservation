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
        private readonly IMapper mapper;

        public GetAllGuestsQueryHandler(IApartmentReservationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<GuestDto>> Handle(GetAllGuestsQuery request, CancellationToken cancellationToken)
        {
            var guests = await this.context.Guests.Where(g => !g.IsDeleted).ToListAsync(cancellationToken).ConfigureAwait(false);
            return guests.Select(this.mapper.Map<GuestDto>);
        }
    }
}