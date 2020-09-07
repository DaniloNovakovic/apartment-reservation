using ApartmentReservation.Application.Interfaces;

namespace ApartmentReservation.Persistence.Read
{

    public class QueryDatabaseSettings : IQueryDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string UsersCollectionName { get; set; }
        public string ApartmentsCollectionName { get; set; }
        public string ReservationsCollectionName { get; set; }
    }
}
