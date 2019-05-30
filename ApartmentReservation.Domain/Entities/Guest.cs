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

        public ICollection<Apartment> RentedApartments { get; set; }

        public ICollection<Reservation> Reservations { get; set; }

        public long UserId { get; set; }
        public User User { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}