using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ApartmentReservation.Domain.Entities;

namespace ApartmentReservation.Persistence.Configurations
{
    public class GuestConfiguration : IEntityTypeConfiguration<Guest>
    {
        public void Configure(EntityTypeBuilder<Guest> builder)
        {
            builder.Property(g => g.Password).IsRequired();

            builder.HasMany(g => g.RentedApartments);

            builder.HasMany(g => g.Reservations)
                .WithOne(g => g.Guest)
                .HasForeignKey(g => g.GuestId);
        }
    }
}