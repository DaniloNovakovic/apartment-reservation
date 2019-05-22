using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ApartmentReservation.Domain.Entities;

namespace ApartmentReservation.Persistence.Configurations
{
    public class ApartmentConfiguration : IEntityTypeConfiguration<Apartment>
    {
        public void Configure(EntityTypeBuilder<Apartment> builder)
        {
            builder.HasMany(a => a.Amenities);

            builder.HasMany(a => a.Comments)
                .WithOne(c => c.Apartment)
                .HasForeignKey(c => c.ApartmentId);

            builder.HasMany(a => a.ForRentalDates)
                .WithOne(d => d.Apartment)
                .HasForeignKey(d => d.ApartmentId);

            builder.HasMany(a => a.Images)
                .WithOne(i => i.Apartment)
                .HasForeignKey(i => i.ApartmentId);
        }
    }
}