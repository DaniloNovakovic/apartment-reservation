using ApartmentReservation.Domain.Read.Common;

namespace ApartmentReservation.Domain.Read.Models
{
    public class ImageModel : ValueObject<ImageModel>
    {
        public string Uri { get; set; }

        protected override bool EqualsCore(ImageModel other)
        {
            return ToString().Equals(other.ToString());
        }

        protected override int GetHashCodeCore()
        {
            return ToString().GetHashCode();
        }

        public override string ToString()
        {
            return Uri;
        }
    }
}
