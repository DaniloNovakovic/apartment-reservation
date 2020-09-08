using ApartmentReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApartmentReservation.Persistence.Configurations
{
    public class ApartmentConfiguration : IEntityTypeConfiguration<Apartment>
    {
        public void Configure(EntityTypeBuilder<Apartment> builder)
        {
            builder.Ignore(a => a.Amenities);

            builder.HasMany(a => a.ApartmentAmenities)
                .WithOne(a => a.Apartment)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(a => a.Comments)
                .WithOne(c => c.Apartment)
                .HasForeignKey(c => c.ApartmentId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(a => a.ForRentalDates)
                .WithOne(d => d.Apartment)
                .HasForeignKey(d => d.ApartmentId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(a => a.Images)
                .WithOne(i => i.Apartment)
                .HasForeignKey(i => i.ApartmentId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Property(a => a.IsSyncNeeded).HasDefaultValue(true);
        }
    }
}