using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApartmentReservation.Application.Interfaces
{
    public interface IApartmentReservationDbContext
    {
        DbSet<Address> Addresses { get; set; }
        DbSet<Administrator> Administrators { get; set; }
        DbSet<Amenity> Amenities { get; set; }
        DbSet<Apartment> Apartments { get; set; }
        DbSet<Comment> Comments { get; set; }
        DbSet<ForRentalDate> ForRentalDates { get; set; }
        DbSet<Guest> Guests { get; set; }
        DbSet<Host> Hosts { get; set; }
        DbSet<Image> Images { get; set; }
        DbSet<Location> Locations { get; set; }
        DbSet<Reservation> Reservations { get; set; }
        DbSet<User> Users { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}