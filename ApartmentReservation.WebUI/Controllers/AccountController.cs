using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApartmentReservation.WebUI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService authService;

        public AccountController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserDto dto)
        {
            await this.authService.LoginAsync(dto, dto.RoleName, this.HttpContext).ConfigureAwait(false);
            return this.NoContent();
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            string roleName = this.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Role)?.Value ?? "";
            await this.authService.LogoutAsync(roleName, this.HttpContext).ConfigureAwait(false);
            return this.NoContent();
        }
    }
}