using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Features.Reservations.Queries;
using ApartmentReservation.Application.Interfaces;
using ApartmentReservation.Common;
using ApartmentReservation.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApartmentReservation.Application.Features.Apartments.Queries
{
    public class GetAllApartmentsQuery : IRequest<IEnumerable<ApartmentDto>>
    {
        public string ActivityState { get; set; }
        public string AmenityName { get; set; }
        public string ApartmentType { get; set; }
        public string CityName { get; set; }
        public string CountryName { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public double? FromPrice { get; set; }
        public double? ToPrice { get; set; }
        public int? FromNumberOfRooms { get; set; }
        public int? ToNumberOfRooms { get; set; }
        public int? NumberOfGuests { get; set; }
        public long? HostId { get; set; }
    }

    public class GetAllApartmentsQueryHandler : IRequestHandler<GetAllApartmentsQuery, IEnumerable<ApartmentDto>>
    {
        private readonly IApartmentReservationDbContext context;
        private readonly IMediator mediator;

        public GetAllApartmentsQueryHandler(IApartmentReservationDbContext context, IMediator mediator)
        {
            this.context = context;
            this.mediator = mediator;
        }

        public async Task<IEnumerable<ApartmentDto>> Handle(GetAllApartmentsQuery request, CancellationToken cancellationToken)
        {
            var query = this.GetApartmentsWithIncludedRelations();

            query = ApplyBasicFilters(request, query);

            var apartments = await query.ToListAsync(cancellationToken).ConfigureAwait(false);

            var tasks = apartments.Select(async item => new ApartmentDto(item)
            {
                Rating = await this.context.Comments.Where(c => !c.IsDeleted && c.ApartmentId == item.Id)
                    .DefaultIfEmpty()
                    .AverageAsync(c => (double)c.Rating, cancellationToken).ConfigureAwait(false),
                AvailableDates = await this.mediator.Send(new GetAvailableDatesQuery() { ApartmentId = item.Id }, cancellationToken).ConfigureAwait(false)
            });

            var apartmentDtos = await Task.WhenAll(tasks).ConfigureAwait(false);
            return this.ApplyComplexFilters(request, apartmentDtos);
        }

        private IEnumerable<ApartmentDto> ApplyComplexFilters(GetAllApartmentsQuery filter, IEnumerable<ApartmentDto> apartmentDtos)
        {
            var query = apartmentDtos;

            if (!string.IsNullOrWhiteSpace(filter.AmenityName))
            {
                query = query.Where(apartment =>
                    apartment.Amenities.Any(amenity =>
                        string.Equals(amenity.Name, filter.AmenityName, StringComparison.OrdinalIgnoreCase)));
            }

            if (filter.FromDate != null && filter.ToDate != null)
            {
                var fromDate = filter.FromDate.Value;
                var toDate = filter.ToDate.Value;

                string[] daysRangeStr = DateTimeHelpers.GetDateDayRange(fromDate, toDate)
                    .Select(d => DateTimeHelpers.FormatToYearMonthDayString(d))
                    .ToArray();

                query = query.Where(a => !daysRangeStr
                    .Except(a.AvailableDates.Select(d => DateTimeHelpers.FormatToYearMonthDayString(d)).ToArray())
                    .Any()).ToArray();
            }
            else if (filter.FromDate != null)
            {
                var fromDate = filter.FromDate.Value;
                query = query.Where(a => a.AvailableDates.Any(d => !DateTimeHelpers.IsDayBefore(d, fromDate)));
            }
            else if (filter.ToDate != null)
            {
                var toDate = filter.ToDate.Value;
                query = query.Where(a => a.AvailableDates.Any(d => !DateTimeHelpers.IsDayAfter(d, toDate)));
            }

            return query.ToArray();
        }

        private IQueryable<Apartment> GetApartmentsWithIncludedRelations()
        {
            return this.context.Apartments
                .Include("ApartmentAmenities.Amenity")
                .Include(a => a.ForRentalDates)
                .Include(a => a.Images)
                .Include(a => a.Location)
                .ThenInclude(l => l.Address)
                .Include(a => a.Host)
                .ThenInclude(h => h.User)
                .Where(a => !a.IsDeleted);
        }

        private static IQueryable<Apartment> ApplyBasicFilters(GetAllApartmentsQuery filters, IQueryable<Apartment> query)
        {
            if (!string.IsNullOrWhiteSpace(filters.ActivityState))
            {
                query = query.Where(apartment => string.Equals(apartment.ActivityState, filters.ActivityState, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(filters.ApartmentType))
            {
                query = query.Where(apartment => string.Equals(apartment.ApartmentType, filters.ApartmentType, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(filters.CityName))
            {
                query = query.Where(a => string.Equals(a.Location.Address.CityName, filters.CityName, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(filters.CountryName))
            {
                query = query.Where(a => string.Equals(a.Location.Address.CountryName, filters.CountryName, StringComparison.OrdinalIgnoreCase));
            }

            if (filters.FromPrice != null)
            {
                query = query.Where(a => a.PricePerNight >= filters.FromPrice);
            }

            if (filters.ToPrice != null)
            {
                query = query.Where(a => a.PricePerNight <= filters.ToPrice);
            }

            if (filters.FromNumberOfRooms != null)
            {
                query = query.Where(a => a.NumberOfRooms >= filters.FromNumberOfRooms);
            }

            if (filters.ToNumberOfRooms != null)
            {
                query = query.Where(a => a.NumberOfRooms <= filters.ToNumberOfRooms);
            }

            if (filters.NumberOfGuests != null)
            {
                query = query.Where(a => a.NumberOfGuests >= filters.NumberOfGuests);
            }

            if (filters.HostId != null)
            {
                query = query.Where(apartment => apartment.HostId == filters.HostId);
            }

            return query;
        }
    }
}