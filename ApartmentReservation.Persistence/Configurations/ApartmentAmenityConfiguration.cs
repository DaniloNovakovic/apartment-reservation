using ApartmentReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApartmentReservation.Persistence.Configurations
{
    public class ApartmentAmenityConfiguration : IEntityTypeConfiguration<ApartmentAmenity>
    {
        public void Configure(EntityTypeBuilder<ApartmentAmenity> builder)
        {
            builder.HasKey(a => new { a.ApartmentId, a.AmenityId });
        }
    }
}