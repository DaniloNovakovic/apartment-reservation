using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Features.Users.Queries;
using ApartmentReservation.Common.Constants;
using ApartmentReservation.WebUI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace ApartmentReservation.WebUI.UnitTests.Controllers.Users
{
    public class UsersController_GetAllTests : UsersControllerTestsBase
    {
        [Fact]
        public async Task WhenInvoked_CallsMediator()
        {
            var expectedValueResult = new List<UserDto> { new UserDto() { Username = "guest", Password = "guest" } };
            var query = new GetAllUsersQuery() { Gender = Genders.Male, RoleName = RoleNames.Guest };
            var controller = new UsersController(this.mediatorMock.Object, this.authServiceMock.Object);
            this.mediatorMock.Setup(m => m.Send(query, It.IsAny<CancellationToken>())).ReturnsAsync(expectedValueResult);

            var result = await controller.Get(query).ConfigureAwait(false);

            var okResult = Assert.IsAssignableFrom<OkObjectResult>(result);
            var value = Assert.IsAssignableFrom<IEnumerable<UserDto>>(okResult.Value);
            Assert.Equal(expectedValueResult, value);
            this.mediatorMock.Verify(m => m.Send(query, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}