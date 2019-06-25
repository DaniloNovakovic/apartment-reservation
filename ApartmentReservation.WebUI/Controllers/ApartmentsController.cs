using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ApartmentReservation.Application.Features.Apartments.Commands;
using ApartmentReservation.Application.Features.Apartments.Queries;
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
    [Authorize(Policy = Policies.AdministratorOrHostOnly)]
    public class ApartmentsController : ControllerBase
    {
        private readonly IMediator mediator;

        public ApartmentsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get([FromQuery]GetAllApartmentsQuery query)
        {
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
        public async Task<IActionResult> Get(long id)
        {
            var apartment = await this.mediator.Send(new GetApartmentQuery() { Id = id }).ConfigureAwait(false);
            apartment.AvailableDates = await this.mediator.Send(new GetAvailableDatesQuery() { ApartmentId = id }).ConfigureAwait(false);
            return this.Ok(apartment);
        }

        [HttpPost]
        [Authorize(Policy = Policies.HostOnly)]
        public async Task<IActionResult> Post([FromBody]CreateApartmentCommand command)
        {
            return this.Ok(await this.mediator.Send(command).ConfigureAwait(false));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, [FromBody]UpdateApartmentCommand command)
        {
            command.Id = id;
            await this.mediator.Send(command).ConfigureAwait(false);
            return this.Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var command = new DeleteApartmentCommand() { ApartmentId = id };
            await this.mediator.Send(command).ConfigureAwait(false);
            return this.Ok();
        }

        [HttpPut("{id}/Amenities")]
        public async Task<IActionResult> UpdateApartmentAmenities(long id, [FromBody]UpdateApartmentAmenitiesCommand command)
        {
            command.ApartmentId = id;
            await this.mediator.Send(command).ConfigureAwait(false);
            return this.Ok();
        }

        [HttpPut("{id}/ForRentalDates")]
        public async Task<IActionResult> UpdateForRentalDates(long id, [FromBody]UpdateForRentalDatesCommand command)
        {
            command.ApartmentId = id;
            await this.mediator.Send(command).ConfigureAwait(false);
            return this.Ok();
        }

        [HttpPost("{id}/Images")]
        public async Task<IActionResult> AddImages(long id, [FromForm]AddImagesToApartmentCommand command)
        {
            command.ApartmentId = id;
            await this.mediator.Send(command).ConfigureAwait(false);
            return this.Ok();
        }

        [HttpPost("{id}/delete-images")]
        public async Task<IActionResult> DeleteImages(long id, [FromBody]DeleteImagesFromApartmentCommand command)
        {
            command.ApartmentId = id;
            await this.mediator.Send(command).ConfigureAwait(false);
            return this.Ok();
        }
    }
}