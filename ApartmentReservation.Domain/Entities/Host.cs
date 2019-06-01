using System.Collections.Generic;

namespace ApartmentReservation.Domain.Entities
{
    public class Host : Logical
    {
        public Host()
        {
            this.ApartmentsForRental = new HashSet<Apartment>();
        }

        public long UserId { get; set; }
        public User User { get; set; }
        public ICollection<Apartment> ApartmentsForRental { get; set; }
    }
}