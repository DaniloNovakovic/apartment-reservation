using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApartmentReservation.Application.Features.Hosts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApartmentReservation.WebUI.Controllers
{
    [Route("api/hosts")]
    [ApiController]
    [Authorize(Policy = "AdministratorOrHostOnly")]
    public class HostsController : ControllerBase
    {
        private readonly IMediator mediator;

        public HostsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        // GET: api/Hosts
        [Authorize(Policy = "AdministratorOnly")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Hosts/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<string> Get(int id)
        {
            return await this.mediator.Send(new GetHostQuery() { Id = id });
        }

        // POST: api/Hosts
        [HttpPost]
        [Authorize(Policy = "AdministratorOnly")]
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