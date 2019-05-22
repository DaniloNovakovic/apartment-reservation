using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApartmentReservation.Persistence
{
    public class ApartmentReservationInitializer
    {
        public static void Initialize(ApartmentReservationDbContext context)
        {
            var initializer = new ApartmentReservationInitializer();
            initializer.SeedEverything(context);
        }

        private void SeedEverything(ApartmentReservationDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Administrators.Any())
            {
                return; // Db is seeded
            }
        }
    }
}