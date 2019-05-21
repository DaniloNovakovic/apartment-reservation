using System;

namespace ApartmentReservation.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        int Complete();
    }
}