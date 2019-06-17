using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApartmentReservation.Application.Features.Amenities.Queries
{
    public class GetAllAmenitiesQuery : IRequest<IEnumerable<AmenityDto>>
    {
        public string Search { get; set; } = "";
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
            var query = this.context.Amenities.Where(a => !a.IsDeleted);

            query = ApplyFilters(request, query);

            var amenities = await query.ToListAsync(cancellationToken).ConfigureAwait(false);

            return amenities.Select(a => new AmenityDto(a));
        }

        private static IQueryable<Domain.Entities.Amenity> ApplyFilters(GetAllAmenitiesQuery request, IQueryable<Domain.Entities.Amenity> query)
        {
            if (!string.IsNullOrEmpty(request.Search))
            {
                query = query.Where(a => a.Name.Trim().Contains(request.Search.Trim(), System.StringComparison.OrdinalIgnoreCase));
            }

            return query;
        }
    }
}