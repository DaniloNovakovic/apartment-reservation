using System;
using System.Collections.Generic;
using System.Text;
using ApartmentReservation.Application.Features.Apartments.Queries;
using ApartmentReservation.Domain.Entities;
using Xunit;

namespace ApartmentReservation.Application.IntegrationTests.Features.Apartments.Queries
{
    public class GetAllApartmentsQueryHandlerTests : InMemoryContextTestBase
    {
        private GetAllApartmentsQueryHandler sut;

        public GetAllApartmentsQueryHandlerTests()
        {
            this.sut = new GetAllApartmentsQueryHandler(this.Context);
        }

        protected override void LoadTestData()
        {
        }
    }
}