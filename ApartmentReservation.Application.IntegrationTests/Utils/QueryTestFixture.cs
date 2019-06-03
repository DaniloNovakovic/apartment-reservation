using System;
using ApartmentReservation.Persistence;
using AutoMapper;
using Xunit;

namespace ApartmentReservation.Application.IntegrationTests.Utils
{
    public class QueryTestFixture : IDisposable
    {
        public ApartmentReservationDbContext Context { get; }
        public IMapper Mapper { get; }

        public QueryTestFixture()
        {
            this.Context = ApartmentReservationContextFactory.Create();
            this.Mapper = AutoMapperFactory.Create();
        }

        public void Dispose()
        {
            ApartmentReservationContextFactory.Destroy(this.Context);
        }
    }

    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
}