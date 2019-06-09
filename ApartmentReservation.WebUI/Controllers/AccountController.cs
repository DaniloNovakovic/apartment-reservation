using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Features.Guests.Commands;
using ApartmentReservation.Application.Features.Users.Queries;
using ApartmentReservation.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> Login([FromBody] LoginUserDto dto)
        {
            await this.authService.LoginAsync(dto, this.HttpContext).ConfigureAwait(false);
            return this.Ok(await this.mediator.Send(new GetUserByUsernameQuery() { Username = dto.Username }));
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            string roleName = this.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Role)?.Value ?? "";
            await this.authService.LogoutAsync(roleName, this.HttpContext).ConfigureAwait(false);
            return this.NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] CreateGuestCommand command)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                await this.mediator.Send(command).ConfigureAwait(false);
            }

            return this.NoContent();
        }
    }
}