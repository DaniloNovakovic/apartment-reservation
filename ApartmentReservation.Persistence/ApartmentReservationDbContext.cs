using ApartmentReservation.Application.Interfaces;
using ApartmentReservation.Domain.Entities;
using ApartmentReservation.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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
        public DbSet<ApartmentAmenity> ApartmentAmenities { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<ForRentalDate> ForRentalDates { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Host> Hosts { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<User> Users { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entities = ChangeTracker
                .Entries()
                .Where(e => e.Entity is ISyncable
                            && (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entityEntry in entities)
            {
                var prevSyncNeeded = entityEntry.OriginalValues.GetValue<bool>(nameof(ISyncable.IsSyncNeeded));
                var currSyncNeeded = entityEntry.CurrentValues.GetValue<bool>(nameof(ISyncable.IsSyncNeeded));

                if (prevSyncNeeded && !currSyncNeeded)
                    continue; // IsSyncNeeded was set to false by replicator

                ((ISyncable)entityEntry.Entity).IsSyncNeeded = true;
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApartmentReservationDbContext).Assembly);
        }
    }
}