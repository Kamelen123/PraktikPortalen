using Microsoft.AspNetCore.Mvc;
using PraktikPortalen.Application.DTOs.Auth;
using PraktikPortalen.Application.Services.Interfaces;

namespace PraktikPortalen.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;
        public AuthController(IAuthService auth) => _auth = auth;

        // POST api/v1/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto, CancellationToken ct)
        {
            var res = await _auth.LoginAsync(dto, ct);
            return res is null ? Unauthorized() : Ok(res);
        }

        // POST api/v1/auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto, CancellationToken ct)
        {
            var res = await _auth.RegisterAsync(dto, ct);
            return res is null ? Conflict("Email already exists or invalid.") : Ok(res);
        }
    }
}
