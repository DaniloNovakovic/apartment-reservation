using System.Security.Claims;
using System.Threading.Tasks;
using ApartmentReservation.Application.Features.Guests.Commands;
using ApartmentReservation.Application.Features.Guests.Queries;
using ApartmentReservation.Application.Infrastructure.Authentication;
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

        public GuestsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        // GET: api/Guests
        [Authorize(Policy = Policies.AdministratorOnly)]
        public async Task<IActionResult> Get()
        {
            return this.Ok(await this.mediator.Send(new GetAllGuestsQuery()).ConfigureAwait(false));
        }

        // GET: api/Hosts/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(long id)
        {
            if (this.IsUserAStranger(id))
            {
                return this.Unauthorized();
            }

            return this.Ok(await this.mediator.Send(new GetGuestQuery() { Id = id }).ConfigureAwait(false));
        }

        private bool IsUserAStranger(long id)
        {
            return !this.HttpContext.User.HasClaim(ClaimTypes.NameIdentifier, id.ToString())
                && !this.HttpContext.User.IsInRole(RoleNames.Administrator);
        }

        // POST: api/Guests
        [HttpPost]
        [Authorize(Policy = Policies.AdministratorOnly)]
        public async Task<IActionResult> Post([FromBody] CreateGuestCommand command)
        {
            await this.mediator.Send(command).ConfigureAwait(false);
            return this.NoContent();
        }

        // PUT: api/Guests/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, [FromBody] UpdateGuestCommand command)
        {
            if (this.IsUserAStranger(id))
            {
                return this.Unauthorized();
            }

            await this.mediator.Send(command).ConfigureAwait(false);

            return this.NoContent();
        }

        // DELETE: api/Guests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            if (this.IsUserAStranger(id))
            {
                return this.Unauthorized();
            }

            await this.mediator.Send(new DeleteGuestCommand() { Id = id }).ConfigureAwait(false);

            return this.NoContent();
        }
    }
}