using ApartmentReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApartmentReservation.Persistence.Configurations
{
    public class GuestConfiguration : IEntityTypeConfiguration<Guest>
    {
        public void Configure(EntityTypeBuilder<Guest> builder)
        {
            builder.HasKey(a => a.UserId);

            builder.HasOne(a => a.User).WithMany()
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(g => g.Reservations)
                .WithOne(g => g.Guest)
                .HasForeignKey(g => g.GuestId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}