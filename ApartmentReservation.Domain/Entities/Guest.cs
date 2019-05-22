using System.Collections.Generic;

namespace ApartmentReservation.Domain.Entities
{
    public class Guest : User
    {
        public Guest()
        {
            this.RentedApartments = new HashSet<Apartment>();
            this.Reservations = new HashSet<Reservation>();
        }

        public ICollection<Apartment> RentedApartments { get; set; }

        public ICollection<Reservation> Reservations { get; set; }
    }
}