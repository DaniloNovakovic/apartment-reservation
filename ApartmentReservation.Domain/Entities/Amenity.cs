namespace ApartmentReservation.Domain.Entities
{
    public class Amenity
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}