using ApartmentReservation.Application.Interfaces;
using ApartmentReservation.Domain.Entities;
using ApartmentReservation.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace ApartmentReservation.Persistence
{
    public class ApartmentReservationDbContext : DbContext, IApartmentReservationDbContext
    {
        public ApartmentReservationDbContext(DbContextOptions<ApartmentReservationDbContext> options)
            : base(options)
        { }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Amenity> Amenities { get; set; }
        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Host> Hosts { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AddressConfiguration());
            modelBuilder.ApplyConfiguration(new AdministratorConfiguration());
            modelBuilder.ApplyConfiguration(new AmenityConfiguration());
            modelBuilder.ApplyConfiguration(new ApartmentConfiguration());
            modelBuilder.ApplyConfiguration(new CommentConfiguration());
            modelBuilder.ApplyConfiguration(new GuestConfiguration());
            modelBuilder.ApplyConfiguration(new HostConfiguration());
            modelBuilder.ApplyConfiguration(new LocationConfiguration());
            modelBuilder.ApplyConfiguration(new ReservationConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}