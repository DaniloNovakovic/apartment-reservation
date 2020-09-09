using ApartmentReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApartmentReservation.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.Password).IsRequired();

            builder.HasAlternateKey(u => u.Username);

            builder.Property(u => u.IsSyncNeeded).HasDefaultValue(true);
        }
    }
}