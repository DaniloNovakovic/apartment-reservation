namespace ApartmentReservation.Domain.Interfaces
{
    public interface ISyncable
    {
        public bool IsSyncNeeded { get; set; }
    }
}
