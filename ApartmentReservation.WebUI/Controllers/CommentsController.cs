using ApartmentReservation.Application.Features;
using ApartmentReservation.Application.Features.Comments.Commands;
using ApartmentReservation.Application.Features.Reservations.Queries;
using ApartmentReservation.Application.Interfaces;
using ApartmentReservation.Common.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ApartmentReservation.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IAuthService authService;

        public CommentsController(IMediator mediator, IAuthService authService)
        {
            this.mediator = mediator;
            this.authService = authService;
        }

        [HttpGet("{id}/Approve")]
        [Authorize(Policy = Policies.HostOnly)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Approve(long id)
        {
            if (await this.authService.CheckIfBanned(this.User).ConfigureAwait(false))
            {
                return this.Forbid();
            }

            var command = new UpdateCommentCommand() { Id = id, Approved = true };
            return this.Ok(await this.mediator.Send(command).ConfigureAwait(false));
        }

        [HttpPost]
        [Authorize(Policy = Policies.GuestOnly)]
        [ProducesResponseType(typeof(EntityCreatedResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Post([FromBody] CreateCommentCommand command)
        {
            bool isAllowed = await this.IsAllowedToCreateComment(command.ApartmentId, command.GuestId).ConfigureAwait(false);

            if (!isAllowed)
            {
                return this.Unauthorized();
            }

            return this.Ok(await this.mediator.Send(command).ConfigureAwait(false));
        }

        [HttpGet("CanPostComment")]
        public async Task<IActionResult> CanPostComment(long? apartmentId, long? guestId)
        {
            bool isAllowed = false;
            if (!(apartmentId is null) && !(guestId is null))
            {
                isAllowed = await this.IsAllowedToCreateComment(apartmentId.Value, guestId.Value).ConfigureAwait(false);
            }
            return this.Ok(new { Allowed = isAllowed });
        }

        private async Task<bool> IsAllowedToCreateComment(long apartmentId, long guestId)
        {
            if (await this.authService.CheckIfBanned(this.User).ConfigureAwait(false))
            {
                return false;
            }

            var query = new GetAllReservationsQuery() { ApartmentId = apartmentId, GuestId = guestId };
            var reservations = await this.mediator.Send(query).ConfigureAwait(false);
            string[] allowedStates = new[] { ReservationStates.Completed };
            return reservations.Any(r => allowedStates.Contains(r.ReservationState));
        }
    }
}