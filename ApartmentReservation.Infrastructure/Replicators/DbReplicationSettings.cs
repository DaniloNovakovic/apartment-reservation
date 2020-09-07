namespace ApartmentReservation.Infrastructure.Replicators
{
    public class DbReplicationSettings
    {
        public int MaxNumberOfEntitiesPerReplication { get; set; }
        public int CheckUsersUpdateTimeMs { get; set; }
    }
}
