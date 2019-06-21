using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ApartmentReservation.Application.Features.Reservations.Commands;
using ApartmentReservation.Application.Features.Reservations.Queries;
using ApartmentReservation.Application.Infrastructure.Authentication;
using ApartmentReservation.Common;
using ApartmentReservation.Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApartmentReservation.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReservationsController : ControllerBase
    {
        private readonly IMediator mediator;

        public ReservationsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllReservationsQuery query)
        {
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
        public async Task<IActionResult> Post([FromBody] CreateReservationCommand command)
        {
            return this.Ok(await this.mediator.Send(command).ConfigureAwait(false));
        }

        [HttpGet("{id}/Withdraw")]
        [Authorize(Policy = Policies.GuestOnly)]
        public async Task<IActionResult> Withdraw(long id)
        {
            return await UpdateReservationAsync(id, ReservationStates.Withdrawn, (args) =>
            {
                return args.ReservationState == ReservationStates.Created
                    || args.ReservationState == ReservationStates.Accepted;
            });
        }

        [HttpGet("{id}/Deny")]
        [Authorize(Policy = Policies.HostOnly)]
        public async Task<IActionResult> Deny(long id)
        {
            return await UpdateReservationAsync(id, ReservationStates.Denied, (args) =>
            {
                return args.ReservationState == ReservationStates.Created;
            });
        }

        [HttpGet("{id}/Accept")]
        [Authorize(Policy = Policies.HostOnly)]
        public async Task<IActionResult> Accept(long id)
        {
            return await UpdateReservationAsync(id, ReservationStates.Accepted, (args) =>
            {
                return args.ReservationState == ReservationStates.Created;
            });
        }

        [HttpGet("{id}/Complete")]
        public async Task<IActionResult> Complete(long id)
        {
            return await UpdateReservationAsync(id, ReservationStates.Completed, (args) =>
            {
                var endDate = args.ReservationStartDate.AddDays(args.NumberOfNightsRented);
                var today = DateTime.Now;
                if (!DateTimeHelpers.AreSameDay(today, endDate) && today < endDate)
                {
                    return false;
                }

                return true;
            });
        }

        private async Task<IActionResult> UpdateReservationAsync(long id, string reservationState, Predicate<CanUpdateReservationArgs> CanUpdate)
        {
            var command = new UpdateReservationCommand() { Id = id, ReservationState = reservationState, CanUpdate = CanUpdate };
            await this.mediator.Send(command).ConfigureAwait(false);
            return this.Ok();
        }
    }
}