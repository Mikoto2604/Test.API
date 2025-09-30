using Microsoft.AspNetCore.Mvc;
using PaymentApp.Domain.Framework;
using PaymentApp.Application.Dto.Auth;
using Microsoft.AspNetCore.Authorization;
using PaymentApp.Application.Services.Auth;

namespace PaymentApp.Api.Controllers
{
    
    [ApiController]
    [Route("api/auth")]
    public class AuthController: BaseController
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService) => _authService = authService;

        [HttpPost("login")]
        [ProducesResponseType(typeof(TokenDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login(LoginRequestDto request)=> Ok(await _authService.LoginAsync(request));

        [Authorize]
        [HttpPost("logout")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Logout() => Ok(await _authService.LogoutAsync(Token));
    }
}
