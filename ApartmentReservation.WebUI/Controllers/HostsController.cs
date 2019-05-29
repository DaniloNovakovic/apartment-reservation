using System.Security.Claims;
using System.Threading.Tasks;
using ApartmentReservation.Application.Features.Hosts;
using ApartmentReservation.Application.Features.Hosts.Commands;
using ApartmentReservation.Application.Infrastructure.Authentication;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApartmentReservation.WebUI.Controllers
{
    [Route("api/hosts")]
    [ApiController]
    [Authorize(Policy = Policies.AdministratorOrHostOnly)]
    public class HostsController : ControllerBase
    {
        private readonly IMediator mediator;

        public HostsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        // GET: api/Hosts
        [Authorize(Policy = Policies.AdministratorOnly)]
        public async Task<IActionResult> Get()
        {
            return this.Ok(await this.mediator.Send(new GetAllHostsQuery()).ConfigureAwait(false));
        }

        // GET: api/Hosts/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(string id)
        {
            if (this.IsUserAStranger(id))
            {
                return this.Unauthorized();
            }

            return this.Ok(await this.mediator.Send(new GetHostQuery() { Id = id }).ConfigureAwait(false));
        }

        private bool IsUserAStranger(string id)
        {
            return !this.HttpContext.User.HasClaim(ClaimTypes.NameIdentifier, id) && !this.HttpContext.User.IsInRole("Administrator");
        }

        // POST: api/Hosts
        [HttpPost]
        [Authorize(Policy = Policies.AdministratorOnly)]
        public async Task<IActionResult> Post([FromBody] CreateHostCommand command)
        {
            await this.mediator.Send(command).ConfigureAwait(false);
            return this.NoContent();
        }

        // PUT: api/Hosts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] UpdateHostCommand command)
        {
            if (this.IsUserAStranger(id))
            {
                return this.Unauthorized();
            }

            await this.mediator.Send(command).ConfigureAwait(false);

            return this.NoContent();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (this.IsUserAStranger(id))
            {
                return this.Unauthorized();
            }

            await this.mediator.Send(new DeleteHostCommand() { Id = id }).ConfigureAwait(false);

            return this.NoContent();
        }
    }
}