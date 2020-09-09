using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Interfaces;
using ApartmentReservation.Common.Exceptions;
using ApartmentReservation.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApartmentReservation.Application.Features.Apartments.Commands
{
    public class UpdateApartmentCommand : IRequest
    {
        public long Id { get; set; }
        public string ApartmentType { get; set; }
        public string ActivityState { get; set; }
        public string CheckInTime { get; set; }
        public string CheckOutTime { get; set; }
        public string CityName { get; set; }
        public string CountryName { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public int? NumberOfRooms { get; set; }
        public int? NumberOfGuests { get; set; }

        public string PostalCode { get; set; }

        public double? PricePerNight { get; set; }
        public string StreetName { get; set; }

        public string StreetNumber { get; set; }
        public string Title { get; set; }
    }

    public class UpdateApartmentCommandHandler : IRequestHandler<UpdateApartmentCommand>
    {
        private readonly IApartmentReservationDbContext context;

        public UpdateApartmentCommandHandler(IApartmentReservationDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(UpdateApartmentCommand request, CancellationToken cancellationToken)
        {
            var dbApartment = await this.context.Apartments
                .Include(a => a.Location)
                .ThenInclude(l => l.Address)
                .SingleOrDefaultAsync(a => a.Id == request.Id && !a.IsDeleted)
                .ConfigureAwait(false);

            if (dbApartment is null)
            {
                throw new NotFoundException();
            }

            if (dbApartment.Location is null)
            {
                dbApartment.Location = new Location();
            }
            if (dbApartment.Location.Address is null)
            {
                dbApartment.Location.Address = new Address();
            }

            MapValues(request, dbApartment);

            await this.context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return Unit.Value;
        }

        private static void MapValues(UpdateApartmentCommand request, Apartment dbApartment)
        {
            if (!string.IsNullOrEmpty(request.ActivityState))
                dbApartment.ActivityState = request.ActivityState;

            if (!string.IsNullOrEmpty(request.ApartmentType))
                dbApartment.ApartmentType = request.ApartmentType;

            if (!string.IsNullOrEmpty(request.Title))
                dbApartment.Title = request.Title;

            if (!string.IsNullOrEmpty(request.CheckInTime))
                dbApartment.CheckInTime = request.CheckInTime;

            if (!string.IsNullOrEmpty(request.CheckOutTime))
                dbApartment.CheckOutTime = request.CheckOutTime;

            dbApartment.NumberOfGuests = request.NumberOfGuests ?? dbApartment.NumberOfGuests;
            dbApartment.NumberOfRooms = request.NumberOfRooms ?? dbApartment.NumberOfRooms;
            dbApartment.PricePerNight = request.PricePerNight ?? dbApartment.PricePerNight;

            var location = dbApartment.Location;
            MapLocation(request, location);
        }

        private static void MapLocation(UpdateApartmentCommand request, Location location)
        {
            location.Latitude = request.Latitude ?? location.Latitude;
            location.Longitude = request.Longitude ?? location.Longitude;

            var address = location.Address;
            MapAddress(request, address);
        }

        private static void MapAddress(UpdateApartmentCommand request, Address address)
        {
            if (!string.IsNullOrEmpty(request.CityName))
                address.CityName = request.CityName;

            if (!string.IsNullOrEmpty(request.CountryName))
                address.CountryName = request.CountryName;

            if (!string.IsNullOrEmpty(request.StreetName))
                address.StreetName = request.StreetName;

            if (!string.IsNullOrEmpty(request.StreetNumber))
                address.StreetNumber = request.StreetNumber;

            if (!string.IsNullOrEmpty(request.PostalCode))
                address.PostalCode = request.PostalCode;
        }
    }
}