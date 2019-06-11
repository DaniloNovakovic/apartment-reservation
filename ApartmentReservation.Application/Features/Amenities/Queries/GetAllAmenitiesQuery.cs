using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApartmentReservation.Application.Features.Amenities.Queries
{
    public class GetAllAmenitiesQuery : IRequest<IEnumerable<AmenityDto>>
    {
    }

    public class GetAllAmenitiesQueryHandler : IRequestHandler<GetAllAmenitiesQuery, IEnumerable<AmenityDto>>
    {
        private readonly IApartmentReservationDbContext context;

        public GetAllAmenitiesQueryHandler(IApartmentReservationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<AmenityDto>> Handle(GetAllAmenitiesQuery request, CancellationToken cancellationToken)
        {
            var amenities = await this.context.Amenities
                .Where(a => !a.IsDeleted)
                .ToListAsync(cancellationToken).ConfigureAwait(false);

            return amenities.Select(a => new AmenityDto(a));
        }
    }
}