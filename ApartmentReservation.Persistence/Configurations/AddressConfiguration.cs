using ApartmentReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApartmentReservation.Persistence.Configurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.Property(a => a.CityName).IsRequired();
            builder.Property(a => a.PostalCode).IsRequired();
            builder.Property(a => a.StreetName).IsRequired();
            builder.Property(a => a.StreetNumber).IsRequired();
        }
    }
}