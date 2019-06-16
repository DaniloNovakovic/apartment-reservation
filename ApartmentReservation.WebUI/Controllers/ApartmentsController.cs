using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ApartmentReservation.Application.Features.Apartments.Commands;
using ApartmentReservation.Application.Features.Apartments.Queries;
using ApartmentReservation.Application.Infrastructure.Authentication;
using ApartmentReservation.Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApartmentReservation.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApartmentsController : ControllerBase
    {
        private readonly IMediator mediator;

        public ApartmentsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]GetAllApartmentsQuery query)
        {
            if (!this.CanSeeInactiveApartments(query))
            {
                query.ActivityState = ActivityStates.Active;
            }
            return this.Ok(await this.mediator.Send(query).ConfigureAwait(false));
        }

        // GET: api/Apartments/5
        [HttpGet("{id}", Name = "GetApartment")]
        public async Task<IActionResult> Get(long id)
        {
            return this.Ok(await this.mediator.Send(new GetApartmentQuery() { Id = id })
                .ConfigureAwait(false));
        }

        [HttpPost]
        [Authorize(Policy = Policies.HostOnly)]
        public async Task<IActionResult> Post([FromBody]CreateApartmentCommand command)
        {
            return Ok(await mediator.Send(command).ConfigureAwait(false));
        }

        private bool CanSeeInactiveApartments(GetAllApartmentsQuery query)
        {
            if (this.User.IsInRole(RoleNames.Administrator))
            {
                return true;
            }

            if (this.User.IsInRole(RoleNames.Host))
            {
                return query.HostId is null || IsAskingForStrangerInfo(query.HostId.Value);
            }

            return false;
        }

        private bool IsAskingForStrangerInfo(long requestedUserId)
        {
            var currClaim = this.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            return requestedUserId.ToString().Equals(currClaim?.Value ?? "");
        }
    }
}