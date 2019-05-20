using System.Collections.Generic;

namespace ApartmentReservation.Domain.Entities
{
    public class Guest
    {
        public Guest()
        {
            this.RentedApartments = new HashSet<Apartment>();
            this.Reservations = new HashSet<Reservation>();
        }

        public string Id { get; set; }

        public ICollection<Apartment> RentedApartments { get; set; }

        public ICollection<Reservation> Reservations { get; set; }

        public User User { get; set; }

        public string UserId { get; set; }
    }
}