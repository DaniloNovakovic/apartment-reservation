using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace ApartmentReservation.Persistence
{
    public class ApartmentReservationDbContext : DbContext
    {
        public ApartmentReservationDbContext(DbContextOptions<ApartmentReservationDbContext> options)
            : base(options)
        { }

        public DbSet<User> Users { get; set; }
    }

    public class User
    {
        public string UserId { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public RoleType Role { get; set; }
    }

    public enum RoleType
    {
        Administrator,
        Host,
        Guest
    }
}