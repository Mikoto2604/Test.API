using PaymentApp.Application.Dto.Auth;
using PaymentApp.Domain.Framework;

namespace PaymentApp.Application.Services.Auth
{
    public interface IAuthService
    {
        Task<Result<TokenDto>> LoginAsync(LoginRequestDto loginRequestDto);
        Task<Result> LogoutAsync(string token);
    }
}
