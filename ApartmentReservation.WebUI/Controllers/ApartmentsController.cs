using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Features;
using ApartmentReservation.Application.Features.Apartments.Commands;
using ApartmentReservation.Application.Features.Apartments.Queries;
using ApartmentReservation.Application.Features.Reservations.Queries;
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
    public class ApartmentsController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IAuthService authService;

        public ApartmentsController(IMediator mediator, IAuthService authService)
        {
            this.mediator = mediator;
            this.authService = authService;
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<ApartmentDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Get([FromQuery]GetAllApartmentsQuery query)
        {
            if (await authService.CheckIfBanned(this.User).ConfigureAwait(false))
            {
                return this.Forbid();
            }

            if (this.User.IsInRole(RoleNames.Host))
            {
                var currClaim = this.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                if (long.TryParse(currClaim?.Value ?? "", out long id))
                {
                    query.HostId = id;
                }
            }
            else if (!this.User.IsInRole(RoleNames.Administrator))
            {
                query.ActivityState = ActivityStates.Active;
            }

            return this.Ok(await this.mediator.Send(query).ConfigureAwait(false));
        }

        // GET: api/Apartments/5
        [HttpGet("{id}", Name = "GetApartment")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApartmentDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Get(long id)
        {
            if (await authService.CheckIfBanned(this.User).ConfigureAwait(false))
            {
                return this.Forbid();
            }

            var apartment = await this.mediator.Send(new GetApartmentQuery() { Id = id }).ConfigureAwait(false);
            return this.Ok(apartment);
        }

        [HttpPost]
        [Authorize(Policy = Policies.HostOnly)]
        [ProducesResponseType(typeof(EntityCreatedResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Post([FromBody]CreateApartmentCommand command)
        {
            if (await authService.CheckIfBanned(this.User).ConfigureAwait(false))
            {
                return this.Forbid();
            }

            return this.Ok(await this.mediator.Send(command).ConfigureAwait(false));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Put(long id, [FromBody]UpdateApartmentCommand command)
        {
            if (await authService.CheckIfBanned(this.User).ConfigureAwait(false))
            {
                return this.Forbid();
            }

            command.Id = id;
            await this.mediator.Send(command).ConfigureAwait(false);
            return this.Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Delete(long id)
        {
            if (await authService.CheckIfBanned(this.User).ConfigureAwait(false))
            {
                return this.Forbid();
            }

            var command = new DeleteApartmentCommand() { ApartmentId = id };
            await this.mediator.Send(command).ConfigureAwait(false);
            return this.Ok();
        }

        [HttpPut("{id}/Amenities")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateApartmentAmenities(long id, [FromBody]UpdateApartmentAmenitiesCommand command)
        {
            if (await authService.CheckIfBanned(this.User).ConfigureAwait(false))
            {
                return this.Forbid();
            }

            command.ApartmentId = id;
            await this.mediator.Send(command).ConfigureAwait(false);
            return this.Ok();
        }

        [HttpPut("{id}/ForRentalDates")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateForRentalDates(long id, [FromBody]UpdateForRentalDatesCommand command)
        {
            if (await authService.CheckIfBanned(this.User).ConfigureAwait(false))
            {
                return this.Forbid();
            }

            command.ApartmentId = id;
            await this.mediator.Send(command).ConfigureAwait(false);
            return this.Ok();
        }

        [HttpPost("{id}/Images")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> AddImages(long id, [FromForm]AddImagesToApartmentCommand command)
        {
            if (await authService.CheckIfBanned(this.User).ConfigureAwait(false))
            {
                return this.Forbid();
            }

            command.ApartmentId = id;
            await this.mediator.Send(command).ConfigureAwait(false);
            return this.Ok();
        }

        [HttpPost("{id}/delete-images")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteImages(long id, [FromBody]DeleteImagesFromApartmentCommand command)
        {
            if (await authService.CheckIfBanned(this.User).ConfigureAwait(false))
            {
                return this.Forbid();
            }

            command.ApartmentId = id;
            await this.mediator.Send(command).ConfigureAwait(false);
            return this.Ok();
        }
    }
}