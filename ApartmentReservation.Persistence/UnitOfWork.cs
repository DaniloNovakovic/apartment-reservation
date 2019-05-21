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
            this.Users = new Repository<User>(context);
        }

        public IRepository<User> Users { get; private set; }

        public int Complete()
        {
            return this.context.SaveChanges();
        }

        public void Dispose()
        {
            this.context.Dispose();
        }
    }
}