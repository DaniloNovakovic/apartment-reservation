using System;
using System.Collections.Generic;
using System.Text;
using ApartmentReservation.Domain.Entities;
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
}