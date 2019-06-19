using System.Threading.Tasks;
using ApartmentReservation.Application.Features.Reservations.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ApartmentReservation.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
            return this.Ok(await this.mediator.Send(query).ConfigureAwait(false));
        }
    }
}