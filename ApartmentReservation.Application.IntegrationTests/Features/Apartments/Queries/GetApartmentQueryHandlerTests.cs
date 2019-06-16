using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Exceptions;
using ApartmentReservation.Application.Features.Apartments.Queries;
using ApartmentReservation.Application.Infrastructure.Authentication;
using ApartmentReservation.Domain.Constants;
using ApartmentReservation.Domain.Entities;
using Xunit;

namespace ApartmentReservation.Application.IntegrationTests.Features.Apartments.Queries
{
    public class GetApartmentQueryDataSetup : InMemoryContextTestBase
    {
        public Apartment DbApartment { get; private set; }
        public Host DbHost { get; private set; }

        protected override void LoadTestData()
        {
            this.DbHost = this.Context.Add(new Host()
            {
                User = new User()
                {
                    Username = "host",
                    Password = "host",
                    RoleName = RoleNames.Host
                }
            }).Entity;

            this.Context.SaveChanges();

            DbApartment = Context.Add(new Apartment()
            {
                ApartmentType = ApartmentTypes.Full,
                ActivityState = ActivityStates.Active,
                Host = DbHost
            }).Entity;

            var amenities = new[]
            {
                new Amenity(){Name="TV"},
                new Amenity(){Name="Heating"}
            };

            Context.AddRange(
                new ApartmentAmenity() { Apartment = DbApartment, Amenity = amenities[0] },
                new ApartmentAmenity() { Apartment = DbApartment, Amenity = amenities[1] }
            );

            this.Context.SaveChanges();
        }
    }

    public class GetApartmentQueryHandlerTests : IClassFixture<GetApartmentQueryDataSetup>
    {
        private readonly Apartment dbApartment;
        private readonly Host dbHost;
        private readonly GetApartmentQueryHandler sut;

        public GetApartmentQueryHandlerTests(GetApartmentQueryDataSetup data)
        {
            this.dbApartment = data.DbApartment;
            this.dbHost = data.DbHost;
            this.sut = new GetApartmentQueryHandler(data.Context);
        }

        [Fact]
        public async Task WhenExists_ReturnsApartmentWithRequestedId()
        {
            var request = new GetApartmentQuery() { Id = dbApartment.Id };

            var result = await sut.Handle(request, CancellationToken.None).ConfigureAwait(false);

            Assert.NotNull(result);
            Assert.Equal(dbApartment.Id, result.Id);
        }

        [Fact]
        public async Task WhenDoesNotExist_ThrowNotFoundException()
        {
            var request = new GetApartmentQuery() { Id = -1 };
            await Assert
                .ThrowsAsync<NotFoundException>(async () => await sut.Handle(request, CancellationToken.None).ConfigureAwait(false))
                .ConfigureAwait(false);
        }
    }
}