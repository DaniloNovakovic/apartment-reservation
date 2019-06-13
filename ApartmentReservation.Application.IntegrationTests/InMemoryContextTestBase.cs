using System.Threading.Tasks;
using ApartmentReservation.Persistence;
using AutoMapper;

namespace ApartmentReservation.Application.IntegrationTests
{
    /// <summary>
    /// Inherit from this type to implement tests that make use of the in-memory test database context.
    /// </summary>
    public abstract class InMemoryContextTestBase : TestBase
    {
        /// <summary>
        /// Gets the in-memory database context.
        /// </summary>
        public ApartmentReservationDbContext Context { get; }

        public IMapper Mapper { get; }

        protected InMemoryContextTestBase()
        {
            this.Context = (ApartmentReservationDbContext)this.ServiceProvider.GetService(typeof(ApartmentReservationDbContext));
            this.Mapper = (IMapper)this.ServiceProvider.GetService(typeof(IMapper));
            this.LoadTestData();
        }

        /// <summary>
        /// Override this method to load test data into the in-memory database context prior to any
        /// tests being executed in your test class.
        /// </summary>
        protected virtual void LoadTestData()
        {
        }

        /// <summary>
        /// Override this method to load test data into the in-memory database context prior to any
        /// tests being executed in your test class.
        /// FRAGILE: this method can't be called from the constructor so you must call it manually
        /// </summary>
        protected virtual async Task LoadTestDataAsync()
        {
            await Task.Delay(0); //To prevent compiler warning
        }
    }
}