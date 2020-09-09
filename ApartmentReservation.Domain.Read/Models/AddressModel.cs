using ApartmentReservation.Domain.Read.Common;

namespace ApartmentReservation.Domain.Read.Models
{
    public class AddressModel : ValueObject<AddressModel>
    {
        public string CityName { get; set; }

        public string PostalCode { get; set; }

        public string StreetName { get; set; }

        public string StreetNumber { get; set; }

        public string CountryName { get; set; }

        protected override bool EqualsCore(AddressModel other)
        {
            return ToString().Equals(other.ToString());
        }

        protected override int GetHashCodeCore()
        {
            return ToString().GetHashCode();
        }

        public override string ToString()
        {
            return $"{CityName}-{PostalCode}-{StreetName}-{StreetNumber}-{CountryName}";
        }
    }
}
