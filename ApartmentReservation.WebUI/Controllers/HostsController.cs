using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Features.Hosts;
using ApartmentReservation.Application.Infrastructure.Authentication;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApartmentReservation.WebUI.Controllers
{
    [Route("api/hosts")]
    [ApiController]
    [Authorize(Policy = Policies.AdministratorOrHostOnly)]
    public class HostsController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly AuthService authService;

        public HostsController(IMediator mediator, AuthService authService)
        {
            this.mediator = mediator;
            this.authService = authService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> Login([FromBody] UserDto dto)
        {
            await this.authService.Login(dto, RoleNames.Host, this.HttpContext);

            return this.NoContent();
        }

        // GET: api/Hosts
        [Authorize(Policy = Policies.AdministratorOnly)]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Hosts/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(string id)
        {
            if (!this.HttpContext.User.HasClaim(ClaimTypes.NameIdentifier, id) && !this.HttpContext.User.IsInRole("Administrator"))
            {
                return this.Unauthorized();
            }

            return this.Ok(await this.mediator.Send(new GetHostQuery() { Id = id }).ConfigureAwait(false));
        }

        // POST: api/Hosts
        [HttpPost]
        [Authorize(Policy = Policies.AdministratorOnly)]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Hosts/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}