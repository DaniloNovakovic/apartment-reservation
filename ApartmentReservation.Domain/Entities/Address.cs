namespace ApartmentReservation.Domain.Entities
{
    public class Address : Logical
    {
        public long Id { get; set; }

        public string CityName { get; set; } = "";

        public string PostalCode { get; set; } = "";

        public string StreetName { get; set; } = "";

        public string StreetNumber { get; set; } = "";

        public string CountryName { get; set; } = "";

        public override string ToString()
        {
            return $"{this.StreetName} {this.StreetNumber}, {this.CityName} {this.PostalCode}";
        }
    }
}