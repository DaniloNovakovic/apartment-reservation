namespace ApartmentReservation.Domain.Entities
{
    public class Address
    {
        public long Id { get; set; }

        public string CityName { get; set; }

        public string PostalCode { get; set; }

        public string StreetName { get; set; }

        public string StreetNumber { get; set; }

        public bool IsDeleted { get; set; } = false;

        public override string ToString()
        {
            return $"{this.StreetName} {this.StreetNumber}, {this.CityName} {this.PostalCode}";
        }
    }
}