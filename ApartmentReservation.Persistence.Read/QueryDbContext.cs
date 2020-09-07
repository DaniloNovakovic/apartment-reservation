using ApartmentReservation.Application.Interfaces;
using ApartmentReservation.Domain.Read.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace ApartmentReservation.Persistence.Read
{
    public class QueryDbContext : IQueryDbContext
    {
        private readonly IMongoCollection<Counter> _counters;
        private readonly IMongoDatabase _db;

        public QueryDbContext(IQueryDatabaseSettings dbSettings)
        {
            var client = new MongoClient(dbSettings.ConnectionString);
            _db = client.GetDatabase(dbSettings.DatabaseName);

            Users = _db.GetCollection<UserModel>(dbSettings.UsersCollectionName);
            Apartments = _db.GetCollection<ApartmentModel>(dbSettings.ApartmentsCollectionName);
            Reservations = _db.GetCollection<ReservationModel>(dbSettings.ReservationsCollectionName);
            _counters = _db.GetCollection<Counter>("Counters");
        }

        public IMongoCollection<ApartmentModel> Apartments { get; set; }
        public IMongoCollection<ReservationModel> Reservations { get; set; }
        public IMongoCollection<UserModel> Users { get; set; }

        public long GetNextSequence(string collectionName)
        {
            Counter ret = _counters.FindOneAndUpdate<Counter>(
                c => c.Id == collectionName,
                Builders<Counter>.Update.Inc(c => c.Sequence, 1),
                new FindOneAndUpdateOptions<Counter, Counter> { IsUpsert = true });

            return ret.Sequence;
        }

        private class Counter
        {
            [BsonId]
            [BsonRepresentation(BsonType.String)]
            public string Id { get; set; }

            public long Sequence { get; set; } = 0;
        }
    }
}
