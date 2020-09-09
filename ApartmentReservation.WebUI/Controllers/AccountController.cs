using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Common.Exceptions;
using ApartmentReservation.Application.Features.Guests.Commands;
using ApartmentReservation.Application.Features.Users.Queries;
using ApartmentReservation.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApartmentReservation.WebUI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService authService;
        private readonly IMediator mediator;

        public AccountController(IAuthService authService, IMediator mediator)
        {
            this.authService = authService;
            this.mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginUserDto dto)
        {
            var userDto = await this.authService.LoginAsync(dto, this.HttpContext).ConfigureAwait(false);
            return this.Ok(userDto);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await this.authService.LogoutAsync(this.HttpContext).ConfigureAwait(false);
            return this.NoContent();
        }

        [HttpPost]
        [ProducesResponseType(typeof(GuestDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] CreateGuestCommand command)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                await this.authService.LogoutAsync(this.HttpContext).ConfigureAwait(false);
            }

            return this.Ok(await this.mediator.Send(command).ConfigureAwait(false));
        }
    }
}