using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Features.Users.Commands;
using ApartmentReservation.Application.Features.Users.Queries;
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
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IAuthService authService;

        public UsersController(IMediator mediator, IAuthService authService)
        {
            this.mediator = mediator;
            this.authService = authService;
        }

        // GET: api/Users
        [Authorize(Policy = Policies.AdministratorOnly)]
        [ProducesResponseType(typeof(IEnumerable<UserDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Get([FromQuery]GetAllUsersQuery query)
        {
            return Ok(await mediator.Send(query).ConfigureAwait(false));
        }

        // GET: api/Users/5
        [HttpGet("{id}", Name = "GetUser")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(long id)
        {
            if (await authService.CheckIfBanned(User).ConfigureAwait(false))
            {
                return Forbid();
            }

            if (IsUserAStranger(id))
            {
                return Unauthorized();
            }

            return Ok(await mediator.Send(new GetUserByIdQuery() { Id = id }).ConfigureAwait(false));
        }

        [HttpGet("{id}/Ban")]
        [Authorize(Policy = Policies.AdministratorOnly)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> BanUser(long id)
        {
            var request = new BanUserCommand() { UserId = id };
            await mediator.Send(request).ConfigureAwait(false);
            return Ok();
        }

        [HttpGet("{id}/Unban")]
        [Authorize(Policy = Policies.AdministratorOnly)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UnbanUser(long id)
        {
            var request = new UnbanUserCommand() { UserId = id };
            await mediator.Send(request).ConfigureAwait(false);
            return Ok();
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(long id, [FromBody] UpdateUserCommand command)
        {
            if (await authService.CheckIfBanned(User).ConfigureAwait(false))
            {
                return Forbid();
            }

            if (IsUserAStranger(id))
            {
                return Unauthorized();
            }

            command.Id = id;

            await mediator.Send(command).ConfigureAwait(false);

            return NoContent();
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        [Authorize(Policy = Policies.AdministratorOnly)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(long id)
        {
            if (IsUserAStranger(id))
            {
                return Unauthorized();
            }

            await mediator.Send(new DeleteUserCommand() { Id = id }).ConfigureAwait(false);

            return NoContent();
        }

        private bool IsUserAStranger(long id)
        {
            return !HttpContext.User.HasClaim(ClaimTypes.NameIdentifier, id.ToString())
                && !HttpContext.User.IsInRole(RoleNames.Administrator);
        }
    }
}