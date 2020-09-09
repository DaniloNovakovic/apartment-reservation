namespace ApartmentReservation.Application.Interfaces
{
    public interface IQueryDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string ApartmentsCollectionName { get; set; }
        string ReservationsCollectionName { get; set; }
        string UsersCollectionName { get; set; }
    }
}
