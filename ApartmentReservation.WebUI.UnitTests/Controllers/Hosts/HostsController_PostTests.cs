using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Features.Hosts.Commands;
using ApartmentReservation.Application.Infrastructure.Authentication;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace ApartmentReservation.WebUI.UnitTests.Controllers.Hosts
{
    public class HostsController_PostTests : HostsControllerTestsBase
    {
        private readonly long userId = 1;

        [Fact]
        public async Task Post_WhenInvoked_SendCreateCommandToMediator()
        {
            var controller = this.CreateController(this.userId, RoleNames.Administrator);
            var createCommand = new CreateHostCommand() { Username = "Djura", Password = "123" };

            await controller.Post(createCommand);

            this.mediatorMock.Verify(m => m.Send(It.Is<CreateHostCommand>(c => c == createCommand), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}