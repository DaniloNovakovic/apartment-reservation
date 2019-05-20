using System.Collections.Generic;

namespace ApartmentReservation.Domain.Entities
{
    public class Host
    {
        public Host()
        {
            this.ApartmentsForRental = new HashSet<Apartment>();
        }

        public string Id { get; set; }

        public ICollection<Apartment> ApartmentsForRental { get; set; }

        public User User { get; set; }

        public string UserId { get; set; }
    }
}