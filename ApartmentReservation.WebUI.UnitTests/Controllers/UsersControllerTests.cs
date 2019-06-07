using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Features.Users.Queries;
using ApartmentReservation.WebUI.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace ApartmentReservation.WebUI.UnitTests.Controllers
{
    public class UsersControllerTests
    {
        [Fact]
        public async Task GetAll_WhenInvoked_CallsMediator()
        {
            var expectedValueResult = new List<UserDto> { new UserDto() { Username = "guest", Password = "guest" } };
            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(m => m.Send(It.IsAny<GetAllUsersQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedValueResult);

            var controller = new UsersController(mediatorMock.Object);

            var result = await controller.Get().ConfigureAwait(false);

            var okResult = Assert.IsAssignableFrom<OkObjectResult>(result);
            var value = Assert.IsAssignableFrom<IEnumerable<UserDto>>(okResult.Value);
            Assert.Equal(expectedValueResult, value);
            mediatorMock.Verify(m => m.Send(It.IsAny<GetAllUsersQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}