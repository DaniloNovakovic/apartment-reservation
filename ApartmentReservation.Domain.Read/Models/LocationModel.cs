using ApartmentReservation.Domain.Read.Common;

namespace ApartmentReservation.Domain.Read.Models
{
    public class LocationModel : ValueObject<LocationModel>
    {
        public AddressModel Address { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        protected override bool EqualsCore(LocationModel other)
        {
            return ToString().Equals(other.ToString());
        }

        protected override int GetHashCodeCore()
        {
            return ToString().GetHashCode();
        }

        public override string ToString()
        {
            return $"({Latitude},{Longitude})-{Address}";
        }
    }
}
