using System.Threading.Tasks;
using ApartmentReservation.Application.Features.Amenities.Commands;
using ApartmentReservation.Application.Features.Amenities.Queries;
using ApartmentReservation.Application.Infrastructure.Authentication;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApartmentReservation.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = Policies.AdministratorOrHostOnly)]
    public class AmenitiesController : ControllerBase
    {
        private readonly IMediator mediator;

        public AmenitiesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        // GET: api/Amenities
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]GetAllAmenitiesQuery query)
        {
            return this.Ok(await this.mediator.Send(query).ConfigureAwait(false));
        }

        // GET: api/Amenities/5
        [HttpGet("{id}", Name = "GetAmenity")]
        public async Task<IActionResult> Get(long id)
        {
            return this.Ok(await this.mediator.Send(new GetAmenityQuery() { Id = id }).ConfigureAwait(false));
        }

        // POST: api/Amenities
        [HttpPost]
        [Authorize(Policy = Policies.AdministratorOnly)]
        public async Task<IActionResult> Post([FromBody] CreateAmenityCommand command)
        {
            await this.mediator.Send(command).ConfigureAwait(false);

            return this.NoContent();
        }

        // PUT: api/Amenities/5
        [HttpPut("{id}")]
        [Authorize(Policy = Policies.AdministratorOnly)]
        public async Task<IActionResult> Put(long id, [FromBody] UpdateAmenityCommand command)
        {
            command.Id = id;
            await this.mediator.Send(command).ConfigureAwait(false);

            return this.NoContent();
        }

        // DELETE: api/Amenities/5
        [HttpDelete("{id}")]
        [Authorize(Policy = Policies.AdministratorOnly)]
        public async Task<IActionResult> Delete(long id)
        {
            await this.mediator.Send(new DeleteAmenityCommand() { Id = id }).ConfigureAwait(false);

            return this.NoContent();
        }
    }
}