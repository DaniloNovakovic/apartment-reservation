using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Interfaces;
using ApartmentReservation.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApartmentReservation.Application.Features.Apartments.Queries
{
    public class GetAllApartmentsQuery : IRequest<IEnumerable<ApartmentDto>>
    {
        public string ActivityState { get; set; }

        public string AmenityName { get; set; }
    }

    public class GetAllApartmentsQueryHandler : IRequestHandler<GetAllApartmentsQuery, IEnumerable<ApartmentDto>>
    {
        private readonly IApartmentReservationDbContext context;

        public GetAllApartmentsQueryHandler(IApartmentReservationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<ApartmentDto>> Handle(GetAllApartmentsQuery request, CancellationToken cancellationToken)
        {
            var query = context.Apartments
                .Include(a => a.Amenities)
                .Include(a => a.ForRentalDates)
                .Include(a => a.Reservations)
                .Include(a => a.Images)
                .Include(a => a.Location)
                .ThenInclude(l => l.Address)
                .Where(a => !a.IsDeleted);

            query = ApplyFilters(request, query);

            var apartments = await query.ToListAsync(cancellationToken).ConfigureAwait(false);

            return apartments.Select(a => new ApartmentDto(a));
        }

        private static IQueryable<Apartment> ApplyFilters(GetAllApartmentsQuery filters, IQueryable<Apartment> query)
        {
            if (!string.IsNullOrWhiteSpace(filters.ActivityState))
            {
                query = query.Where(apartment => string.Equals(apartment.ActivityState, filters.ActivityState, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(filters.AmenityName))
            {
                query = query.Where(apartment =>
                    apartment.Amenities.Any(amenity =>
                        string.Equals(amenity.Name, filters.AmenityName, StringComparison.OrdinalIgnoreCase)));
            }

            return query;
        }
    }
}