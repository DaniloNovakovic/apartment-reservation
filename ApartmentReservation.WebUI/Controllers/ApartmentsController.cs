using System.Threading.Tasks;
using ApartmentReservation.Application.Features.Apartments.Queries;
using ApartmentReservation.Application.Infrastructure.Authentication;
using ApartmentReservation.Domain.Constants;
using MediatR;
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
            if (!this.IsInAnyRole(RoleNames.Administrator, RoleNames.Host))
            {
                query.ActivityState = ActivityStates.Active;
            }
            return this.Ok(await this.mediator.Send(query).ConfigureAwait(false));
        }

        public bool IsInAnyRole(params string[] roles)
        {
            foreach (string role in roles)
            {
                if (this.User.IsInRole(role))
                {
                    return true;
                }
            }
            return false;
        }
    }
}