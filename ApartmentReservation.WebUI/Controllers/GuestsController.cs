using System.Security.Claims;
using System.Threading.Tasks;
using ApartmentReservation.Application.Features.Guests.Commands;
using ApartmentReservation.Application.Features.Guests.Queries;
using ApartmentReservation.Application.Infrastructure.Authentication;
using ApartmentReservation.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApartmentReservation.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = Policies.AdministratorOrGuestOnly)]
    public class GuestsController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IAuthService authService;

        public GuestsController(IMediator mediator, IAuthService authService)
        {
            this.mediator = mediator;
            this.authService = authService;
        }

        // GET: api/Guests
        [Authorize(Policy = Policies.AdministratorOnly)]
        public async Task<IActionResult> Get()
        {
            return this.Ok(await this.mediator.Send(new GetAllGuestsQuery()).ConfigureAwait(false));
        }

        // GET: api/Guests/5
        [HttpGet("{id}", Name = "GetGuest")]
        public async Task<IActionResult> Get(long id)
        {
            if (await authService.CheckIfBanned(this.User).ConfigureAwait(false))
            {
                return this.Forbid();
            }

            if (this.IsUserAStranger(id))
            {
                return this.Unauthorized();
            }

            return this.Ok(await this.mediator.Send(new GetGuestQuery() { Id = id }).ConfigureAwait(false));
        }

        // POST: api/Guests
        [HttpPost]
        [Authorize(Policy = Policies.AdministratorOnly)]
        public async Task<IActionResult> Post([FromBody] CreateGuestCommand command)
        {
            await this.mediator.Send(command).ConfigureAwait(false);
            return this.NoContent();
        }

        private bool IsUserAStranger(long id)
        {
            return !this.HttpContext.User.HasClaim(ClaimTypes.NameIdentifier, id.ToString())
                && !this.HttpContext.User.IsInRole(RoleNames.Administrator);
        }
    }
}