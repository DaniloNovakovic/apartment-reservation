using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Domain.Entities;
using ApartmentReservation.Domain.Interfaces;
using System.Linq;

namespace ApartmentReservation.Application
{
    internal static class CustomMapper
    {
        public static void Map(IUser src, IUserRoleLogical dest, bool isDeleted = false)
        {
            dest.User.FirstName = src.FirstName ?? dest.User.FirstName;
            dest.User.LastName = src.LastName ?? dest.User.LastName;
            dest.User.Password = src.Password ?? dest.User.Password;
            dest.User.Gender = src.Gender ?? dest.User.Gender;
            dest.User.RoleName = src.RoleName;
            dest.User.IsDeleted = isDeleted;
            dest.IsDeleted = isDeleted;
        }

        public static void Map(IUser src, IUserRoleLogical dest, string roleName, bool isDeleted = false)
        {
            Map(src, dest, isDeleted);
            dest.User.RoleName = roleName;
        }

        public static TDestination Map<TDestination>(object source) where TDestination : new()
        {
            var destination = new TDestination();

            Map(source, destination);

            return destination;
        }

        public static void Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            foreach (var srcProp in source.GetType().GetProperties())
            {
                var destProp = destination.GetType().GetProperty(srcProp.Name);
                if (destProp == null)
                {
                    continue;
                }
                destProp.SetValue(destination, srcProp.GetValue(source));
            }
        }

        public static ApartmentDto Map(Domain.Read.Models.ApartmentModel apartment)
        {
            var dto = new ApartmentDto
            {
                Id = apartment.Id,
                ForRentalDates = apartment.ForRentalDates,
                AvailableDates = apartment.AvailableDates,
                CheckInTime = apartment.CheckInTime,
                CheckOutTime = apartment.CheckOutTime,
                ActivityState = apartment.ActivityState,
                Rating = apartment.Rating,
                Title = apartment.Title,
                PricePerNight = apartment.PricePerNight,
                ApartmentType = apartment.ApartmentType,
                NumberOfGuests = apartment.NumberOfGuests,
                NumberOfRooms = apartment.NumberOfRooms
            };

            dto.Amenities = apartment.Amenities.Select(a => Map<AmenityDto>(a)).ToList();
            dto.Images = apartment.Images.Select(i => Map<ImageDto>(i)).ToList();
            dto.Comments = apartment.Comments.Select(c => Map<CommentDto>(c)).ToList();
            dto.Location = Map(apartment.Location);
            dto.HostId = apartment.HostId;

            return dto;
        }

        public static LocationDto Map(Domain.Read.Models.LocationModel location)
        {
            var address = location.Address;

            return new LocationDto()
            {
                Latitude = location.Latitude,
                Longitude = location.Longitude,
                Address = new AddressDto
                {
                    CityName = address.CountryName,
                    CountryName = address.CountryName,
                    StreetName = address.StreetName,
                    StreetNumber = address.StreetNumber,
                    PostalCode = address.PostalCode
                }
            };
        }
    }
}