using ApartmentReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApartmentReservation.Persistence.Configurations
{
    public class HostConfiguration : IEntityTypeConfiguration<Host>
    {
        public void Configure(EntityTypeBuilder<Host> builder)
        {
            builder.Property(h => h.Password).IsRequired();

            builder.HasMany(h => h.ApartmentsForRental)
                .WithOne(a => a.Host)
                .HasForeignKey(a => a.HostId);
        }
    }
}