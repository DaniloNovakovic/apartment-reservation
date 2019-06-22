using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ApartmentReservation.Application.Features.Comments.Commands;
using ApartmentReservation.Application.Features.Comments.Queries;
using ApartmentReservation.Application.Features.Reservations.Queries;
using ApartmentReservation.Application.Infrastructure.Authentication;
using ApartmentReservation.Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApartmentReservation.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IMediator mediator;

        public CommentsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllCommentsQuery query)
        {
            if (!this.User.IsInRole(RoleNames.Host) && !this.User.IsInRole(RoleNames.Administrator))
            {
                query.Approved = true;
            }
            return this.Ok(await mediator.Send(query).ConfigureAwait(false));
        }

        [HttpGet("{id}/Approve")]
        [Authorize(Policy = Policies.HostOnly)]
        public async Task<IActionResult> Approve(long id)
        {
            var command = new UpdateCommentCommand() { Id = id, Approved = true };
            return this.Ok(await this.mediator.Send(command).ConfigureAwait(false));
        }

        [HttpPost]
        [Authorize(Policy = Policies.GuestOnly)]
        public async Task<IActionResult> Post([FromBody]CreateCommentCommand command)
        {
            bool isAllowed = await this.IsAllowedToCreateComment(command.ApartmentId, command.GuestId).ConfigureAwait(false);

            if (!isAllowed)
            {
                return this.Unauthorized();
            }

            return this.Ok(await this.mediator.Send(command).ConfigureAwait(false));
        }

        private async Task<bool> IsAllowedToCreateComment(long apartmentId, long guestId)
        {
            var query = new GetAllReservationsQuery() { ApartmentId = apartmentId, GuestId = guestId };
            var reservations = await this.mediator.Send(query).ConfigureAwait(false);
            string[] allowedStates = new[] { ReservationStates.Completed, ReservationStates.Denied };
            return reservations.Any(r => allowedStates.Contains(r.ReservationState));
        }
    }
}