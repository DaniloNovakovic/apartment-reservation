using System.Collections.Generic;
using ApartmentReservation.Domain.Interfaces;

namespace ApartmentReservation.Domain.Entities
{
    public class Guest : Logical, IUserRoleLogical
    {
        public Guest()
        {
            this.RentedApartments = new HashSet<Apartment>();
            this.Reservations = new HashSet<Reservation>();
        }

        public ICollection<Apartment> RentedApartments { get; set; }

        public ICollection<Reservation> Reservations { get; set; }

        public long UserId { get; set; }
        public User User { get; set; }
    }
}