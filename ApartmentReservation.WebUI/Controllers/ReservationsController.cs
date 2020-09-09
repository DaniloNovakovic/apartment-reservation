using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Features;
using ApartmentReservation.Application.Features.Reservations.Commands;
using ApartmentReservation.Application.Features.Reservations.Queries;
using ApartmentReservation.Application.Interfaces;
using ApartmentReservation.Common;
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
    public class ReservationsController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IAuthService authService;

        public ReservationsController(IMediator mediator, IAuthService authService)
        {
            this.mediator = mediator;
            this.authService = authService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ReservationDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Get([FromQuery] GetAllReservationsQuery query)
        {
            if (await authService.CheckIfBanned(this.User).ConfigureAwait(false))
            {
                return this.Forbid();
            }

            var currClaim = this.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (this.User.IsInRole(RoleNames.Guest))
            {
                query.GuestId = long.Parse(currClaim.Value);
                query.ReservationState = "";
                query.HostId = null;
            }
            if (this.User.IsInRole(RoleNames.Host))
            {
                query.HostId = long.Parse(currClaim.Value);
            }

            return this.Ok(await this.mediator.Send(query).ConfigureAwait(false));
        }

        [HttpPost]
        [Authorize(Policy = Policies.GuestOnly)]
        [ProducesResponseType(typeof(EntityCreatedResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Post([FromBody] CreateReservationCommand command)
        {
            if (await authService.CheckIfBanned(this.User).ConfigureAwait(false))
            {
                return this.Forbid();
            }

            return this.Ok(await this.mediator.Send(command).ConfigureAwait(false));
        }

        [HttpGet("{id}/Withdraw")]
        [Authorize(Policy = Policies.GuestOnly)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Withdraw(long id)
        {
            return await this.UpdateReservationAsync(id, ReservationStates.Withdrawn, (args) =>
            {
                return args.ReservationState == ReservationStates.Created
                    || args.ReservationState == ReservationStates.Accepted;
            });
        }

        [HttpGet("{id}/Deny")]
        [Authorize(Policy = Policies.HostOnly)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Deny(long id)
        {
            return await this.UpdateReservationAsync(id, ReservationStates.Denied, (args) =>
            {
                return args.ReservationState == ReservationStates.Created;
            });
        }

        [HttpGet("{id}/Accept")]
        [Authorize(Policy = Policies.HostOnly)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Accept(long id)
        {
            return await this.UpdateReservationAsync(id, ReservationStates.Accepted, (args) =>
            {
                return args.ReservationState == ReservationStates.Created;
            });
        }

        [HttpGet("{id}/Complete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Complete(long id)
        {
            return await this.UpdateReservationAsync(id, ReservationStates.Completed, (args) =>
            {
                var endDate = args.ReservationStartDate.AddDays(args.NumberOfNightsRented);
                var today = DateTime.Now;
                if (!DateTimeHelpers.AreSameDay(today, endDate) && today < endDate)
                {
                    return false;
                }
                if (args.ReservationState != ReservationStates.Accepted)
                {
                    return false;
                }
                return true;
            });
        }

        private async Task<IActionResult> UpdateReservationAsync(long id, string reservationState, Predicate<CanUpdateReservationArgs> CanUpdate)
        {
            if (await authService.CheckIfBanned(this.User).ConfigureAwait(false))
            {
                return this.Forbid();
            }

            var command = new UpdateReservationCommand() { Id = id, ReservationState = reservationState, CanUpdate = CanUpdate };
            await this.mediator.Send(command).ConfigureAwait(false);
            return this.Ok();
        }
    }
}