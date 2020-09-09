using System;
using System.Linq;
using ApartmentReservation.Common;
using ApartmentReservation.Common.Constants;
using ApartmentReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApartmentReservation.Persistence
{
    public class ApartmentReservationInitializer
    {
        public Amenity[] Amenities { get; private set; }
        public Administrator Admin { get; private set; }
        public Host Host { get; private set; }
        public Guest[] Guests { get; private set; }
        public Apartment[] Apartments { get; private set; }
        public Image[] Images { get; private set; }
        public DateTime MinDate { get; } = new DateTime(year: 2019, month: 6, day: 24); // Monday
        public DateTime MaxDate { get; } = new DateTime(year: 2019, month: 7, day: 24); // Wednesday
        public Reservation[] Reservations { get; private set; }

        public ApartmentReservationInitializer()
        {
            MinDate = DateTime.Now.AddDays(-4);
            MaxDate = MinDate.AddMonths(1);
        }

        public static void Initialize(ApartmentReservationDbContext context)
        {
            var initializer = new ApartmentReservationInitializer();
            initializer.SeedEverything(context);
        }

        protected void SeedEverything(ApartmentReservationDbContext context)
        {
            context.Database.Migrate();

            if (context.Administrators.Any())
            {
                return;
            }

            this.SeedAdministrators(context);
            this.SeedHosts(context);
            this.SeedGuests(context);
            this.SeedAmenities(context);
            this.SeedApartments(context);
            this.SeedApartmentAmenities(context);
            this.SeedImages(context);
            this.SeedForRentalDates(context);
            this.SeedReservations(context);
            this.SeedComments(context);
        }

        private void SeedAdministrators(ApartmentReservationDbContext context)
        {
            this.Admin = context.Add(new Administrator()
            {
                User = new User()
                {
                    Username = "admin",
                    Password = "admin",
                    FirstName = "Jotaro",
                    LastName = "Kujo",
                    Gender = Genders.Male,
                    RoleName = RoleNames.Administrator
                }
            }).Entity;

            context.SaveChanges();
        }

        private void SeedHosts(ApartmentReservationDbContext context)
        {
            this.Host = context.Add(new Host()
            {
                User = new User()
                {
                    Username = "host",
                    Password = "host",
                    FirstName = "Enyaba",
                    LastName = "Geil",
                    Gender = Genders.Female,
                    RoleName = RoleNames.Host
                }
            }).Entity;

            context.SaveChanges();
        }

        private void SeedGuests(ApartmentReservationDbContext context)
        {
            this.Guests = new[]
            {
                new Guest()
                {
                    User = new User()
                    {
                        Username = "guest",
                        Password = "guest",
                        FirstName = "Dio",
                        LastName = "Brando",
                        Gender = Genders.Other,
                        RoleName = RoleNames.Guest
                    }
                },
                new Guest()
                {
                    User = new User()
                    {
                        Username = "guest2",
                        Password = "guest2",
                        FirstName = "Joseph",
                        LastName = "Joester",
                        Gender = Genders.Male,
                        RoleName = RoleNames.Guest
                    }
                }
            };

            context.Guests.AddRange(this.Guests);
            context.SaveChanges();
        }

        private void SeedAmenities(ApartmentReservationDbContext context)
        {
            this.Amenities = new[]
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

            context.Amenities.AddRange(this.Amenities);
            context.SaveChanges();
        }

        private void SeedApartments(ApartmentReservationDbContext context)
        {
            this.Apartments = new[]
            {
                new Apartment()
                {
                    Host = this.Host,
                    ActivityState = ActivityStates.Active,
                    ApartmentType = ApartmentTypes.Full,
                    CheckInTime = "14:00:00",
                    CheckOutTime = "10:00:00",
                    NumberOfGuests = 3,
                    NumberOfRooms = 4,
                    PricePerNight = 83,
                    Title = "Great apartment, affordable",
                    Location = new Location()
                    {
                        Latitude = 45.2615316,
                        Longitude = 19.8347492,
                        Address = new Address()
                        {
                            CityName = "Нови Сад",
                            CountryName = "RS",
                            PostalCode = "21101",
                            StreetName = "Булевар краља Петра I",
                            StreetNumber = "12"
                        }
                    }
                },
                new Apartment()
                {
                    Host = this.Host,
                    ActivityState = ActivityStates.Active,
                    ApartmentType = ApartmentTypes.SingleRoom,
                    CheckInTime = "14:00:00",
                    CheckOutTime = "10:00:00",
                    NumberOfGuests = 1,
                    NumberOfRooms = 1,
                    PricePerNight = 37,
                    Title = "The Pondhouse - A Magical Place",
                    Location = new Location()
                    {
                        Latitude = 48.865425,
                        Longitude = 2.3800042,
                        Address = new Address()
                        {
                            CityName = "Париз",
                            CountryName = "FR",
                            PostalCode = "75011",
                            StreetName = "Rue Dranem",
                            StreetNumber = "2"
                        }
                    }
                },
                new Apartment()
                {
                    Host = this.Host,
                    ActivityState = ActivityStates.Inactive,
                    ApartmentType = ApartmentTypes.Full,
                    CheckInTime = "14:00:00",
                    CheckOutTime = "10:00:00",
                    NumberOfGuests = 4,
                    NumberOfRooms = 3,
                    PricePerNight = 54,
                    Title = "Good location, nice view.",
                    Location = new Location()
                    {
                        Latitude = 45.2517311,
                        Longitude = 19.8362212,
                        Address = new Address()
                        {
                            CityName = "Нови Сад",
                            CountryName = "RS",
                            PostalCode = "21101",
                            StreetName = "Футошка",
                            StreetNumber = "8a"
                        }
                    }
                },
            };

            context.Apartments.AddRange(this.Apartments);
            context.SaveChanges();
        }

        private void SeedApartmentAmenities(ApartmentReservationDbContext context)
        {
            var apartmentAmenities = new[]
            {
                new ApartmentAmenity() {Apartment = Apartments[0], Amenity = Amenities[0]},
                new ApartmentAmenity() {Apartment = Apartments[0], Amenity = Amenities[1]},
                new ApartmentAmenity() {Apartment = Apartments[0], Amenity = Amenities[2]},
                new ApartmentAmenity() {Apartment = Apartments[0], Amenity = Amenities[3]},
                new ApartmentAmenity() {Apartment = Apartments[0], Amenity = Amenities[4]},
                new ApartmentAmenity() {Apartment = Apartments[1], Amenity = Amenities[2]},
                new ApartmentAmenity() {Apartment = Apartments[1], Amenity = Amenities[3]},
                new ApartmentAmenity() {Apartment = Apartments[1], Amenity = Amenities[4]}
            };

            context.ApartmentAmenities.AddRange(apartmentAmenities);
            context.SaveChanges();
        }

        private void SeedImages(ApartmentReservationDbContext context)
        {
            string[] imgUris = new[]
            {
                "https://www.onni.com/wp-content/uploads/2016/11/Rental-Apartment-Page-new-min.jpg",
                "https://arystudios.files.wordpress.com/2015/08/3dcontemperoryapartmentrenderingarchitecturalduskviewrealisticarystudios.jpg",
                "https://www.travelonline.com/melbourne/city-cbd/accommodation/adina-apartment-hotel-melbourne-flinders-street/penthouse-76880.jpg",
                "./images/apartment_1_pond_chair_stunning.JPG",
                "./images/apartment_1_pond_flowers.JPG",
                "./images/apartment_1_pond_view_from_bed.JPG"
            };

            this.Images = new[]
            {
                new Image(){Apartment = this.Apartments[0], ImageUri = imgUris[0]},
                new Image(){Apartment = this.Apartments[0], ImageUri = imgUris[1]},
                new Image(){Apartment = this.Apartments[2], ImageUri = imgUris[2]},
                new Image(){Apartment = this.Apartments[1], ImageUri = imgUris[3]},
                new Image(){Apartment = this.Apartments[1], ImageUri = imgUris[4]},
                new Image(){Apartment = this.Apartments[1], ImageUri = imgUris[5]}
            };

            context.Images.AddRange(this.Images);
            context.SaveChanges();
        }

        private void SeedForRentalDates(ApartmentReservationDbContext context)
        {
            var days = DateTimeHelpers.GetDateDayRange(this.MinDate, this.MaxDate);

            context.ForRentalDates.AddRange(days.Select(d => new ForRentalDate() { Apartment = this.Apartments[0], Date = d }).ToArray());
            context.ForRentalDates.AddRange(days.Select(d => new ForRentalDate() { Apartment = this.Apartments[1], Date = d }).ToArray());
            context.ForRentalDates.AddRange(days.Select(d => new ForRentalDate() { Apartment = this.Apartments[2], Date = d }).ToArray());

            context.SaveChanges();
        }

        private void SeedReservations(ApartmentReservationDbContext context)
        {
            this.Reservations = new[]
            {
                new Reservation()
                {
                    Apartment = this.Apartments[0],
                    Guest = this.Guests[0],
                    ReservationStartDate = MinDate,
                    NumberOfNightsRented = 3,
                    ReservationState = ReservationStates.Accepted,
                    TotalCost = 3 * this.Apartments[0].PricePerNight
                },
                new Reservation()
                {
                    Apartment = this.Apartments[0],
                    Guest = this.Guests[0],
                    ReservationStartDate = this.MinDate.AddDays(4),
                    NumberOfNightsRented = 1,
                    ReservationState = ReservationStates.Completed,
                    TotalCost = 1 * this.Apartments[0].PricePerNight
                },
                new Reservation()
                {
                    Apartment = this.Apartments[0],
                    Guest = this.Guests[0],
                    ReservationStartDate = MinDate,
                    NumberOfNightsRented = 2,
                    ReservationState = ReservationStates.Withdrawn,
                    TotalCost = 2 * this.Apartments[0].PricePerNight
                },
                new Reservation()
                {
                    Apartment = this.Apartments[0],
                    Guest = this.Guests[1],
                    ReservationStartDate = this.MinDate.AddDays(8),
                    NumberOfNightsRented = 2,
                    ReservationState = ReservationStates.Created,
                    TotalCost = 2 * this.Apartments[0].PricePerNight
                },
                new Reservation()
                {
                    Apartment = this.Apartments[1],
                    Guest = this.Guests[0],
                    ReservationStartDate = MinDate,
                    NumberOfNightsRented = 3,
                    ReservationState = ReservationStates.Denied,
                    TotalCost = 3 * this.Apartments[1].PricePerNight
                },
                 new Reservation()
                {
                    Apartment = this.Apartments[0],
                    Guest = this.Guests[1],
                    ReservationStartDate = MinDate,
                    NumberOfNightsRented = 3,
                    ReservationState = ReservationStates.Denied,
                    TotalCost = 3 * this.Apartments[0].PricePerNight
                }
            };

            context.Reservations.AddRange(this.Reservations);
            context.SaveChanges();
        }

        private void SeedComments(ApartmentReservationDbContext context)
        {
            var comments = new[]
            {
                new Comment()
                {
                    Apartment = Apartments[0],
                    Guest = Guests[0],
                    Approved = true,
                    Rating = 4,
                    Text = "Had fun, enjoyable experience. Neighbours were kinda anoying tho."
                },
                new Comment()
                {
                    Apartment = Apartments[0],
                    Guest = Guests[1],
                    Approved = false,
                    Rating = 1,
                    Text = "Bad experience! Would not recommend to anybody!"
                }
            };

            context.Comments.AddRange(comments);
            context.SaveChanges();
        }
    }
}