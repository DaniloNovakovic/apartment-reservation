using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Features.Hosts;
using ApartmentReservation.Application.Infrastructure.Authentication;
using ApartmentReservation.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApartmentReservation.WebUI.Controllers
{
    [Route("api/hosts")]
    [ApiController]
    [Authorize(Policy = Policies.AdministratorOrHostOnly)]
    public class HostsController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IUnitOfWork unitOfWork;

        public HostsController(IMediator mediator, IUnitOfWork unitOfWork)
        {
            this.mediator = mediator;
            this.unitOfWork = unitOfWork;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> Login([FromBody] UserDto dto)
        {
            string username = dto.username;
            string password = dto.password;

            var host = await this.unitOfWork.Hosts.GetAsync(username);

            if (host is null)
                return this.NotFound();

            if (host.Password != password)
                return this.Unauthorized();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, username),
                new Claim(ClaimTypes.Role, RoleNames.Host)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await this.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));

            return this.Ok();
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
                return Unauthorized();
            }

            return Ok(await this.mediator.Send(new GetHostQuery() { Id = id }).ConfigureAwait(false));
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