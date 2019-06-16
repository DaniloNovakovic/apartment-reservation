using ApartmentReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApartmentReservation.Persistence.Configurations
{
    public class AmenityConfiguration : IEntityTypeConfiguration<Amenity>
    {
        public void Configure(EntityTypeBuilder<Amenity> builder)
        {
            builder.Property(a => a.Name).IsRequired();

            builder.Ignore(a => a.Apartments);

            builder.HasMany(a => a.ApartmentAmenities)
                .WithOne(a => a.Amenity)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}