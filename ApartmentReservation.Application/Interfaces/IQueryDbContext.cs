using ApartmentReservation.Domain.Read.Models;
using MongoDB.Driver;

namespace ApartmentReservation.Application.Interfaces
{

    public interface IQueryDbContext
    {
        public IMongoCollection<ApartmentModel> Apartments { get; set; }
        public IMongoCollection<ReservationModel> Reservations { get; set; }
        public IMongoCollection<UserModel> Users { get; set; }

        /// <summary>
        /// Generates unique id of type int64 (auto-increment)
        /// </summary>
        /// <param name="collectionName">name of collection for who to generate id</param>
        /// <returns>New Id for collection</returns>
        public long GetNextSequence(string collectionName);
    }
}
