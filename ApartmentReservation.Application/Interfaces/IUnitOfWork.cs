using System;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Interfaces.Repositories;
using ApartmentReservation.Domain.Entities;

namespace ApartmentReservation.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> CompleteAsync(CancellationToken cancellationToken = default);

        IRepository<Address> Addresses { get;  }
        IRepository<Administrator> Administrators { get;  }
        IRepository<Amenity> Amenities { get;  }
        IRepository<Apartment> Apartments { get;  }
        IRepository<Comment> Comments { get;  }
        IRepository<Guest> Guests { get;  }
        IRepository<Host> Hosts { get;  }
        IRepository<Image> Images { get;  }
        IRepository<Location> Locations { get;  }
        IRepository<Reservation> Reservations { get;  }
        IRepository<ForRentalDate> ForRentalDates { get;  }
    }
}