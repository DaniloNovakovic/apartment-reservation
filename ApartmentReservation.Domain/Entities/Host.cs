using System.Collections.Generic;

namespace ApartmentReservation.Domain.Entities
{
    public class Host : User
    {
        public Host()
        {
            this.ApartmentsForRental = new HashSet<Apartment>();
        }

        public ICollection<Apartment> ApartmentsForRental { get; set; }
    }
}