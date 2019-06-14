namespace ApartmentReservation.Application.Features
{
    public class EntityCreatedResult
    {
        public long Id { get; set; }
    }

    public class EntityCreatedResult<TKey>
    {
        public TKey Id { get; set; }
    }
}