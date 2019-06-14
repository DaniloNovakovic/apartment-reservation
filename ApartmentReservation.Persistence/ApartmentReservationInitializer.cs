using System.Collections.Generic;
using System.Linq;
using ApartmentReservation.Application.Infrastructure.Authentication;
using ApartmentReservation.Domain.Constants;
using ApartmentReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApartmentReservation.Persistence
{
    public class ApartmentReservationInitializer
    {
        private readonly Dictionary<long, User> Users = new Dictionary<long, User>();
        private IEnumerable<Administrator> Administrators = new List<Administrator>();
        private List<Amenity> Amenities = new List<Amenity>();
        private IEnumerable<Guest> Guests = new List<Guest>();
        private IEnumerable<Host> Hosts = new List<Host>();

        public static void Initialize(ApartmentReservationDbContext context)
        {
            var initializer = new ApartmentReservationInitializer();
            initializer.SeedEverything(context);
        }

        protected void SeedEverything(ApartmentReservationDbContext context)
        {
            context.Database.Migrate();
            //context.Database.EnsureCreated();

            if (context.Administrators.Any())
            {
                return; // Db is seeded
            }

            this.SeedUsers(context);
            this.SeedAdministrators(context);
            this.SeedHosts(context);
            this.SeedGuests(context);
            this.SeedAmenities(context);
            this.SeedApartments(context);
        }

        private void SeedUsers(ApartmentReservationDbContext context)
        {
            this.Users[1] = new User()
            {
                Username = "admin",
                Password = "admin",
                RoleName = RoleNames.Administrator
            };

            this.Users[2] = new User()
            {
                Username = "host",
                Password = "host",
                RoleName = RoleNames.Host
            };

            this.Users[3] = new User()
            {
                Username = "guest",
                Password = "guest",
                RoleName = RoleNames.Guest
            };

            context.Users.AddRange(this.Users.Values);

            context.SaveChanges();
        }

        private void SeedAdministrators(ApartmentReservationDbContext context)
        {
            this.Administrators = this.Users.Values
                .Where(u => u.RoleName == RoleNames.Administrator)
                .Select(u => new Administrator() { User = u });

            context.Administrators.AddRange(this.Administrators);

            context.SaveChanges();
        }

        private void SeedHosts(ApartmentReservationDbContext context)
        {
            this.Hosts = this.Users.Values
                .Where(u => u.RoleName == RoleNames.Host)
                .Select(u => new Host() { User = u });

            context.Hosts.AddRange(this.Hosts);

            context.SaveChanges();
        }

        private void SeedGuests(ApartmentReservationDbContext context)
        {
            this.Guests = this.Users.Values
                .Where(u => u.RoleName == RoleNames.Guest)
                .Select(u => new Guest() { User = u });

            context.Guests.AddRange(this.Guests);

            context.SaveChanges();
        }

        private void SeedAmenities(ApartmentReservationDbContext context)
        {
            this.Amenities = new List<Amenity>()
            {
                new Amenity(){Name ="TV"},
                new Amenity(){Name ="Kitchen"},
                new Amenity(){Name ="High chair"},
                new Amenity(){Name ="Air conditioning"},
                new Amenity(){Name ="Heating"},
                new Amenity(){Name ="Wifi"},
                new Amenity(){Name ="Refrigerator"},
                new Amenity(){Name ="Microwave"}
            };

            context.AddRange(this.Amenities);
            context.SaveChanges();
        }

        private void SeedApartments(ApartmentReservationDbContext context)
        {
            var address = GetAddress(context);
            var location = GetLocation(context, address);
            var host = context.Hosts.FirstOrDefault();
            var amenities = context.Amenities.FirstOrDefault();

            var apartment = new Apartment()
            {
                ActivityState = ActivityStates.Active,
                ApartmentType = ApartmentTypes.Full,
                Host = host,
                Location = location,
                Title = "Magnificent apartment",
                PricePerNight = 23
            };

            apartment = context.Apartments.Add(apartment).Entity;
            this.Amenities.ForEach(a => apartment.Amenities.Add(a));
            context.SaveChanges();

            var images = GetImages();
            images.ForEach(i => apartment.Images.Add(i));
            context.SaveChanges();
        }

        private static List<Image> GetImages()
        {
            return new List<Image>()
            {
                new Image(){ImageUri = "https://www.onni.com/wp-content/uploads/2016/11/Rental-Apartment-Page-new-min.jpg"},
                new Image(){ImageUri = "https://arystudios.files.wordpress.com/2015/08/3dcontemperoryapartmentrenderingarchitecturalduskviewrealisticarystudios.jpg"},
                new Image(){ImageUri = "https://www.travelonline.com/melbourne/city-cbd/accommodation/adina-apartment-hotel-melbourne-flinders-street/penthouse-76880.jpg"},
            };
        }

        private static Address GetAddress(ApartmentReservationDbContext context)
        {
            var address = new Address()
            {
                CityName = "Novi Sad",
                PostalCode = "21102",
                StreetName = "Bulevar kralja Petra",
                StreetNumber = "25",
                CountryName = "Serbia"
            };

            address = context.Addresses.Add(address).Entity;
            context.SaveChanges();
            return address;
        }

        private static Location GetLocation(ApartmentReservationDbContext context, Address address)
        {
            var location = new Location()
            {
                Latitude = 45.267136,
                Longitude = 19.833549,
                Address = address
            };
            location = context.Locations.Add(location).Entity;
            context.SaveChanges();
            return location;
        }
    }
}