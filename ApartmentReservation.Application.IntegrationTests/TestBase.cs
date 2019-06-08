using System;
using ApartmentReservation.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ApartmentReservation.Application.IntegrationTests
{
    /// <summary>
    /// Inherit from this type to implement tests that have access to a service provider, empty
    /// in-memory database, and basic configuration.
    /// </summary>
    public abstract class TestBase
    {
        protected TestBase()
        {
            if (this.ServiceProvider == null)
            {
                IServiceCollection services = new ServiceCollection();

                // set up empty in-memory test db
                services
                  .AddEntityFrameworkInMemoryDatabase()
                  .AddDbContext<ApartmentReservationDbContext>(options =>
                  {
                      options.UseInMemoryDatabase("InMemory").UseInternalServiceProvider(services.BuildServiceProvider());
                      options.EnableSensitiveDataLogging();
                  });

                services.AddTransient(_ => AutoMapperFactory.Create());

                // set up service provider for tests
                this.ServiceProvider = services.BuildServiceProvider();
            }
        }

        protected IServiceProvider ServiceProvider { get; }

        // https://docs.efproject.net/en/latest/miscellaneous/testing.html
        protected DbContextOptions<ApartmentReservationDbContext> CreateNewContextOptions()
        {
            // Create a fresh service provider, and therefore a fresh InMemory database instance.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Create a new options instance telling the context to use an InMemory database and the
            // new service provider.
            var builder = new DbContextOptionsBuilder<ApartmentReservationDbContext>();
            builder.UseInMemoryDatabase("InMemory")
                   .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }
    }
}