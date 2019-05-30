using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Interfaces;
using ApartmentReservation.Application.Interfaces.Repositories;
using ApartmentReservation.Domain.Entities;
using ApartmentReservation.Persistence.Repositories;

namespace ApartmentReservation.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApartmentReservationDbContext context;

        public UnitOfWork(ApartmentReservationDbContext context)
        {
            this.context = context;
            this.Hosts = new Repository<Host>(context);
            this.Administrators = new Repository<Administrator>(context);
            this.Users = new Repository<User>(context);
        }

        public IRepository<Address> Addresses { get; }
        public IRepository<Administrator> Administrators { get; }
        public IRepository<Amenity> Amenities { get; }
        public IRepository<Apartment> Apartments { get; }
        public IRepository<Comment> Comments { get; }
        public IRepository<Guest> Guests { get; }
        public IRepository<Host> Hosts { get; }
        public IRepository<Image> Images { get; }
        public IRepository<Location> Locations { get; }
        public IRepository<Reservation> Reservations { get; }
        public IRepository<ForRentalDate> ForRentalDates { get; }
        public IRepository<User> Users { get; }

        public async Task<int> CompleteAsync(CancellationToken cancellationToken = default)
        {
            return await this.context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public void Dispose()
        {
            this.context.Dispose();
        }
    }
}