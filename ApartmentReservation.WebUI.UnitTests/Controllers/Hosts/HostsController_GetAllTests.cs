using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Common.Constants;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace ApartmentReservation.WebUI.UnitTests.Controllers.Hosts
{
    public class HostsController_GetAllTests : HostsControllerTestsBase
    {
        private readonly long userId = 1;

        [Fact]
        public async Task GetAll_WhenInvoked_ReturnHostDtosFromMediator()
        {
            // Arrange
            var expectedResultValue = new List<HostDto> { new HostDto() };

            this.mediatorMock
                .Setup(m => m.Send(It.IsAny<IRequest<IEnumerable<HostDto>>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResultValue);

            var controller = this.CreateController(this.userId, RoleNames.Administrator);

            // Act
            var result = await controller.Get();

            // Assert
            var okResult = Assert.IsAssignableFrom<OkObjectResult>(result);
            var value = Assert.IsAssignableFrom<IEnumerable<HostDto>>(okResult.Value);
            Assert.Equal(expectedResultValue, value);
        }
    }
}