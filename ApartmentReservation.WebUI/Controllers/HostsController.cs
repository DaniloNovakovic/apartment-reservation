using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Features.Hosts;
using ApartmentReservation.Application.Features.Hosts.Commands;
using ApartmentReservation.Application.Interfaces;
using ApartmentReservation.Common.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApartmentReservation.WebUI.Controllers
{
    [Route("api/[controller]")]
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

        // GET: api/Hosts
        [Authorize(Policy = Policies.AdministratorOnly)]
        [ProducesResponseType(typeof(IEnumerable<HostDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            return this.Ok(await this.mediator.Send(new GetAllHostsQuery()).ConfigureAwait(false));
        }

        // GET: api/Hosts/5
        [HttpGet("{id}", Name = "GetHost")]
        [ProducesResponseType(typeof(HostDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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

            return this.Ok(await this.mediator.Send(new GetHostQuery() { Id = id }).ConfigureAwait(false));
        }

        // POST: api/Hosts
        [HttpPost]
        [Authorize(Policy = Policies.AdministratorOnly)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Post([FromBody] CreateHostCommand command)
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