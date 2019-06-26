using ApartmentReservation.Domain.Interfaces;

namespace ApartmentReservation.Domain.Entities
{
    public class Logical : ILogical
    {
        public bool IsDeleted { get; set; }

        public Logical()
        {
            this.IsDeleted = false;
        }
    }
}