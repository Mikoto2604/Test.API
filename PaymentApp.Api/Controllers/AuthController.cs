using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaymentApp.Application.Dto.Auth;
using PaymentApp.Application.Services.Auth;
using PaymentApp.Domain.Framework;

namespace PaymentApp.Api.Controllers
{
    
    [ApiController]
    [Route("auth")]
    public class AuthController: ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(TokenDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login(LoginRequestDto request)=> Ok(await _authService.LoginAsync(request));

        [Authorize]
        [HttpPost("logout")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        public async Task<IActionResult> Logout()
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            return Ok(await _authService.LogoutAsync(token));
        }
    }
}
