﻿using System.Security.Claims;
using System.Threading.Tasks;
using ApartmentReservation.Application.Features.Users.Commands;
using ApartmentReservation.Application.Features.Users.Queries;
using ApartmentReservation.Application.Infrastructure.Authentication;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApartmentReservation.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IMediator mediator;

        public UsersController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        // GET: api/Users
        [Authorize(Policy = Policies.AdministratorOnly)]
        public async Task<IActionResult> Get([FromQuery]GetAllUsersQuery query)
        {
            return this.Ok(await this.mediator.Send(query).ConfigureAwait(false));
        }

        // GET: api/Users/5
        [HttpGet("{id}", Name = "GetUser")]
        public async Task<IActionResult> Get(long id)
        {
            if (this.IsUserAStranger(id))
            {
                return this.Unauthorized();
            }

            return this.Ok(await this.mediator.Send(new GetUserByIdQuery() { Id = id }).ConfigureAwait(false));
        }

        [HttpGet("{id}/Ban")]
        [Authorize(Policy = Policies.AdministratorOnly)]
        public async Task<IActionResult> BanUser(long id)
        {
            var request = new BanUserCommand() { UserId = id };
            await this.mediator.Send(request).ConfigureAwait(false);
            return this.Ok();
        }

        [HttpGet("{id}/Unban")]
        [Authorize(Policy = Policies.AdministratorOnly)]
        public async Task<IActionResult> UnbanUser(long id)
        {
            var request = new UnbanUserCommand() { UserId = id };
            await this.mediator.Send(request).ConfigureAwait(false);
            return this.Ok();
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, [FromBody] UpdateUserCommand command)
        {
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