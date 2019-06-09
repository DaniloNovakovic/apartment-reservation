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
        private readonly IMapper mapper;

        public GetAllAmenitiesQueryHandler(IApartmentReservationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<AmenityDto>> Handle(GetAllAmenitiesQuery request, CancellationToken cancellationToken)
        {
            var amenities = await this.context.Amenities
                .Where(a => !a.IsDeleted)
                .ToListAsync(cancellationToken).ConfigureAwait(false);

            return amenities.Select(this.mapper.Map<AmenityDto>);
        }
    }
}