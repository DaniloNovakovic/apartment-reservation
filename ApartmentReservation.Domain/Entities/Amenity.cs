using ApartmentReservation.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ApartmentReservation.Domain.Entities
{
    public class Amenity : Logical, ISyncable
    {
        public Amenity()
        {
            this.ApartmentAmenities = new HashSet<ApartmentAmenity>();
        }

        public long Id { get; set; }

        public string Name { get; set; }

        public ICollection<ApartmentAmenity> ApartmentAmenities { get; set; }
        public IEnumerable<Apartment> Apartments => this.ApartmentAmenities.Where(x => !x.IsDeleted).Select(a => a.Apartment);

        public bool IsSyncNeeded { get; set; }
    }
}