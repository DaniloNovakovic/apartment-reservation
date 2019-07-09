﻿using System.Security.Claims;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Features.Users.Commands;
using ApartmentReservation.Application.Features.Users.Queries;
using ApartmentReservation.Application.Infrastructure.Authentication;
using ApartmentReservation.Application.Interfaces;
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
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Get([FromQuery]GetAllUsersQuery query)
        {
            return this.Ok(await this.mediator.Send(query).ConfigureAwait(false));
        }

        // GET: api/Users/5
        [HttpGet("{id}", Name = "GetUser")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

            return this.Ok(await this.mediator.Send(new GetUserByIdQuery() { Id = id }).ConfigureAwait(false));
        }

        [HttpGet("{id}/Ban")]
        [Authorize(Policy = Policies.AdministratorOnly)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> BanUser(long id)
        {
            var request = new BanUserCommand() { UserId = id };
            await this.mediator.Send(request).ConfigureAwait(false);
            return this.Ok();
        }

        [HttpGet("{id}/Unban")]
        [Authorize(Policy = Policies.AdministratorOnly)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UnbanUser(long id)
        {
            var request = new UnbanUserCommand() { UserId = id };
            await this.mediator.Send(request).ConfigureAwait(false);
            return this.Ok();
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(long id, [FromBody] UpdateUserCommand command)
        {
            if (await authService.CheckIfBanned(this.User).ConfigureAwait(false))
            {
                return this.Forbid();
            }

            if (this.IsUserAStranger(id))
            {
                return this.Unauthorized();
            }

            command.Id = id;

            await this.mediator.Send(command).ConfigureAwait(false);

            return this.NoContent();
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        [Authorize(Policy = Policies.AdministratorOnly)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(long id)
        {
            if (this.IsUserAStranger(id))
            {
                return this.Unauthorized();
            }

            await this.mediator.Send(new DeleteUserCommand() { Id = id }).ConfigureAwait(false);

            return this.NoContent();
        }

        private bool IsUserAStranger(long id)
        {
            return !this.HttpContext.User.HasClaim(ClaimTypes.NameIdentifier, id.ToString())
                && !this.HttpContext.User.IsInRole(RoleNames.Administrator);
        }
    }
}