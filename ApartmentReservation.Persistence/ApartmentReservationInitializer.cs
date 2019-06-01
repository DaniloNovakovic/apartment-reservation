using System;
using System.Collections.Generic;
using System.Linq;
using ApartmentReservation.Application.Infrastructure.Authentication;
using ApartmentReservation.Domain.Entities;

namespace ApartmentReservation.Persistence
{
    public class ApartmentReservationInitializer
    {
        private readonly Dictionary<long, Administrator> Administrators = new Dictionary<long, Administrator>();
        private readonly Dictionary<long, Guest> Guests = new Dictionary<long, Guest>();
        private readonly Dictionary<long, Host> Hosts = new Dictionary<long, Host>();
        private readonly Dictionary<long, User> Users = new Dictionary<long, User>();

        public static void Initialize(ApartmentReservationDbContext context)
        {
            var initializer = new ApartmentReservationInitializer();
            initializer.SeedEverything(context);
        }

        protected void SeedEverything(ApartmentReservationDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Administrators.Any())
            {
                return; // Db is seeded
            }

            this.SeedUsers(context);
            this.SeedAdministrators(context);
            this.SeedHosts(context);
            this.SeedGuests(context);
        }

        private void SeedAdministrators(ApartmentReservationDbContext context)
        {
            context.Administrators.AddRange(this.Users.Values
                .Where(u => u.RoleName == RoleNames.Administrator)
                .Select(u => new Administrator()
                {
                    User = u
                }));

            context.SaveChanges();
        }

        private void SeedGuests(ApartmentReservationDbContext context)
        {
            context.Guests.AddRange(this.Users.Values
                .Where(u => u.RoleName == RoleNames.Guest)
                .Select(u => new Guest()
                {
                    User = u
                }));

            context.SaveChanges();
        }

        private void SeedHosts(ApartmentReservationDbContext context)
        {
            context.Hosts.AddRange(this.Users.Values
                .Where(u => u.RoleName == RoleNames.Host)
                .Select(u => new Host()
                {
                    User = u
                }));

            context.SaveChanges();
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
    }
}