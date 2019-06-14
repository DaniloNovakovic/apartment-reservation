using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Interfaces;
using ApartmentReservation.Domain.Constants;
using ApartmentReservation.Domain.Entities;
using FluentValidation;
using MediatR;

namespace ApartmentReservation.Application.Features.Apartments.Commands
{
    public class CreateApartmentCommand : IRequest<EntityCreatedResult>
    {
        public CreateApartmentCommand()
        {
            Amenities = new List<AmenityDto>();
            ForRentalDates = new List<ForRentalDateDto>();
            Images = new List<ImageDto>();
        }

        public IEnumerable<AmenityDto> Amenities { get; set; }
        public string ApartmentType { get; set; }
        public DateTime? CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
        public IEnumerable<ForRentalDateDto> ForRentalDates { get; set; }
        public long HostId { get; set; }
        public IEnumerable<ImageDto> Images { get; set; }
        public LocationDto Location { get; set; }
        public int NumberOfRooms { get; set; }
        public double PricePerNight { get; set; }
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
                Location = CustomMapper.Map(request.Location),
                NumberOfRooms = request.NumberOfRooms,
                PricePerNight = request.PricePerNight,
                Title = request.Title
            };

            var entityEntry = await context.Apartments.AddAsync(apartment, cancellationToken).ConfigureAwait(false);
            await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            apartment = entityEntry.Entity;
            await AppendAmenitiesAsync(request.Amenities, apartment, cancellationToken).ConfigureAwait(false);
            await AppendForRentalDatesAsync(request.ForRentalDates, apartment, cancellationToken).ConfigureAwait(false);
            await AppendImages(request.Images, apartment, cancellationToken).ConfigureAwait(false);

            return new EntityCreatedResult() { Id = apartment.Id };
        }

        private async Task AppendImages(IEnumerable<ImageDto> from, Apartment to, CancellationToken cancellationToken)
        {
            foreach (var image in from)
            {
                to.Images.Add(new Image() { ImageUri = image.Uri });
            }

            await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        private async Task AppendForRentalDatesAsync(IEnumerable<ForRentalDateDto> from, Apartment to, CancellationToken cancellationToken)
        {
            foreach (var frd in from)
            {
                to.ForRentalDates.Add(new ForRentalDate() { Date = frd.Date });
            }

            await this.context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        private async Task AppendAmenitiesAsync(IEnumerable<AmenityDto> from, Apartment to, CancellationToken cancellationToken = default)
        {
            var dbAmenities = context.Amenities.Where(amenity => from.Any(a => a.Name == amenity.Name)).ToList();

            foreach (var amenity in dbAmenities)
            {
                to.Amenities.Add(amenity);
            }

            await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
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