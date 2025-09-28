using PaymentApp.Application.Dto.Auth;
using PaymentApp.Domain.Framework;
using System.Security.Claims;

namespace PaymentApp.Application.Services.Auth
{
    public interface IJwtService
    {
        Result<TokenDto> GenerateToken(List<Claim> claims);

        Task<Result<bool>> TokenIsActive(string token);

        Task<Result> DeactivateToken(string token);
    }
}
