using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Interfaces;
using ApartmentReservation.Common.Constants;
using ApartmentReservation.Domain.Entities;
using FluentValidation;
using MediatR;

namespace ApartmentReservation.Application.Features.Apartments.Commands
{
    public class CreateApartmentCommand : IRequest<EntityCreatedResult>
    {
        public CreateApartmentCommand()
        {
            this.Amenities = new List<AmenityDto>();
            this.ForRentalDates = new List<DateTime>();
        }

        public IEnumerable<AmenityDto> Amenities { get; set; }
        public string ApartmentType { get; set; }
        public string CheckInTime { get; set; }
        public string CheckOutTime { get; set; }
        public string CityName { get; set; }
        public string CountryName { get; set; }
        public IEnumerable<DateTime> ForRentalDates { get; set; }
        public long HostId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int NumberOfRooms { get; set; }
        public int NumberOfGuests { get; set; }
        public string PostalCode { get; set; }

        public double PricePerNight { get; set; }
        public string StreetName { get; set; }

        public string StreetNumber { get; set; }
        public string Title { get; set; }
    }

    public class CreateApartmentCommandHandler : IRequestHandler<CreateApartmentCommand, EntityCreatedResult>
    {
        private readonly IApartmentReservationDbContext context;

        public CreateApartmentCommandHandler(IApartmentReservationDbContext context)
        {
            this.context = context;
        }

        public async Task<EntityCreatedResult> Handle(CreateApartmentCommand request, CancellationToken cancellationToken)
        {
            var apartment = new Apartment()
            {
                ApartmentType = request.ApartmentType,
                ActivityState = ActivityStates.Inactive,
                CheckInTime = request.CheckInTime,
                CheckOutTime = request.CheckOutTime,
                HostId = request.HostId,
                NumberOfRooms = request.NumberOfRooms,
                NumberOfGuests = request.NumberOfGuests,
                PricePerNight = request.PricePerNight,
                Title = request.Title,
                Location = new Location()
                {
                    Latitude = request.Latitude,
                    Longitude = request.Longitude,
                    Address = new Address()
                    {
                        CityName = request.CityName,
                        CountryName = request.CountryName,
                        PostalCode = request.PostalCode,
                        StreetName = request.StreetName,
                        StreetNumber = request.StreetNumber
                    }
                }
            };

            var entityEntry = await this.context.Apartments.AddAsync(apartment, cancellationToken).ConfigureAwait(false);
            await this.context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            apartment = entityEntry.Entity;
            await this.AppendAmenitiesAsync(request.Amenities, apartment, cancellationToken).ConfigureAwait(false);
            await this.AppendForRentalDatesAsync(request.ForRentalDates, apartment, cancellationToken).ConfigureAwait(false);

            return new EntityCreatedResult() { Id = apartment.Id };
        }

        private async Task AppendAmenitiesAsync(IEnumerable<AmenityDto> from, Apartment to, CancellationToken cancellationToken = default)
        {
            var dbAmenities = this.context.Amenities.Where(amenity => from.Any(a => a.Name == amenity.Name)).ToList();

            foreach (var amenity in dbAmenities)
            {
                to.ApartmentAmenities.Add(new ApartmentAmenity() { Amenity = amenity, Apartment = to });
            }

            await this.context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        private async Task AppendForRentalDatesAsync(IEnumerable<DateTime> from, Apartment to, CancellationToken cancellationToken)
        {
            foreach (var frd in from)
            {
                to.ForRentalDates.Add(new ForRentalDate() { Date = frd });
            }

            await this.context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
    }

    public class CreateApartmentCommandValidation : AbstractValidator<CreateApartmentCommand>
    {
        public CreateApartmentCommandValidation()
        {
            this.RuleFor(c => c.ApartmentType).NotEmpty();
            this.RuleFor(c => c.Title).NotEmpty();
            this.RuleFor(c => c.NumberOfRooms).GreaterThan(0);
            this.RuleFor(c => c.PricePerNight).GreaterThanOrEqualTo(0);
        }
    }
}