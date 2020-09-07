using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ApartmentReservation.Domain.Read.Common
{
    public abstract class Record
    {
        [BsonId]
        [BsonRepresentation(BsonType.Int64)]
        public virtual long Id { get; set; }
    }
}
