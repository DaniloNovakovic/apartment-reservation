using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Features.Hosts;
using ApartmentReservation.Application.Features.Hosts.Commands;
using ApartmentReservation.Application.Infrastructure.Authentication;
using ApartmentReservation.Application.Interfaces;
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
        private readonly IAuthService authService;

        public HostsController(IMediator mediator, IAuthService authService)
        {
            this.mediator = mediator;
            this.authService = authService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> Login([FromBody] UserDto dto)
        {
            await this.authService.LoginAsync(dto, RoleNames.Host, this.HttpContext);

            return this.NoContent();
        }

        // GET: api/Hosts
        [Authorize(Policy = Policies.AdministratorOnly)]
        public async Task<IActionResult> Get()
        {
            return Ok(await mediator.Send(new GetAllHostsQuery()).ConfigureAwait(false));
        }

        // GET: api/Hosts/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(string id)
        {
            if (IsUserAStranger(id))
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
            await mediator.Send(command).ConfigureAwait(false);
            return NoContent();
        }

        // PUT: api/Hosts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] UpdateHostCommand command)
        {
            if (IsUserAStranger(id))
            {
                return Unauthorized();
            }

            await mediator.Send(command).ConfigureAwait(false);

            return NoContent();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (IsUserAStranger(id))
            {
                return Unauthorized();
            }

            await mediator.Send(new DeleteHostCommand() { Id = id }).ConfigureAwait(false);

            return NoContent();
        }
    }
}