using System;
using ApartmentReservation.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ApartmentReservation.Application.IntegrationTests.Utils
{
    public static class ApartmentReservationContextFactory
    {
        public static ApartmentReservationDbContext Create()
        {
            var options = new DbContextOptionsBuilder<ApartmentReservationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new ApartmentReservationDbContext(options);

            context.Database.EnsureCreated();

            context.SaveChanges();

            return context;
        }

        public static void Destroy(ApartmentReservationDbContext context)
        {
            context.Database.EnsureDeleted();

            context.Dispose();
        }
    }
}