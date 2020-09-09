namespace ApartmentReservation.Infrastructure.Replicators
{
    public class DbReplicationSettings
    {
        public int MaxNumberOfEntitiesPerReplication { get; set; }
        public int CheckUsersUpdateTimeMs { get; set; }
        public int CheckReservationsUpdateTimeMs { get; set; }
        public int CheckApartmentsUpdateTimeMs { get; set; }
        public int CheckAmenitiesUpdateTimeMs { get; set; }
    }
}
