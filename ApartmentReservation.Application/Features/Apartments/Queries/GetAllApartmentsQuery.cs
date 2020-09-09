using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Interfaces;
using ApartmentReservation.Common;
using ApartmentReservation.Domain.Read.Models;
using MediatR;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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
        private readonly IQueryDbContext context;

        public GetAllApartmentsQueryHandler(IQueryDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<ApartmentDto>> Handle(GetAllApartmentsQuery request, CancellationToken cancellationToken)
        {
            var query = this.context.Apartments.AsQueryable();

            query = ApplyBasicFilters(request, query);

            var apartments = await query.ToListAsync(cancellationToken).ConfigureAwait(false);

            var filtered = this.ApplyComplexFilters(request, apartments);

            return filtered.Select(a => CustomMapper.Map(a)).ToList();
        }

        private IEnumerable<ApartmentModel> ApplyComplexFilters(GetAllApartmentsQuery filter, IEnumerable<ApartmentModel> apartmentDtos)
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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "RCS1155:Use StringComparison when comparing strings.", Justification = "Current version of MongoDB.Driver.Linq doesn't support it")]
        private static IMongoQueryable<ApartmentModel> ApplyBasicFilters(GetAllApartmentsQuery filters, IMongoQueryable<ApartmentModel> query)
        {
            if (!string.IsNullOrWhiteSpace(filters.ActivityState))
            {
                query = query.Where(apartment => apartment.ActivityState.ToLower() == filters.ActivityState.ToLower());
            }

            if (!string.IsNullOrEmpty(filters.ApartmentType))
            {
                query = query.Where(apartment => apartment.ApartmentType.ToLower() == filters.ApartmentType.ToLower());
            }

            if (!string.IsNullOrEmpty(filters.CityName))
            {
                query = query.Where(a => a.Location.Address.CityName.ToLower() == filters.CityName.ToLower());
            }

            if (!string.IsNullOrEmpty(filters.CountryName))
            {
                query = query.Where(a => a.Location.Address.CountryName.ToLower() == filters.CountryName.ToLower());
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