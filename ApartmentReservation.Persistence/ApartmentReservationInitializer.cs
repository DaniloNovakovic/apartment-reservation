using System.Collections.Generic;
using System.Linq;
using ApartmentReservation.Application.Infrastructure.Authentication;
using ApartmentReservation.Domain.Entities;

namespace ApartmentReservation.Persistence
{
    public class ApartmentReservationInitializer
    {
        private IEnumerable<Administrator> Administrators = new List<Administrator>();
        private IEnumerable<Guest> Guests = new List<Guest>();
        private IEnumerable<Host> Hosts = new List<Host>();
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
            this.Administrators = this.Users.Values
                .Where(u => u.RoleName == RoleNames.Administrator)
                .Select(u => new Administrator() { User = u });

            context.Administrators.AddRange(this.Administrators);

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

        private void SeedHosts(ApartmentReservationDbContext context)
        {
            this.Hosts = this.Users.Values
                .Where(u => u.RoleName == RoleNames.Host)
                .Select(u => new Host() { User = u });

            context.Hosts.AddRange(this.Hosts);

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