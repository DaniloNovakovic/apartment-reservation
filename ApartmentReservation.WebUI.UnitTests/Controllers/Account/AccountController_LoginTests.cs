﻿using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Features.Guests.Commands;
using ApartmentReservation.Application.Infrastructure.Authentication;
using Moq;
using Xunit;

namespace ApartmentReservation.WebUI.UnitTests.Controllers.Account
{
    public class AccountController_LoginTests : AccountControllerTestsBase
    {
        [Fact]
        public async Task Login_WhenInvoked_CallsAuthService()
        {
            var controller = this.GetUnauthenticatedController();
            var user = new LoginUserDto() { Username = "admin", Password = "admin" };

            await controller.Login(user).ConfigureAwait(false);

            this.authServiceMock.Verify(a => a.LoginAsync(user, controller.HttpContext));
        }
    }
}