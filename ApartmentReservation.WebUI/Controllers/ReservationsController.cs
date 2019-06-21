using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ApartmentReservation.Application.Features.Reservations.Commands;
using ApartmentReservation.Application.Features.Reservations.Queries;
using ApartmentReservation.Application.Infrastructure.Authentication;
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
    }
}